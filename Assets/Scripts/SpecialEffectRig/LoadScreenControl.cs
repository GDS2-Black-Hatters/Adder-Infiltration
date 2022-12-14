using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenControl : MonoBehaviour
{
    public static LoadScreenControl Instance { get; private set; }

    //General Parameters
    private const float transitionFogDensity = 0.01f;
    private const float loadFogDensity = 0.3f;

    //Entry Parameters
    private const float entryRate = 0.2f;

    //Exit Parameters
    private const float approachLerpSpeed = 0.8f;
    private const float minApproachDistance = 10;
    private const float maxApproachDistance = 15;
    private const float initialOffset = 10;
    private const float targetEndApproachSpeed = 1;

    [SerializeField] private Transform exitRigTransform;
    [SerializeField] private SmoothRandomForward cameraAutoMovement;

    private bool canExit = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitiateEntry());
        StartCoroutine(DelayExit());
    }

    //A Test Function to test exit functionality
    private IEnumerator DelayExit()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(InitiateExit(() => { GameManager.LevelManager.ActiveSceneController.EngageScene(); }));
    }

    public IEnumerator InitiateEntry()
    {
        float progress = 0;
        while(progress < 1)
        {
            RenderSettings.fogDensity = Mathf.SmoothStep(transitionFogDensity, loadFogDensity, progress);

            progress += Time.deltaTime * entryRate;
            yield return null;
        }
        canExit = true;
    }

    public IEnumerator InitiateExit(System.Action onCompletionCall)
    {
        Debug.Log("Start Load End Sequence");

        float progress = 0;

        //first slow camera rotation
        float camIniRotSpeed = cameraAutoMovement.rotSpeed;
        while(progress < 1)
        {
            cameraAutoMovement.rotSpeed = Mathf.SmoothStep(camIniRotSpeed, 0, progress);
            progress += Time.deltaTime * approachLerpSpeed;
            yield return null;
        }

        progress = 0;

        exitRigTransform.gameObject.SetActive(true);
        //start position = forward from camera and some random offset to the side
        Vector3 exitRigStartPos = cameraAutoMovement.transform.TransformPoint(new(Random.Range(-initialOffset,initialOffset), Random.Range(-initialOffset,initialOffset), Random.Range(minApproachDistance,maxApproachDistance) * cameraAutoMovement.forwardSpeed));
        
        //start rotation = looking at the camera's expected position
        Quaternion exitRigStartRot = Quaternion.LookRotation(exitRigStartPos - cameraAutoMovement.transform.TransformPoint(new(0,0, cameraAutoMovement.forwardSpeed * 2/approachLerpSpeed)));

        float iniApproachSpeed = cameraAutoMovement.forwardSpeed;

        //slowly move the exit rig into position
        while (progress < 1)
        {
            float easeOutProgress = 1-(Mathf.Pow(1-progress, 2.5f));
            exitRigTransform.position = Vector3.Lerp( exitRigStartPos, cameraAutoMovement.transform.position, easeOutProgress);
            exitRigTransform.rotation = Quaternion.Slerp(exitRigStartRot, cameraAutoMovement.transform.rotation, easeOutProgress);

            cameraAutoMovement.transform.rotation = Quaternion.RotateTowards(cameraAutoMovement.transform.rotation, Quaternion.LookRotation(exitRigStartPos - cameraAutoMovement.transform.position), progress * 20 * Time.deltaTime);

            cameraAutoMovement.forwardSpeed = Mathf.SmoothStep(iniApproachSpeed, targetEndApproachSpeed, progress);

            RenderSettings.fogDensity = Mathf.SmoothStep(loadFogDensity, transitionFogDensity, progress);

            progress += Time.deltaTime * approachLerpSpeed;
            yield return null;
        }

        exitRigTransform.SetParent(cameraAutoMovement.transform);
        exitRigTransform.localPosition = Vector3.zero;
        exitRigTransform.localRotation = Quaternion.identity;
        onCompletionCall.Invoke();

        yield return new WaitForEndOfFrame();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
