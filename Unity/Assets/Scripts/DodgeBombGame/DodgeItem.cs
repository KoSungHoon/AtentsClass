using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeItem : CharacterProperty2D
{
    public enum Type
    {
       Bomb,Score,None
    }
    public Type myType = Type.None;
    public LayerMask crashMask;
    public float dropSpeed = 3.0f;
    public Sprite[] imgList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetType(Type Type)
    {
        myType = Type;
        myRenderer.sprite = imgList[(int)myType];
        //switch (myType)
        //{
        //    case Type.Bomb:
        //        break;
        //    case Type.Score:
        //        break;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime*dropSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1<<collision.gameObject.layer)&crashMask) != 0) //레이어 확인 
        {
            Destroy(gameObject); 
        }
    }//리지드바디가 있는 애들끼리 접촉
}
