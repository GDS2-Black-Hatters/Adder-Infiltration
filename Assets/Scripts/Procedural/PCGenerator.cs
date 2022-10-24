using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PCGenerator : MonoBehaviour
{
    [SerializeField] private bool generateOnStart;
    [SerializeField] private bool testGenerate;
    [SerializeField] private bool selfDestruct = false;

    public UnityEngine.Events.UnityEvent OnGenerationComplete;
    private bool generating = false;
    private bool stillGenerating = true;

    protected abstract IEnumerator Generate();

    protected void Start()
    {
        if(generateOnStart)
        {
            generating = true;
            StartCoroutine(Generate());
        }
    }
    protected void OnValidate()
    {
        if(testGenerate)
        {
            testGenerate = false;
            generating = true;
            StartCoroutine(Generate());
        }
    }
    protected void GenerationIncomplete()
    {
        stillGenerating = true;
    }

    protected void LateUpdate()
    {
        if(!generating) return;

        if(!stillGenerating)
        {
            //Debug.Log("Generation Complete");
            OnGenerationComplete.Invoke();
            generating = false;
            if(selfDestruct) Destroy(this);
        }
        stillGenerating = false;
    }
}
