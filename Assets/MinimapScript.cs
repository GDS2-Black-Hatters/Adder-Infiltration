using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    public bool rotate;

    void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        if(rotate)
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
