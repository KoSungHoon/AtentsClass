using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (myAnim.GetBool("isAir")) return;
        //StartCoroutine(Moving(pos));
        //myNav.SetDestination(pos);
        StopAllCoroutines();
        StartCoroutine(JumpableMoving(pos));
    }

    IEnumerator Moving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        myAnim.SetBool("isMoving", true);        
        while(myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            yield return null;
        }
        myAnim.SetBool("isMoving", false);
    }

    IEnumerator JumpableMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            if(myNav.isOnOffMeshLink)
            {
                myAnim.SetBool("isAir", true);
                myNav.isStopped = true;
                Vector3 endPos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();
                while(dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if(dist < delta)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                    yield return null;
                }
                myAnim.SetBool("isAir", false);                
                myNav.isStopped = false;
                myNav.CompleteOffMeshLink();
            }            

            yield return null;
        }
    }
}
