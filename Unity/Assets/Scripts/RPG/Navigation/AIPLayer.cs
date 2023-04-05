using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPLayer : AIMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMove(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            switch (path.status)
            {
                case NavMeshPathStatus.PathPartial: //중간에 막히는 경우
                    break;
                case NavMeshPathStatus.PathInvalid: //아예 움직임이 불가능할 경우
                    break;
                case NavMeshPathStatus.PathComplete: 
                    break;

            }
            MoveByPath(path.corners);
        }//path를 얻어서 이동시키기 길찾기가 가능하면 true 불가능하면 false 내위치,이동할 위치,레이어 번호,패스 

    }
}
