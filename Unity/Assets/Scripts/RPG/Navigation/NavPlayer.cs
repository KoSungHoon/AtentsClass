using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //네비게이션 쓰기 위한 지시문

public class NavPlayer : CharacterProperty
{
    public NavMeshAgent myNav;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        myAnim.SetFloat("Speed", myNav.velocity.magnitude/myNav.speed);
    }
    public void OnWarp(Vector3 pos)
    {
        myNav.Warp(pos);    
    }
    public void OnMove(Vector3 pos)
    {
        if(myAnim.GetBool("isAir")) return;
        StopAllCoroutines();
        myNav.SetDestination(pos);
        StartCoroutine(JumpableMoving(pos));
    }
    IEnumerator Moving(Vector3 pos)
    {
        myNav.SetDestination(pos);//해당 위치까지 자동으로 이동 
        //myAnim.SetBool("isMoving", true);

        while (myNav.remainingDistance > myNav.stoppingDistance || myNav.pathPending)// 현제 거리 >남아있는거리,계산중일땐 true 계산이 끝나면 false
        {
            yield return null;
        }
        //myAnim.SetBool("isMoving", false);
    }
    IEnumerator JumpableMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.remainingDistance > myNav.stoppingDistance || myNav.pathPending)// 현제 거리 >남아있는거리,계산중일땐 true 계산이 끝나면 false
        {
            if (myNav.isOnOffMeshLink) // 오프 매쉬 링크의 영역에 들어오면 true 
            {
                myAnim.SetBool("isAir", true);
                myNav.isStopped = true;
                Vector3 endPos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();
                while (dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if (dist < delta)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                    yield return null;
                }
                myAnim.SetBool("isAir", false);
                myNav.CompleteOffMeshLink();
                myNav.isStopped = false;
                yield return null;
                myNav.velocity = dir * myNav.speed;
            }
            yield return null;

        }

    }
}
