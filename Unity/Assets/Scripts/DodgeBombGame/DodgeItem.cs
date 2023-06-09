using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeItem : CharacterProperty2D
{
    public enum Type
    {
        Bomb, Score, None
    }
    public Type myType = Type.None;
    public LayerMask crashMask;
    public float DropSpeed = 3.0f;
    public Sprite[] imgList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetType(Type type)
    {
        myType = type;
        myRenderer.sprite = imgList[(int)myType];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * DropSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(((1 << collision.gameObject.layer) & crashMask) != 0)
        {
            Destroy(gameObject);

            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                switch (myType)
                {
                    case Type.Bomb:
                        DodgeBomb.Inst.Life--;
                        break;
                    case Type.Score:
                        DodgeBomb.Inst.Score += 100;
                        break;
                }
            }
        }
    }
}
