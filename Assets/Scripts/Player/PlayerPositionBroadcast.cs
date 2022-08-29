using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionBroadcast : MonoBehaviour
{
    void LateUpdate()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}
