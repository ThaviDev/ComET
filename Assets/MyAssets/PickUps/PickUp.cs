using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //[SerializeField] int _type;
    //[SerializeField] Sprite[] _myVisuals;
    protected SpriteRenderer _sr;

    protected virtual void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        //_sr.sprite = _myVisuals[_type];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerStats _stats = col.transform.GetComponent<PlayerStats>();
        if (_stats != null)
        {
            /*
            switch (_type) {
                case 0:
                    _stats.AddCandy();
                break;
                case 1:
                    _stats.AddPhonePiece();
                break;
            }*/
            /*
            if (_type == 0)
            {
                _stats.AddCandy();
            } else if (_type >= 1)
            {
                _stats.AddPhonePiece();
            }
            */
            WasPickedUp(_stats);
            Destroy(gameObject);
        }
    }

    protected virtual void WasPickedUp(PlayerStats _pyr)
    {

    }
}
