using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DynamicArcConeMesh : MonoBehaviour
{
    [SerializeField] private float horizontalFov = 60;
    [SerializeField] private int horizontalResolution = 10;
    [SerializeField] private float verticalFov = 20;
    [SerializeField] private int verticalResolution = 4;
    private float horizontalAnglePer, verticalAnglePer; //caching these values to minimize division calculation

    [SerializeField] private float range = 10;

    [SerializeField] private LayerMask occlusionMask;

    private Mesh mesh;
    private Vector3[] vertex;

    private void Awake()
    {
        InitilizeMesh();
    }

    private void Update()
    {
        UpdateVertex();
        mesh.vertices = vertex;
    }

    private void InitilizeMesh()
    {
        mesh = new();
        mesh.MarkDynamic();

        horizontalAnglePer = horizontalFov / horizontalResolution;
        verticalAnglePer = verticalFov / verticalResolution;

        //Verts
        vertex = new Vector3[(horizontalResolution + 1) * (verticalResolution + 1) + 1];
        UpdateVertex();
    
        //Triangles
        /*
        front face: horizontalResolution * verticalResolution * 2 * 3
        top bottom face: horizontalResolution * 2 * 2 * 3 
        side face: verticalResolution * 2 * 2 * 3
        */
        int[] triangles = new int[( (horizontalResolution * verticalResolution) + ( (horizontalResolution + verticalResolution) * 2) ) * 6];
        
        //Front Faces
        int ti = 0;
        for (int vi = 0, y = 0; y < verticalResolution; y++, vi++) {
			for (int x = 0; x < horizontalResolution; x++, ti += 6, vi++) {
				triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + horizontalResolution + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + 1;
                triangles[ti + 5] = vi + horizontalResolution + 2;
            }
		}

        ti += 6;
        //Top and Bottom
        for(int x = 0; x < horizontalResolution; x++, ti += 6){
            //bottom face
            triangles[ti] = x + 1;
            triangles[ti + 1] = x;
            triangles[ti + 2] = vertex.Length - 1;
            
            //top face
            triangles[ti + 3] = vertex.Length - x - 3;
            triangles[ti + 4] = vertex.Length - x - 2;
            triangles[ti + 5] = vertex.Length - 1;
        }

        //Side Faces
        for(int y = 0; y < verticalResolution; y++, ti += 6){
            //left face
            triangles[ti] = y * (horizontalResolution + 1);
            triangles[ti + 1] = (y + 1) * (horizontalResolution + 1);
            triangles[ti + 2] = vertex.Length - 1;
            
            //right face
            triangles[ti + 3] = (y + 1) * (horizontalResolution + 1) + horizontalResolution;
            triangles[ti + 4] = y * (horizontalResolution + 1) + horizontalResolution;
            triangles[ti + 5] = vertex.Length - 1;
        }

        mesh.vertices = vertex;
        mesh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        if (gameObject.TryGetComponent(out MeshCollider col))
        {
            col.sharedMesh = mesh;
        }
    }

    private void UpdateVertex()
    {
        for (int i = 0; i < vertex.Length - 1; i++)
        {
            Vector3 direction = GetDirectionFromIndex(i);
            vertex[i] = Physics.Raycast(transform.position, transform.rotation * direction, out RaycastHit hitResult, range, occlusionMask) ? transform.worldToLocalMatrix * (hitResult.point - transform.position) : direction * range;
        }
    }

    private Vector3 GetDirectionFromIndex(int index)
    {
        float angleX = index % (horizontalResolution + 1) * horizontalAnglePer - horizontalFov * 0.5f;
        float angleY = index / (horizontalResolution + 1) * verticalAnglePer - verticalFov * 0.5f;
        return GetDirectionFromAngle(angleX, angleY);
    }

    private Vector3 GetDirectionFromAngle(float angleX, float angleY)
    {
        float angleXRad = angleX * Mathf.Deg2Rad;
        float angleYRad = angleY * Mathf.Deg2Rad;
        
        return new Vector3(Mathf.Sin(angleXRad), Mathf.Sin(angleYRad), Mathf.Cos(angleXRad) * Mathf.Cos(angleYRad));
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }
}
