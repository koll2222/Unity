using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty2D : RPGProperty
{
    SpriteRenderer _renderer = null;
    protected SpriteRenderer myRenderer
    {
        get
        {
            if(_renderer == null)
            {
                _renderer = GetComponent<SpriteRenderer>();
                if(_renderer == null)
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
                _rigid2D = GetComponent<Rigidbody2D>();
                if (_rigid2D == null)
                {
                    _rigid2D = GetComponentInChildren<Rigidbody2D>();
                }
            }
            return _rigid2D;
        }
    }
}
