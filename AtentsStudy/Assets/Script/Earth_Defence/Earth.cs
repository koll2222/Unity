using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngineInternal;

public class Earth : MonoBehaviour
{
    public LayerMask enemyMask;
    public GameObject myEnemy = null;
    public GameObject DestroyMeteor = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatingMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, enemyMask))
            {
                DestroyObject(hit.transform.gameObject);
                DestroyEffect(hit.transform.gameObject);
                Debug.Log($"Level : {Level}  Score : {Score}");
            }
        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, enemyMask))
            {
                DestroyObject(hit.transform.gameObject);
                DestroyEffect(hit.transform.gameObject);
                Debug.Log($"Level : {Level}  Score : {Score}");
            }
        }
    }
    Vector3 newEnemyPos;
    // 五砺神 持失
    IEnumerator CreatingMeteor()
    {
        newEnemyPos.y = 0;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            float rndPosX = Random.Range(-50.0f, 50.0f);
            float rndPosZ = Random.Range(-50.0f, 50.0f);
            if (rndPosX > -10.0f && rndPosX < 10.0f)
            {
                rndPosX += 10.0f;
                rndPosX *= rndPosX;
            }
            if (rndPosZ > -10.0f && rndPosZ < 10.0f)
            {
                rndPosZ += 10.0f;
                rndPosZ *= rndPosZ;
            }
            newEnemyPos.x = rndPosX;
            newEnemyPos.z = rndPosZ;
            GameObject obj = Instantiate(myEnemy);
            obj.transform.position = newEnemyPos;


        }
    }
    void DestroyEffect(GameObject obj)
    {
        Instantiate(DestroyMeteor, obj.transform.position, Quaternion.identity);
    }

    void DestroyObject(GameObject obj)
    {
        Destroy(obj);
        GetScore();
        LevelCheck();
    }

    public int Score = 0;
    public int Level = 1;
    int lev_Check = 0;

    void LevelCheck()
    {
        if (lev_Check >= 10)
        {
            Level++;
            lev_Check -= 10;
            Meteor.MoveSpeed += 0.5f;
        }
    }

    void GetScore()
    {
        Score += 10;
        lev_Check += 1;
    }

}
