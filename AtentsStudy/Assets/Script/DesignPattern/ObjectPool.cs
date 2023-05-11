using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    Dictionary<string, Queue<GameObject>> myPool = new Dictionary<string, Queue<GameObject>>();
    public GameObject GetObject<T>(GameObject org, Vector3 pos, Quaternion rot = default, Transform parent = null)
    {
        if (myPool.ContainsKey(typeof(T).Name))
        {
            if (myPool[typeof(T).Name].Count > 0)
            {
                GameObject obj = myPool[typeof(T).Name].Dequeue();  // ²¨³»±â
                obj.SetActive(true);
                obj.transform.SetParent(parent);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                return obj;
            }
        }
        else
        {
            myPool[typeof(T).Name] = new Queue<GameObject>();
        }
        return Instantiate(org, pos, rot, parent);
    }

    public void ReleaseObject<T>(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        myPool[typeof(T).Name].Enqueue(obj);
    }
}
