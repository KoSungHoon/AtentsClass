using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterMovement2D ,IBattle
{
    public LayerMask enemyMask;
    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Deamage");
    }
    public bool IsLive
    {
        get
        {
            return true;
        }
    }
    public void OnAttack()
    {
       Collider2D[] list=Physics2D.OverlapCircleAll(transform.position+Forward()*0.5f,0.5f,enemyMask);
        foreach(Collider2D col in list)
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        AirCheck();
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
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("isAir"))
        {
            //myRigid2D.AddForce(Vector2.up*500.0f); //물리를 이용한 점프 
            Jump(1.0f,3.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Drop();
        }

    }
   
    
}
