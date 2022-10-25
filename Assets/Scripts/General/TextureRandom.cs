using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandom : MonoBehaviour
{
    [SerializeField] Texture2D[] availableTexture;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_MainText", availableTexture[Random.Range(0, availableTexture.Length)]);
        Destroy(this);
    }
}
