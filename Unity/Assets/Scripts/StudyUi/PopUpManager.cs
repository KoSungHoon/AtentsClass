using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PopUpManager : MonoBehaviour
{
    public GameObject myNoTouch;
    public Stack<PopUpWindow> popupList = new Stack<PopUpWindow>();
    public UnityAction allColse = null;
    public static PopUpManager Inst
    {
        get;private set;
    }
    public void CreatePopUp(string title,string content)
    {
        myNoTouch.SetActive(true);
        myNoTouch.transform.SetAsLastSibling();
        PopUpWindow scp = (Instantiate(Resources.Load("PopUp"),transform) as GameObject).GetComponent<PopUpWindow>();
        scp.Initialize(title, content);
        allColse += scp.OnClose;
        popupList.Push(scp);
    }
    public void closePopUp(PopUpWindow pw)
    {
        allColse -= pw.OnClose;
        popupList.Pop();
        if (popupList.Count == 0)
        {
            myNoTouch.SetActive(false);

        }
        else
        {
            myNoTouch.transform.SetSiblingIndex(myNoTouch.transform.GetSiblingIndex() - 1);
        }
    }
    private void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //while (popupList.Count > 0)
            //{
            //    popupList.Peek().OnClose(); 
            //}//스택 사용
            allColse?.Invoke(); //딜리게이트 사용
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&popupList.Count>0)
        {
            popupList.Peek().OnClose();
        }
    }
}
