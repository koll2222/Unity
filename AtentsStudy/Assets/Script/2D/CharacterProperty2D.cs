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
}
