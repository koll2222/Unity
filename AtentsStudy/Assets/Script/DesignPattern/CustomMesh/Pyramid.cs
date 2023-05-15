using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    public bool CheckNormal, CheckUV = false;
    public MeshFilter myFilter;
    public Vector3[] vertexBuffer = new Vector3[16];
    public int[] indexBuffer = new int[18];
    public Vector2[] uvList = new Vector2[16];
    public Vector3[] normalList = new Vector3[16];

    Vector3 CalNormal(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 d1 = v2 - v1;
        Vector3 d2 = v3 - v1;
        return Vector3.Cross(d1, d2).normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        myFilter = GetComponent<MeshFilter>();
        vertexBuffer[0] = new Vector3(0f, 1f, 0f);
        vertexBuffer[1] = new Vector3(0.5f, 0f, -0.5f);
        vertexBuffer[2] = new Vector3(-0.5f, 0f, -0.5f);

        vertexBuffer[3] = new Vector3(0f, 1f, 0f);
        vertexBuffer[4] = new Vector3(0.5f, 0f, 0.5f);
        vertexBuffer[5] = new Vector3(0.5f, 0f, -0.5f);

        vertexBuffer[6] = new Vector3(0f, 1f, 0f);
        vertexBuffer[7] = new Vector3(-0.5f, 0f, 0.5f);
        vertexBuffer[8] = new Vector3(0.5f, 0f, 0.5f);

        vertexBuffer[9] = new Vector3(0f, 1f, 0f);
        vertexBuffer[10] = new Vector3(-0.5f, 0f, -0.5f);
        vertexBuffer[11] = new Vector3(-0.5f, 0f, 0.5f);

        vertexBuffer[12] = new Vector3(-0.5f, 0f, -0.5f);
        vertexBuffer[13] = new Vector3(0.5f, 0f, -0.5f);
        vertexBuffer[14] = new Vector3(0.5f, 0f, 0.5f);
        vertexBuffer[15] = new Vector3(-0.5f, 0f, 0.5f);

        indexBuffer[0] = 0;
        indexBuffer[1] = 1;
        indexBuffer[2] = 2;

        indexBuffer[3] = 3;
        indexBuffer[4] = 4;
        indexBuffer[5] = 5;

        indexBuffer[6] = 6;
        indexBuffer[7] = 7;
        indexBuffer[8] = 8;

        indexBuffer[9] = 9;
        indexBuffer[10] = 10;
        indexBuffer[11] = 11;

        // ¹Ù´Ú
        indexBuffer[12] = 12;
        indexBuffer[13] = 13;
        indexBuffer[14] = 14;

        indexBuffer[15] = 12;
        indexBuffer[16] = 14;
        indexBuffer[17] = 15;

        uvList[0] = new Vector2(0.5f, 0.5f);
        uvList[1] = new Vector2(1f, 0f);
        uvList[2] = new Vector2(0f, 0f);
        uvList[3] = new Vector2(0.5f, 0.5f);
        uvList[4] = new Vector2(1f, 1f);
        uvList[5] = new Vector2(1f, 0f);
        uvList[6] = new Vector2(0.5f, 0.5f);
        uvList[7] = new Vector2(0f, 1f);
        uvList[8] = new Vector2(1f, 1f);
        uvList[9] = new Vector2(0.5f, 0.5f);
        uvList[10] = new Vector2(0f, 0f);
        uvList[11] = new Vector2(0f, 1f);
        uvList[12] = new Vector2(0f, 1f);
        uvList[13] = new Vector2(1f, 1f);
        uvList[14] = new Vector2(1f, 0f);
        uvList[15] = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuffer;
        mesh.triangles = indexBuffer;


        if (CheckUV) mesh.uv = uvList;
        else mesh.uv = null;

        if (CheckNormal)
        {
            for (int i = 0; i < indexBuffer.Length; i += 3)
            {
                normalList[indexBuffer[i]] = normalList[indexBuffer[i + 1]] = normalList[indexBuffer[i + 2]] =
                    CalNormal(vertexBuffer[indexBuffer[i]], vertexBuffer[indexBuffer[i + 1]], vertexBuffer[indexBuffer[i + 2]]);
            }
            mesh.normals = normalList;
        }
        myFilter.mesh = mesh;
    }
}
