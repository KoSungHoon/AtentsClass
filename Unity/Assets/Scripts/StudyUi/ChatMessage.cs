using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;
    
    public void SetMessage(string msg, string tag = "")
    {
        float width = GetComponent<RectTransform>().rect.width;        
        string temp = string.Empty;
        string res = string.Empty;
        for(int i = 0; i < msg.Length; ++i)
        {
            GameUtils.Inst.strBuilder.Clear();
            GameUtils.Inst.strBuilder.Append(temp);
            GameUtils.Inst.strBuilder.Append(msg[i]);
            Vector2 tempSize = myLabel.GetPreferredValues(GameUtils.Inst.MergeChar(temp, msg[i]));
            if(tempSize.x > width)
            {                
                temp = GameUtils.Inst.MergeChar(temp, '\n');
                res = GameUtils.Inst.MergeString(res, temp);
                temp = string.Empty;
            }
            temp = GameUtils.Inst.MergeChar(temp, msg[i]);
        }
        res = GameUtils.Inst.MergeString(res, temp);
        GetComponent<RectTransform>().sizeDelta = myLabel.GetPreferredValues(res);
        myLabel.text = GameUtils.Inst.MergeString(tag, res);
    }
}
