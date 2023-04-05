using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty2D : CharacterProperty
{
    SpriteRenderer _renderer = null;
    protected SpriteRenderer myRenderer
    {
        get
        {
            if (_renderer == null)
            {

                _renderer = GetComponent<SpriteRenderer>();
                if (_renderer == null)
                {
                    _renderer = GetComponentInChildren<SpriteRenderer>();
                }
            }
            return _renderer;
        }
    }
    Rigidbody2D _rigid2D = null;
    protected Rigidbody2D myRigid2D
    {
        get
        {
            if (_rigid2D == null)
            {
                _rigid2D = GetComponent<Rigidbody2D>(); //나한테 있다면 
                if (_rigid2D == null)
                {
                    _rigid2D = GetComponentInChildren<Rigidbody2D>();//나한텐 없고 자식 한테 있다면
                }
            }
            return _rigid2D;

        }
    }
}
