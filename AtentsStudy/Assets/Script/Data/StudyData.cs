using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ITEM
{
    [SerializeField]
    int Data;
    //ItemDataObject Data;
    public ItemDataObject.GRADE grade;
    [SerializeField] int Level;
    public void Upgrade(ItemDataObject data)
    {
        if (Level == data.Power.Length) return;
        if(UnityEngine.Random.Range(0,100) > 90)
        {
            Level++;
        }
    }
}

[Serializable]
public struct PlayerData
{
    public string ID;
    public string Gold;
}

public class StudyData : MonoBehaviour
{
    //public ItemDataObject[] itemList;
    /* 위와 같은 방식으로 사용하면 안 됩니다~
     */

    public ItemDataObject[] orgDatas;
    public PlayerData myData;
    public PlayerData myLoadData;
    public string myText;
    public string myJson;
    public ITEM[] itemList;
    public ITEM[] loadItemList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //FileManager.SaveText($"{Application.dataPath}/test.text", "TEST!");
            //FileManager.SaveBinary($"{Application.dataPath}/test.dat", myData);
            FileManager.SaveBinaryArray($"{Application.dataPath}/testArray.dat", itemList);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //myText = FileManager.LoadText($"{Application.dataPath}/test.text");
            //myLoadData = FileManager.LoadBinary<PlayerData>($"{Application.dataPath}/test.dat");
            loadItemList = FileManager.LoadBinaryArray<ITEM>($"{Application.dataPath}/testArray.dat");
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            myJson = JsonUtility.ToJson(myData);
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            myLoadData = JsonUtility.FromJson<PlayerData>(myJson);
        }
    }
}
