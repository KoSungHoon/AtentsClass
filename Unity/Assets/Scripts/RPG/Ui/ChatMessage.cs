using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;
   public void SetMessage(string msg)
    {
        float width = GetComponent<RectTransform>().rect.width;
        myLabel.text = msg;
        GetComponent<RectTransform>().sizeDelta=myLabel.GetPreferredValues();
        string temp =string.Empty;
        for(int i = 0; i < msg.Length; i++)
        {
            Vector2 tempSize = myLabel.GetPreferredValues(temp + msg[i]);
            if (tempSize.x > width)
            {
                temp += '\n';
                temp = string.Empty;
            }
                temp += msg[i];
            
        }
    }
}
