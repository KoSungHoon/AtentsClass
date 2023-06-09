using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatSystem : MonoBehaviour
{
    public Transform myContents;
    public TMPro.TMP_InputField myInput;
    public Scrollbar myScroll;
    // Start is called before the first frame update
    void Start()
    {
        
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
        (Instantiate(Resources.Load("ChatMessage")) as GameObject).GetComponent<ChatMessage>().SetMessage(msg);
        myInput.text = string.Empty;
        myInput.ActivateInputField();
        StartCoroutine(MakingZero());
    }
    IEnumerator MakingZero()
    {
        yield return new WaitForEndOfFrame();
        myScroll.value = 0.0f;
        yield return new WaitForEndOfFrame();
    }
}
