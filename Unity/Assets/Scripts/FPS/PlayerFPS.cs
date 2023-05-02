using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPS : CharacterProperty
{
    Vector2 InputData = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputData.x = Input.GetAxis("Horizontal");
        InputData.y = Input.GetAxis("Vertical");

        myAnim.SetFloat("X", InputData.x);
        myAnim.SetFloat("Y", InputData.y);
    }
}
