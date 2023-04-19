using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpManager : MonoBehaviour
{
    public GameObject myNotouch;
    public Stack<PopUpWindow> popupList = new Stack<PopUpWindow>();
    public UnityAction allClose = null;
    public static PopUpManager Inst
    {
        get; private set;
    }
    private void Awake()
    {
        Inst = this;
    }
    public void CreatePopUp(string title, string content)
    {
        myNotouch.SetActive(true);
        myNotouch.transform.SetAsLastSibling();
        PopUpWindow scp = (Instantiate(Resources.Load("PopUp"), transform) as GameObject).GetComponent<PopUpWindow>();
        scp.Initialize(title, content);
        allClose += scp.OnClose;    // delegate�� �Լ� ������ ����
        popupList.Push(scp);
    }

    public void ClosePopup(PopUpWindow pw)
    {
        allClose -= pw.OnClose;
        popupList.Pop();
        if(popupList.Count == 0)
        {
            myNotouch.SetActive(false);
        }
        else
        {
            myNotouch.transform.SetSiblingIndex(myNotouch.transform.GetSiblingIndex() - 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Stack ������ for���� �ƴ� while�� Ȱ���ϵ���..
            //while(popupList.Count > 0)
            //{
            //    // Stack Peek : ������ ���� ����
            //    popupList.Peek().OnClose();
            //}
            allClose?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && popupList.Count > 0)
        {
            popupList.Peek().OnClose();
        }
    }
}
