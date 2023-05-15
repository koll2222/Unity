using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public MeshFilter myFilter;
    [SerializeField] float ViewAngle = 90f;
    [SerializeField] float ViewDistance = 5f;
    [SerializeField] int DetailCount = 100;
    public LayerMask crashMask;

    List<Vector3> dirList = new List<Vector3>();

    Vector3[] vertexBuffer;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = Quaternion.Euler(0, -ViewAngle * 0.5f, 0) * new Vector3(0, 0, 1);
        dirList.Add(dir);

        float gapAngle = ViewAngle / (float)(DetailCount - 1);
        for(int i = 1; i < DetailCount; i++)
        {
            dir = Quaternion.Euler(0, gapAngle, 0) * dir;
            dirList.Add(dir);
        }
        
        vertexBuffer = new Vector3[DetailCount + 1];
        vertexBuffer[0] = Vector3.zero;
        for(int i = 0; i < DetailCount; i++)
        {
            vertexBuffer[i + 1] = vertexBuffer[0] + dirList[i] * ViewDistance;
        }

        int[] indexBuffer = new int[(DetailCount - 1) * 3];

        for(int i = 0, v = 1; i < indexBuffer.Length; i += 3, v++)
        {
            indexBuffer[i] = 0;
            indexBuffer[i + 1] = v;
            indexBuffer[i + 2] = v + 1;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuffer;
        mesh.triangles = indexBuffer;
        myFilter.mesh = mesh;

    }

    private void FixedUpdate()
    {
        for(int i = 0; i < dirList.Count; i++)
        {
            Ray ray = new Ray(transform.position, transform.rotation * dirList[i]);
            float dist = ViewDistance;
            if(Physics.Raycast(ray, out RaycastHit hit, ViewDistance, crashMask))
            {
                dist = hit.distance;
            }
            vertexBuffer[i + 1] = vertexBuffer[0] + dirList[i] * dist;
        }
        myFilter.mesh.vertices = vertexBuffer;
    }
}
