using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform myTarget;
    public LayerMask crashMask;
    Vector3 Dir=Vector3.zero;
    float Dist = 0.0f;
    float targetDist = 0.0f;
    public float RotSpeed = 180.0f;
    public float ZoomSpeed = 5.0f;
    private void Awake()
    {
        transform.LookAt(myTarget);
        rotX = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        Dir = transform.position - myTarget.position;
       targetDist=Dist=Dir.magnitude;
        Dir.Normalize();
        
        Dir = transform.InverseTransformDirection(Dir);//ȸ������ �����ϴ� �Լ� ������ ��Ȳ������ �𷺼��� �ȴ�(Dir);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    Quaternion rotX=Quaternion.identity, rotY=Quaternion.identity;//x y ���� �������� �� ȸ����
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {
            float x = Input.GetAxis("Mouse Y")*RotSpeed;
            float y = -Input.GetAxis("Mouse X")*RotSpeed;
            rotX *= Quaternion.Euler(x, 0, 0);
            rotY *= Quaternion.Euler(0, y, 0);

            //Quaternion rot = Quaternion.Euler(0,y,0)*Quaternion.Euler(x, 0, 0);
            //Dir = rot * Dir;
            //transform.LookAt(myTarget);
            //transform.forward = -Dir;
            //transform.rotation = Quaternion.LookRotation(-Dir);
            float angle = rotX.eulerAngles.x;
            if (angle > 180.0f)
            {
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, -60.0f, 80.0f);
            rotX = Quaternion.Euler(angle, 0, 0);
        }
        targetDist +=-Input.GetAxis("Mouse ScrollWheel")*ZoomSpeed;
        targetDist = Mathf.Clamp(targetDist , 1.0f, 10.0f);

        Dist = Mathf.Lerp(Dist, targetDist, Time.deltaTime * 3.0f);

        Vector3 dir = rotY * rotX * Dir;  //X���� ���� �����̸� ī�޶� �̻��ϰ� �̵��Ѵ� Y�� ȸ���� ������ ���� �ؾ��Ѵ�
        float radius = 0.5f;
        if (Physics.Raycast(new Ray(myTarget.position, dir),out RaycastHit hit,Dist,crashMask))
        { //���̸� �̿��� �ٴڶ��� �ʰ� �ϱ�
            //transform.position = hit.point + -dir * radius;
            Dist = hit.distance - radius;
        }
            transform.position= myTarget.position+dir*Dist;
        

      
        transform.LookAt(myTarget);
      
       
    }
}
