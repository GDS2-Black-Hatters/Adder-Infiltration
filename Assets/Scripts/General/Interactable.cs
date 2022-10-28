using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private float interactHoldDuration = 3;
    [SerializeField] private UnityEngine.Events.UnityEvent onInteractComplete;
    [SerializeField] private UnityEngine.Events.UnityEvent<float> onInteractProgress; //for updating visuals, sends progress as a float from 0-1.

    private TimeTracker interactHoldTimer;
    [SerializeField] protected AK.Wwise.Event progressSFXEvent;

    private void Start()
    {
        interactHoldTimer = new(interactHoldDuration, 1);
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
        progressSFXEvent.Post(gameObject);
    }

    public void InteractHalt()
    {
        StopAllCoroutines();
        StartCoroutine(Interacting(false));
        progressSFXEvent.Stop(gameObject);
    }

    private IEnumerator Interacting(bool holding)
    {
        int scale = holding ? 1 : -1;
        interactHoldTimer.Update(scale > 0 ? interactHoldDuration * 0.075f : 0);
        do
        {
            interactHoldTimer.Update(Time.deltaTime * scale);
            onInteractProgress.Invoke(interactHoldTimer.TimePercentage);
            yield return null;
        //has a minor bug where if the interact is started at a frame with time.deltatime = 0, the check will fail and the coroutine will end immediately.
        } while (interactHoldTimer.TimePercentage != 0 && interactHoldTimer.TimePercentage != 1);
    }
}
