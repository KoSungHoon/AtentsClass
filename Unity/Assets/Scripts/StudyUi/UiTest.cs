using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public void MoveBox(float v)
    {
        transform.position = orgPos + Vector3.right * v;
    }

    Vector3 orgPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            PopUpManager.Inst.CreatePopup("테스트", "내용 없음");
        }
    }
}
