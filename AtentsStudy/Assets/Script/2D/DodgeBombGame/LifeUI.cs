using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public GameObject[] heartList;

    public void SetLife(int n)
    {
        for(int i = 0; i < heartList.Length; i++)
        {
            if(i < n)
            {
                heartList[i].SetActive(true);
            }
            else
            {
                heartList[i].SetActive(false);
            }
        }
    }
}
