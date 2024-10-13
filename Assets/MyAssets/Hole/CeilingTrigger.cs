using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingTrigger : MonoBehaviour
{
    [SerializeField] float _waitTime;
    Collider2D _collider;
    float _currWaitTime;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        MyGameManager.HoleFallEvent += WaitTrigger;
    }
    private void WaitTrigger(Vector3 VecP)
    {
        _currWaitTime = _waitTime;
    }
    void Update() {
        if (_currWaitTime > 0)
        {
            _collider.enabled = false;
            _currWaitTime -= Time.deltaTime;
        } else
        {
            _collider.enabled = true;
        }
        //print(_currWaitTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerMotor>())
        {
            MyGameManager.Instance.HoleClimbEventTrigger();
        }
    }
}
