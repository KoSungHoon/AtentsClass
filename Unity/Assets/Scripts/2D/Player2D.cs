using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
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
            if (coJump != null) StopCoroutine(coJump);
            coJump=StartCoroutine(Jumping(1.0f,3.0f));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Droping());
        }

    }
    Coroutine coJump = null;
    bool isDown = false;
    IEnumerator Jumping(float totalTime,float maxHeight) //삼각함수를 이용한 점프
    {
        isDown = false;
        myAnim.SetTrigger("Jump");
        float t=0.0f;//현제 시간
        float orgHeight = transform.position.y; //현제 위치값
        while (t <= totalTime)
        {
            
            if(t>totalTime*0.5f)isDown = true;
            t += Time.deltaTime;
            // T:totalTime=y:PI -> t/tot=y/Pi ->= Pi*t/tot
            float h = Mathf.Sin((t/totalTime)*Mathf.PI)*maxHeight;

            Vector3 pos = new Vector3(transform.position.x, orgHeight + h, transform.position.z);

            if (isDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Vector3.Distance(transform.position, pos), crashMask);

                if (hit.collider != null)
                {
                    transform.position = hit.point;
                    yield break;
                }
            }
            transform.position =pos;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x,orgHeight,transform.position.z);
    }
    float dropDist = 0.0f;
    IEnumerator Droping()
    {
        dropDist =1.0f;
        
        yield return null;

    }
    public LayerMask crashMask;
    void AirCheck()
    {
        Vector2 orgPos = transform.position+Vector3.up*0.05f;
        Vector2 dir = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.1f, crashMask);
        if ( hit.collider != null&& dropDist<=0.0f)
        {
            if (isDown)
            {
                if (coJump != null) StopCoroutine(coJump);
                transform.position = hit.point;
            }
            
            myAnim.SetBool("isAir", false);
            
            
        }
        else
        {
            myAnim.SetBool("isAir", true);
            float delta =9.8f * Time.deltaTime;
            transform.position +=Vector3.down*delta;

            if (dropDist > 0.0f)
            {
                dropDist -= delta;
            }
        }
    }

    

}
