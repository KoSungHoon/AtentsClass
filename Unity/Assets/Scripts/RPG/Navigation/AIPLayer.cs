using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : AIMovement, IBattle
{
    public void OnDamage(float dmg)
    {
        curHp -= dmg;
    }
    public bool IsLive
    {
        get;
    }
    // Start is called before the first frame update
    void Start()
    {
        MinimapIcon icon =
            (Instantiate(Resources.Load("MinimapIcon"), SceneData.Inst.miniMap) as GameObject).GetComponent<MinimapIcon>();
        icon.Initialize(transform, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            OnDamage(10.0f);
        }
    }

    public void OnMove(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            switch(path.status)
            {
                case NavMeshPathStatus.PathPartial:
                    break;
                case NavMeshPathStatus.PathInvalid:
                    break;
                case NavMeshPathStatus.PathComplete:
                    break;
            }
            MoveByPath(path.corners);
        }
    }
}
