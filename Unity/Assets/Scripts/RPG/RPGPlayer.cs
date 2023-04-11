using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGPlayer : CharacterMovement, IBattle
{
    Transform myTarget = null;

    public bool IsLive
    {
        get => !Mathf.Approximately(curHp, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Attack");
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetTrigger("Skill");
        }
    }
    
    public void OnMove(Vector3 pos)
    {
        if(IsLive) MoveToPos(pos);
    }

    public void OnDamage(float dmg)
    {
        curHp -= dmg;
        if (Mathf.Approximately(curHp, 0.0f))
        {
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach(Collider col in list)
            {
                col.enabled = false;
            }
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Dead");
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }

    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }

    public void BeginBattle(Transform target)
    {
        if (!IsLive) return;
        if(myTarget != null)
        {
            myTarget.GetComponent<CharacterProperty>().DeathAlarm -= TargetDead;
        }

        myTarget = target;
        FollowTarget(myTarget);
        myTarget.GetComponent<CharacterProperty>().DeathAlarm += TargetDead;
    }
    void TargetDead()
    {
        myTarget = null;
        StopAllCoroutines();
    }
}
