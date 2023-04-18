using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;

    public void SetMessage(string msg, string tag = "")
    {
        float width = GetComponent<RectTransform>().rect.width;
        string tmp = string.Empty;
        string res = string.Empty;
        for(int i = 0; i < msg.Length; i++)
        {
            GameUtils.Inst.strBuilder.Clear();
            GameUtils.Inst.strBuilder.Append(tmp);
            GameUtils.Inst.strBuilder.Append(msg[i]);
            Vector2 tmpSize = myLabel.GetPreferredValues(GameUtils.Inst.MergeChar(tmp, msg[i]));
            if(tmpSize.x > width)
            {
                tmp = GameUtils.Inst.MergeChar(tmp, '\n');
                res = GameUtils.Inst.MergeString(res, tmp);
                tmp = string.Empty;
            }
            tmp = GameUtils.Inst.MergeChar(tmp, msg[i]);
        }
        res = GameUtils.Inst.MergeString(res, tmp);
        GetComponent<RectTransform>().sizeDelta = myLabel.GetPreferredValues(res);
        myLabel.text = GameUtils.Inst.MergeString(tag, res);
    }
}
