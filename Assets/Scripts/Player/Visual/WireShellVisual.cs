using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireShellVisual : MonoBehaviour
{
    [SerializeField] protected Transform LineParent;

    [SerializeField]
    protected Transform[] Vertecies;

    [SerializeField]
    protected LineVertexPair[] Lines;

    [System.Serializable]
    public struct LineVertexPair
    {
        [System.NonSerialized] public LineRenderer lineRend;
        public Vector2Int LineVerts;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            GameObject newLine = new GameObject("Line_" + Lines[i].LineVerts.x + '_' + Lines[i].LineVerts.y);
            newLine.transform.SetParent(LineParent);
            Lines[i].lineRend = newLine.AddComponent<LineRenderer>();
            Lines[i].lineRend.startWidth = 0.1f;
            Lines[i].lineRend.endWidth = 0.1f;
        }
        UpdateLineVerts();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineVerts();
    }

    protected void UpdateLineVerts()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].lineRend.SetPositions(new Vector3[]{ Vertecies[Lines[i].LineVerts.x].position, Vertecies[Lines[i].LineVerts.y].position});  
        }
    }
}
