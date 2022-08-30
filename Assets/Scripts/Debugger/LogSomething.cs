using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSomething : MonoBehaviour
{
    [SerializeField] private string logMessage = "Insert Message Here";

    public void PrintLog()
    {
        Debug.Log(logMessage);
    }
}
