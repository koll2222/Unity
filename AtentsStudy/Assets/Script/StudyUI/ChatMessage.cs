using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;

    public void SetMessage(string msg)
    {
        float width = GetComponent<RectTransform>().rect.width;
        string tmp = string.Empty;
        string res = string.Empty;
        for(int i = 0; i < msg.Length; i++)
        {
            Vector2 tmpSize = myLabel.GetPreferredValues(tmp + msg[i]);
            if(tmpSize.x > width)
            {
                tmp += '\n';
                res += tmp;
                tmp = string.Empty;
            }
            tmp += msg[i];
        }
        res += tmp;
        GetComponent<RectTransform>().sizeDelta = myLabel.GetPreferredValues(res);
        myLabel.text = res;
    }
}
