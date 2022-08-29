using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChange : MonoBehaviour
{
    [SerializeField] Color colorA = Color.black;
    [SerializeField] Color colorB = Color.white;

    [SerializeField] string parameterName = "_BaseColor";

    private bool colorState;

    public void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.SetColor(parameterName, colorState? colorA : colorB);
        colorState = !colorState;
    }
}
