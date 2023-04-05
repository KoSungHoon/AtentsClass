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
                case NavMeshPathStatus.PathPartial: //�߰��� ������ ���
                    break;
                case NavMeshPathStatus.PathInvalid: //�ƿ� �������� �Ұ����� ���
                    break;
                case NavMeshPathStatus.PathComplete: 
                    break;

            }
            MoveByPath(path.corners);
        }//path�� �� �̵���Ű�� ��ã�Ⱑ �����ϸ� true �Ұ����ϸ� false ����ġ,�̵��� ��ġ,���̾� ��ȣ,�н� 

    }
}
