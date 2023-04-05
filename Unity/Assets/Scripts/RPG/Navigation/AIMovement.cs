using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : CharacterMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void MoveByPath(Vector3[] pathList)
    {
        StopAllCoroutines();
        StartCoroutine(MoivingByPath(pathList));
    }
    IEnumerator MoivingByPath(Vector3[] pathList)
    {
        //for(int i = 1; i < pathList.Length; i++)
        //{
        //    MoveToPos(pathList[i]);
        //    yield return null;
        //}
        int i = 1;
        myAnim.SetFloat("Speed",1.0f);
        while (i < pathList.Length)
        {
            bool done = false;
            MoveToPos(pathList[i],()=>done=true);
            while (!done)
            {
                for(int n = i; n < pathList.Length; ++n)
                {
                    Debug.DrawLine(n==i?transform.position:pathList[n-1],pathList[n],Color.red);//두 지점을 찍으면 Scene에서 선을 보여줌
                }
                yield return null;
            }
            ++i;
        }
        myAnim.SetFloat("Speed",0.0f);
    }
}
