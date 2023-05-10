using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 스크립터블 오브젝트는 아이템의 원형만을 가지고 있는 게 좋음
 * 예) 등급을 가지고 있다면 같은 무기라도 다른 등급의 무기 파일을 다 생성해 줘야 하기에 등급은 가지고 있지 않는 게 좋다.
 */
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Item", order = -1)]
public class ItemDataObject : ScriptableObject
{
    public SkillDataObject mySkill;
    public enum GRADE
    {
        Normal, Magic, Unique, Rare, Legend, Epic, Myth
    }
    [field:SerializeField] public string Name
    {
        get; private set;
    }
    [field:SerializeField] public float[] Power
    {
        get;private set;
    }
    public float GetPower(int lv, GRADE grade)
    {
        float tmp = Power[lv];
        switch (grade)
        {
            case GRADE.Normal:
                break;
            case GRADE.Magic:
                tmp *= 1.1f;
                break;
            case GRADE.Unique:
                tmp *= 1.2f;
                break;
            case GRADE.Rare:
                tmp *= 1.3f;
                break;
            case GRADE.Legend:
                tmp *= 1.4f;
                break;
            case GRADE.Epic:
                tmp *= 1.5f;
                break;
            case GRADE.Myth:
                tmp *= 1.6f;
                break;
        }return tmp;
    }
    [field:SerializeField] public float Price
    {
        get; private set;
    }
}
