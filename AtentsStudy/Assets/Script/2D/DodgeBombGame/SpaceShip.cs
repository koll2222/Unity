using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public Vector2 MoveArea;    // x는 최소값 y는 최대값으로 활용
    float myDir = 0.0f;
    public float moveSpeed = 1.0f;
    public float Delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        switch(Random.Range(0, 2))
        {
            case 0:
                myDir = -1.0f;
                break;
            case 1:
                myDir = 1.0f;
                break;
        }
        StartCoroutine(Dropping(Delay));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * myDir * moveSpeed * Time.deltaTime);
        if(transform.position.x <= MoveArea.x)
        {
            myDir *= -1.0f;
            transform.position = new Vector2(MoveArea.x, transform.position.y);
        }
        if(transform.position.x >= MoveArea.y)
        {
            myDir *= -1.0f;
            transform.position = new Vector2(MoveArea.y, transform.position.y);
        }
    }

    IEnumerator Dropping(float Delay)
    {
        while (true)
        {
            GameObject obj = Instantiate(Resources.Load("Item"), transform.position, Quaternion.identity) as GameObject;
            int count = System.Enum.GetValues(typeof(DodgeItem.Type)).Length;   // enum의 갯수를 가져옴
            obj.GetComponent<DodgeItem>().SetType((DodgeItem.Type)Random.Range(0, count -1));

            yield return new WaitForSeconds(Delay);
        }
    }
}
