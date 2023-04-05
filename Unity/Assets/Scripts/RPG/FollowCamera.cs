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
        
        Dir = transform.InverseTransformDirection(Dir);//회전값을 제거하는 함수 절대축 상황에서의 디렉션이 된다(Dir);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    Quaternion rotX=Quaternion.identity, rotY=Quaternion.identity;//x y 축을 기준으로 한 회전값
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

        Vector3 dir = rotY * rotX * Dir;  //X축을 먼져 움직이면 카메라가 이상하게 이동한다 Y축 회전이 무조건 먼져 해야한다
        float radius = 0.5f;
        if (Physics.Raycast(new Ray(myTarget.position, dir),out RaycastHit hit,Dist,crashMask))
        { //레이를 이용한 바닥뚫지 않게 하기
            //transform.position = hit.point + -dir * radius;
            Dist = hit.distance - radius;
        }
            transform.position= myTarget.position+dir*Dist;
        

      
        transform.LookAt(myTarget);
      
       
    }
}
