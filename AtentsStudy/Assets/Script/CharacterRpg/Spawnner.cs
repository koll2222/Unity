using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public GameObject orgObject;
    public float Width = 5.0f;
    public float Height = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Monster.TotalCount; i++)
        {
            Vector3 pos = transform.position;
            pos.x = Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z = Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<RPGProperty>().DeathAlarm += ReSpwan;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReSpwan()
    {
        StartCoroutine(ReSpwanning());
    }
    IEnumerator ReSpwanning()
    {
        yield return new WaitForSeconds(10.0f);

        if (Monster.TotalCount < 3)
        {
            Vector3 pos = transform.position;
            pos.x = Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z = Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<RPGProperty>().DeathAlarm += ReSpwan;
        }
    }
}
