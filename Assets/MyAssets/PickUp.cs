using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] int _type;
    [SerializeField] Sprite[] _myVisuals;

    private void Start()
    {
        SpriteRenderer _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _myVisuals[_type];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerStats _stats;
        if (_stats = col.transform.GetComponent<PlayerStats>())
        {
            _stats = col.transform.GetComponent<PlayerStats>();
            /*
            switch (_type) {
                case 0:
                    _stats.AddCandy();
                break;
                case 1:
                    _stats.AddPhonePiece();
                break;
            }*/
            if (_type == 0)
            {
                _stats.AddCandy();
            } else if (_type >= 1)
            {
                _stats.AddPhonePiece();
            }
            Destroy(gameObject);
        }
    }
}
