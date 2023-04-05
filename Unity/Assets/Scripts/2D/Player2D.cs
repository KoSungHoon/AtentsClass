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
            dir.x = Input.GetAxisRaw("Horizontal"); //GetAxis는 가속이 붙어서 조금 조금씩 오르는 느낌이 있다 따라서 그 느낌을 안줄려면 바로 바로 바꿔주게 GetAxisRaw로 해줘야 한다
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
