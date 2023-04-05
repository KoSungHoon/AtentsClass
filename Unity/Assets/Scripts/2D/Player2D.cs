using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!myAnim.GetBool("isAttacking"))
        {
            Vector2 dir = Vector2.zero;
            dir.x = Input.GetAxisRaw("Horizontal"); //GetAxis�� ������ �پ ���� ���ݾ� ������ ������ �ִ� ���� �� ������ ���ٷ��� �ٷ� �ٷ� �ٲ��ְ� GetAxisRaw�� ����� �Ѵ�
            if (!Mathf.Approximately(dir.x, 0.0f))
            {
                myAnim.SetBool("isMoving", true);
                if (dir.x < 0.0f)
                {
                    myRenderer.flipX = true;
                }
                else
                {
                    myRenderer.flipX = false;
                }
            }
            else
            {
                myAnim.SetBool("isMoving", false);
            }
            transform.Translate(dir * MoveSpeed * Time.deltaTime);
        }
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("Attack");
            }
        

    }
}
