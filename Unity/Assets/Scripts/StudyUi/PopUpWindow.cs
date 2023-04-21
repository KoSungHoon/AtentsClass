using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : MonoBehaviour
{
    public TMPro.TMP_Text myTitle;
    public TMPro.TMP_Text myContent;

    public void Initialize(string title,string Content)
    {
        myTitle.text = title;
        myContent.text = Content;
    }
   public void OnClose()
    {
        PopUpManager.Inst.closePopUp(this);
        Destroy(gameObject);
    }
}
