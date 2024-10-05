using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeedOG;
    [SerializeField]
    private float _moveSpeedRun;
    private float _presentSpeed;
    private bool _isMoving;
    private Rigidbody2D rb;
    private AbilityMotor _abilMot;
    private AnimFloatEvent _ungroundedScript;
    /* Es el script que maneja los eventos de animacion cuando flota
    * Esta ubicado en los eventos de la animacion en el hijo que se encarga de la animacion
    * */
    [SerializeField] MyInputManager _inputMan;
    bool _frame1Ability; // 


    // Inputs
    bool _inptMoveRight;
    bool _inptMoveLeft;
    bool _inptMoveUp;
    bool _inptMoveDown;
    bool _inptInteract;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _ungroundedScript = transform.GetChild(0).GetComponent<AnimFloatEvent>();
        _abilMot = gameObject.GetComponent<AbilityMotor>();
    }
    void Update()
    {
        _inptMoveRight = _inputMan.MoveRightButton;
        _inptMoveLeft = _inputMan.MoveLeftButton;
        _inptMoveUp = _inputMan.MoveUpButton;
        _inptMoveDown = _inputMan.MoveDownButton;
        _inptInteract = _inputMan.InteractButton;

        // Interaccion de abilidad
        if (_ungroundedScript.isFloating && _frame1Ability == true)
        {
            _frame1Ability = false;
            _abilMot.ActivateAbility();
        }
        if (!_ungroundedScript.isFloating)
        {
            _frame1Ability = true;
        }

        //if (_inptInteract)
        //{
            //_abilMot.ActivateAbility();
            //print("Estoy Presionando Interact");
        //}
        //Logica de accion
        //_actionInput = Input.GetButton("Jump");
        //Logica de correr
        //Solo puede correr cuando se mueva y no este flotando
        if (_inptInteract && _ungroundedScript.isFloating == false && _isMoving)
        {
            _presentSpeed = _moveSpeedRun;
        }
        else
        {
            _presentSpeed = _moveSpeedOG;
        }
        //Deja de estar en el estado de correr cuando deja de presionar accion
        //PD: Puede estar en el estado de correr mientras este quieto,
        //esto es para que no empieze actuar mientras esta cambiando de direccion
    }

    void FixedUpdate()
    {
        //Axis de movimiento
        //_axisX = Input.GetAxisRaw("Horizontal");
        //_axisY = Input.GetAxisRaw("Vertical");
        var _axisX = 0;
        var _axisY = 0;
        if (_inptMoveUp) { _axisY = 1; } else if (_inptMoveDown) {  _axisY = -1; } else { _axisY = 0; }
        if (_inptMoveRight) { _axisX = 1; } else if (_inptMoveLeft) {  _axisX = -1; } else { _axisX = 0; }
        Vector3 mov = new Vector3(_axisX, _axisY, 0f);

        //Debug.Log(mov);

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, _presentSpeed * Time.deltaTime));
        
        if (Mathf.Abs(_axisX) > 0.1 || Mathf.Abs(_axisY) > 0.1) {
            _isMoving = true;
        }
        else {
            _isMoving = false;
        }
    }
}
