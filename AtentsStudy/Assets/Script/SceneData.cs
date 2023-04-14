using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Canvas sceneCanvas = null;
    public Transform hpBars = null;
    public Transform miniMap = null;
    private void Awake()
    {
        Inst = this;
    }
}
