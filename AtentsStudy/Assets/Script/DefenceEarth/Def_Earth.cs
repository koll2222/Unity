using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_Earth : MonoBehaviour
{
    // static 변수들은 데이터 영역에 생성이 됨
    public static Def_Earth Instance = null;
    public Transform myEarth = null;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //지구를 기준으로 원형에 생성
    IEnumerator Spawning()
    {
        {
            /*
            while (myEarth != null)
            {
                Vector3 pos = Vector3.zero;
                while (Mathf.Approximately(pos.magnitude, 0.0f))
                {
                    pos.x = Random.Range(-1.0f, 1.0f);
                    pos.y = Random.Range(-1.0f, 1.0f);
                }
                Vector3 rndDir = (pos - myEarth.position).normalized;
                pos = myEarth.position + rndDir * 8.0f;
                GameObject obj = Instantiate(Resources.Load("DefenceEarth/Def_Meteor"), pos, Quaternion.identity) as GameObject;
                yield return new WaitForSeconds(0.1f);
            }*/
        }
        while (myEarth != null)
        {
            Vector3 pos = Vector3.zero;
            Vector3 rndDir = new Vector3(0, 1, 0);
            float angle = Random.Range(0.0f, 360.0f);
            //방향 벡터에 쿼터니언값을 곱하면 방향 벡터가 회전하게 됨.
            rndDir = Quaternion.Euler(0, 0, angle) * rndDir;

            pos = myEarth.position + rndDir * 8.0f;
            GameObject obj = Instantiate(Resources.Load("DefenceEarth/Def_Meteor"), pos, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
