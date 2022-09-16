using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private float interactHoldDuration = 3;
    [SerializeField] private UnityEngine.Events.UnityEvent onInteractComplete;
    [SerializeField] private UnityEngine.Events.UnityEvent<float> onInteractProgress; //for updating visuals, sends progress as a float from 0-1.

    private TimeTracker interactHoldTimer;

    private void Start()
    {
        interactHoldTimer = new TimeTracker(interactHoldDuration, 1, false);
        interactHoldTimer.onFinish += () => onInteractComplete.Invoke();
    }

    public void OnFocus()
    {
        //Debug.Log("Focus: " + transform.parent.name);
    }

    public void OnUnfocus()
    {
        StopAllCoroutines();
        StartCoroutine(Interacting(false));
        //Debug.Log("Unfocus: " + transform.parent.name);
    }

    public void AddInteraction(UnityEngine.Events.UnityAction unityAction)
    {
        onInteractComplete.AddListener(unityAction);
    }

    public void InteractStart()
    {
        StopAllCoroutines();
        StartCoroutine(Interacting(true));
    }

    public void InteractHalt()
    {
        StopAllCoroutines();
        StartCoroutine(Interacting(false));        
    }

    private IEnumerator Interacting(bool holding)
    {
        interactHoldTimer.SetTimeScale(holding ? 1f : -1f);
        do
        {
            interactHoldTimer.Update(Time.deltaTime);
            onInteractProgress.Invoke(interactHoldTimer.tick / interactHoldDuration);
            yield return null;
        //has a minor bug where if the interact is started at a frame with time.deltatime = 0, the check will fail and the coroutine will end immediately.
        } while (interactHoldTimer.tick > 0 && interactHoldTimer.tick < interactHoldDuration);

        if(holding && interactHoldTimer.tick >= interactHoldDuration)
        {
            onInteractComplete.Invoke();
        }
    }
}
