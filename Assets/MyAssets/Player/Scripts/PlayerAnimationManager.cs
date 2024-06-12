using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _sprite;
    private PlayerMotor _playerControls;
    // Es el script que maneja los eventos de animacion cuando flota
    private AnimFloatEvent _ungroundedScript;

    void Start()
    {
        _playerControls = GetComponent<PlayerMotor>();
        var objAnimation = transform.GetChild(0);
        _anim = objAnimation.GetComponent<Animator>();
        _sprite = objAnimation.GetComponent<SpriteRenderer>();
        _ungroundedScript = objAnimation.GetComponent<AnimFloatEvent>();
    }

    void Update()
    {
        var _axisX = _playerControls.GetAxisX();
        var _axisY = _playerControls.GetAxisY();
        var _actionInput = _playerControls.GetActionInput();

        Debug.Log("AxisX: " + _axisX + " AxisY: " + _axisY + " AcInput: " + _actionInput + " ifGrounded: ");
        //Debug.Log(""+ _ungroundedScript.isFloating);
        if (_actionInput)
        {
            _anim.SetBool("IsActing", true);
        }
        else
        {
            _anim.SetBool("IsActing", false);
        }

        if (_actionInput && _ungroundedScript.isFloating == false && _anim.GetBool("IsMoving"))
        {
            _anim.SetBool("IsRunning", true);
        }

        if (!_actionInput && _anim.GetBool("IsRunning"))
        {
            _anim.SetBool("IsRunning", false);
        }

        if (Mathf.Abs(_axisX) > 0.1 || Mathf.Abs(_axisY) > 0.1)
        {
            _anim.SetBool("IsMoving", true);
        }
        else
        {
            _anim.SetBool("IsMoving", false);
        }
        if (_axisX > 0)
        {
            _sprite.flipX = true;
        }
        else if (_axisX < 0)
        {
            _sprite.flipX = false;
        }
    }
}
