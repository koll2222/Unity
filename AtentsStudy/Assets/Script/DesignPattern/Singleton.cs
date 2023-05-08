using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    static T _inst = null;
    public static T Instance
    {
        get
        {
            if(_inst == null)
            {
                _inst = FindObjectOfType<T>();  // 이 함수의 T 타입은 오브젝트
                if(_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).ToString();
                    _inst = obj.AddComponent<T>();
                }
            }
            return _inst;
        }
    }

    protected void Initialize()
    {
        if(_inst == null)
        {
            _inst = this as T;
        }
        else
        {
            Destroy(this);
        }
    }
}
