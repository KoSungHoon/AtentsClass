using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //transform.Translate(transform.forward * Time.deltaTime, Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-transform.up * 360.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up * 360.0f * Time.deltaTime);
        }
        //S�� ������ �ڷ� �̵�, A�� ������ �������� ȸ��, D�� ���������� ȸ��
    }
}
