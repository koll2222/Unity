using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ChatSystem : MonoBehaviour
{
    public enum ChatType
    {
        ��ü, �Ϲ�, �Ӹ�, ��Ƽ, ���
    }
    public Transform myContents;
    public TMP_InputField myInput;
    public Scrollbar myScroll;
    public TMPro.TMP_Dropdown myMenu;
    // Start is called before the first frame update
    void Start()
    {
        
        myMenu.value = -1;
        int count = System.Enum.GetValues(typeof(ChatType)).Length;
        for(int i = 0; i < count; i++)
        {
            GameUtils.Inst.strBuilder.Clear();
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            //data.text = GetTypeColor((ChatType)i) + ((ChatType)i).ToString();
            GameUtils.Inst.strBuilder.Append(GetTypeColor((ChatType)i));
            GameUtils.Inst.strBuilder.Append((ChatType)i).ToString();
            data.text = GameUtils.Inst.strBuilder.ToString();
            myMenu.options.Add(data);
        }
        myMenu.value = 0;
    }

    string GetTypeColor(ChatType type)
    {
        string tmp = string.Empty;

        switch (type)
        {
            case ChatType.��ü:
                tmp = "<#ffff00>";
                break;
            case ChatType.�Ϲ�:
                tmp = "<#ffffff>";
                break;
            case ChatType.�Ӹ�:
                tmp = "<#ff00ff>";
                break;
            case ChatType.��Ƽ:
                tmp = "<#0000ff>";
                break;
            case ChatType.���:
                tmp = "<#00ff00>";
                break;

        }

        return tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChat(string msg)
    {
        if(msg == string.Empty)
        {
            myInput.DeactivateInputField();
            return;
        }
        (Instantiate(Resources.Load("RPG/ChatMessage"), myContents) as GameObject)
            .GetComponent<ChatMessage>().SetMessage(msg, GetTypeColor((ChatType)myMenu.value));
        myInput.text = string.Empty;
        myInput.ActivateInputField();
        StartCoroutine(MakingZero());
    }

    IEnumerator MakingZero()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        myScroll.value = 0;
    }
    
}
