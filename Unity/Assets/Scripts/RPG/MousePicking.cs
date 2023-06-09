using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MousePicking : MonoBehaviour
{
    public LayerMask pickMask;
    public LayerMask enemyMask;
    public UnityEvent<Vector3> clickAction = null;
    public UnityEvent<Vector3> rightClick = null;
    public UnityEvent<Transform> attackAction = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask | enemyMask))
            {
                if (((1 << hit.transform.gameObject.layer) & enemyMask) != 0)
                {
                    //����
                    attackAction?.Invoke(hit.transform);
                }
                else
                {
                    //�̵�
                    clickAction?.Invoke(hit.point);
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                rightClick?.Invoke(hit.point);
            }
        }
    }
}
