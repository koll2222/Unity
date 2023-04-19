using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    public TMPro.TMP_Text myTitle;
    public TMPro.TMP_Text myContent;

    public void Initialize(string title, string content)
    {
        myTitle.text = title;
        myContent.text = content;
    }

    public void OnClose()
    {
        PopUpManager.Inst.ClosePopup(this);
        Destroy(gameObject);
    }

}
