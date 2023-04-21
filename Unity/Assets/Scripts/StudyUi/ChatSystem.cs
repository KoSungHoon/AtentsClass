using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class ChatSystem : MonoBehaviour
    {
    public enum ChatType
    {
        전체,일반,귓말,파티,길드
    }
    
        public Transform myContents;
        public TMP_InputField myInput;
        public Scrollbar myScroll;
        public TMP_Dropdown myMenu;
        // Start is called before the first frame update
        void Start()
        { 
        myMenu.value = -1;
        int cout = System.Enum.GetValues(typeof(ChatType)).Length;
        for(int i = 0; i < cout; i++)
        {
            GameUtils.Inst.strBuilder.Clear();  
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            //data.text = GetTypeColor((ChatType)i) +((ChatType)i).ToString();//C#에서는 문자열 끼리 더할때 가비지가 발생해 권장하지 않는다 
            GameUtils.Inst.strBuilder.Append(GetTypeColor((ChatType)i));
            GameUtils.Inst.strBuilder.Append(((ChatType)i).ToString());
            data.text = GameUtils.Inst.strBuilder.ToString();
            myMenu.options.Add(data);
        }
        myMenu.value = 0;
        }
    string GetTypeColor(ChatType type)
    {
        string temp = string.Empty;
        switch (type)
        {
            case ChatType.전체:
                temp = "<#ffff00>";
                break;
            case ChatType.일반:
                temp = "<#ffffff>";
                break;
            case ChatType.귓말:
                temp = "<#ff00ff>";
                break;
            case ChatType.파티:
                temp = "<#0000ff>";
                break;
            case ChatType.길드:
                temp = "<#00ff00>";
                break;

        }
        return temp;
    }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddChat(string msg)
        {
            if (msg == string.Empty)
            {
                myInput.DeactivateInputField();
                return;
            }
            (Instantiate(Resources.Load("chatMessage"), myContents) as GameObject).GetComponent<ChatMessage>().SetMessage(msg,GetTypeColor((ChatType)myMenu.value));
            myInput.text = string.Empty;
            myInput.ActivateInputField();
            StartCoroutine(MakingZero());
        }

        IEnumerator MakingZero()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            myScroll.value = 0.0f;
        }
    }
