using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;
    
    public void SetMessage(string msg)
    {
        float width = GetComponent<RectTransform>().rect.width;        
        string temp = string.Empty;
        string res = string.Empty;
        for(int i = 0; i < msg.Length; ++i)
        {
            Vector2 tempSize = myLabel.GetPreferredValues(temp + msg[i]);
            if(tempSize.x > width)
            {
                temp += '\n';
                res += temp;
                temp = string.Empty;
            }
            temp += msg[i];
        }
        res += temp;
        GetComponent<RectTransform>().sizeDelta = myLabel.GetPreferredValues(res);
        myLabel.text = res;
    }
}
