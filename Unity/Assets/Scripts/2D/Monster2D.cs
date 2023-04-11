using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2D : CharacterMovement2D, IBattle
{
    public enum State
    {
        Create, Normal, Battle
    }

    public State myState = State.Create;

    public Transform myTarget = null;

    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }
    public bool IsLive
    {
        get
        {
            return true;
        }
    }
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case State.Create:
                break;
            case State.Normal:
                MoveByDirection(Forward(), ()=> TurnMove() );
                break;
            case State.Battle:
                StopAllCoroutines();
                Attack(myTarget);
                break;
        }
    }

    void TurnMove()
    {
        Turn();
        MoveByDirection(Forward(), TurnMove);
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Create:
                break;
            case State.Normal:
                break;
            case State.Battle:
                break;
        }
    }

    public void FindEnemy(Transform target)
    {
        myTarget = target;
        ChangeState(State.Battle);
    }

    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        AirCheck();
        StateProcess();
    }
}
