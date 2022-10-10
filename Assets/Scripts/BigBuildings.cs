using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuildings : MonoBehaviour
{

    void Start()
     {
         StartCoroutine(LateStart(0.1f));
     }
 
     IEnumerator LateStart(float waitTime)
     {
         yield return new WaitForSeconds(waitTime);
         //Your Function You Want to Call
         GameObject buildings;
         buildings = transform.Find("ProceduralChunkGenerator1").gameObject;
         buildings.transform.localScale = new Vector3(10,10,10);
         buildings = transform.Find("ProceduralChunkGenerator2").gameObject;
         buildings.transform.localScale = new Vector3(10,10,10);
         buildings = transform.Find("ProceduralChunkGenerator3").gameObject;
         buildings.transform.localScale = new Vector3(10,10,10);
         buildings = transform.Find("ProceduralChunkGenerator4").gameObject;
         buildings.transform.localScale = new Vector3(10,10,10);
         buildings = transform.Find("ProceduralChunkGenerator5").gameObject;
         buildings.transform.localScale = new Vector3(10,10,10);
     }
}
