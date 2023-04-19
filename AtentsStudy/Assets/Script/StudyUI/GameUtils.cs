using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameUtils : MonoBehaviour
{
    static GameUtils _inst = null;
    public static GameUtils Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = FindObjectOfType<GameUtils>();
                if(_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameUtils";
                    _inst = obj.AddComponent<GameUtils>();
                }
            }
            return _inst;
        }
    }

    public StringBuilder strBuilder = null;

    private void Awake()
    {
        GameUtils ins = FindAnyObjectByType<GameUtils>();
        if(ins != this)
        {
            Destroy(this);
            return;
        }

        strBuilder = new StringBuilder();
    }

    public string MergeChar(string a, char b)
    {
        strBuilder.Clear();
        strBuilder.Append(a);
        strBuilder.Append(b);
        return strBuilder.ToString();
    }

    public string MergeString(string a, string b)
    {
        strBuilder.Clear();
        strBuilder.Append(a);
        strBuilder.Append(b);
        return strBuilder.ToString();
    }
}
