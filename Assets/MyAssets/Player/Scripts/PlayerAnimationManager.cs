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

    [SerializeField] MyInputManager _inputMan;

    void Start()
    {
        _playerControls = GetComponent<PlayerMotor>();
        var objAnimation = transform.GetChild(0);
        _anim = objAnimation.GetComponent<Animator>();
        _sprite = objAnimation.GetComponent<SpriteRenderer>();
        _ungroundedScript = objAnimation.GetComponent<AnimFloatEvent>();
        _inputMan = _inputMan ?? FindObjectOfType<MyInputManager>();
    }

    void Update()
    {
        // Inputs
        bool _inptMoveRight = _inputMan.MoveRightButton;
        bool _inptMoveLeft = _inputMan.MoveLeftButton;
        bool _inptMoveUp = _inputMan.MoveUpButton;
        bool _inptMoveDown = _inputMan.MoveDownButton;
        bool _inptInteract = _inputMan.InteractButton;
        var _axisX = 0;
        var _axisY = 0;
        if (_inptMoveUp) { _axisY = 1; } else if (_inptMoveDown) { _axisY = -1; } else { _axisY = 0; }
        if (_inptMoveRight) { _axisX = 1; } else if (_inptMoveLeft) { _axisX = -1; } else { _axisX = 0; }

        //var _axisX = _playerControls.GetAxisX();
        //var _axisY = _playerControls.GetAxisY();
        //var _actionInput = _playerControls.GetActionInput();

        //Debug.Log("AxisX: " + _axisX + " AxisY: " + _axisY + " AcInput: " + _inptInteract + " ifGrounded: ");
        //Debug.Log(""+ _ungroundedScript.isFloating);
        if (_inptInteract)
        {
            _anim.SetBool("IsActing", true);
        }
        else
        {
            _anim.SetBool("IsActing", false);
        }

        if (_inptInteract && _ungroundedScript.isFloating == false && _anim.GetBool("IsMoving"))
        {
            _anim.SetBool("IsRunning", true);
        }

        if (!_inptInteract && _anim.GetBool("IsRunning"))
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
