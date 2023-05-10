using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill", order = 0)]
public class SkillDataObject : ScriptableObject
{
    public enum TYPE
    {
        Passive, Active
    }
    public TYPE myType;
    [field:SerializeField] public string Content
    {
        get; private set;
    }

    public int Code;
    public float Value1;
    public float Value2;

}
