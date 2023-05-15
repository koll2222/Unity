using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public MeshFilter myFilter;
    public Vector3[] vertexBuffer = new Vector3[4];
    public int[] indexBuffer = new int[6];
    public Vector2[] uvList = new Vector2[4];
    public Vector3[] normalList = new Vector3[4];
    // Start is called before the first frame update
    void Start()
    {
        //Vector3[] vertexBuffer = new Vector3[4];
        vertexBuffer[0] = new Vector3(-0.5f, 0.5f, 0);
        vertexBuffer[1] = new Vector3(-0.5f, -0.5f, 0);
        vertexBuffer[2] = new Vector3(0.5f, -0.5f, 0);
        vertexBuffer[3] = new Vector3(0.5f, 0.5f, 0);

        //int[] indexBuffer = new int[6];     // ���� �׸� �� ��ǻ�ʹ� �ﰢ���� ������ �ð�������� ��ġ�� �ȴٸ� �ո�, �ݽð�� �޸����� �ν�
        indexBuffer[0] = 0;
        indexBuffer[1] = 3;
        indexBuffer[2] = 2;
        indexBuffer[3] = 0;
        indexBuffer[4] = 2;
        indexBuffer[5] = 1;

        //Vector2[] uvList = new Vector2[4];
        uvList[0] = new Vector2(0, 1);
        uvList[1] = new Vector2(0, 0);
        uvList[2] = new Vector2(1, 0);
        uvList[3] = new Vector2(1, 1);

        for (int i = 0; i < indexBuffer.Length; i += 3)
        {
            normalList[indexBuffer[i]] = normalList[indexBuffer[i + 1]] = normalList[indexBuffer[i + 2]] =
                CalNormal(vertexBuffer[indexBuffer[i]], vertexBuffer[indexBuffer[i + 1]], vertexBuffer[indexBuffer[i + 2]]);
        }
    }

    Vector3 CalNormal(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 d1 = v2 - v1;
        Vector3 d2 = v3 - v1;
        return Vector3.Cross(d1, d2).normalized;
    }


    // Update is called once per frame
    void Update()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertexBuffer;   // vertex buffer
        mesh.triangles = indexBuffer;  // index buffer
        mesh.uv = uvList; // u => ����, v => ����, ���� �ϴ��� 0,0
        mesh.normals = normalList;
        myFilter.mesh = mesh;
    }
}
