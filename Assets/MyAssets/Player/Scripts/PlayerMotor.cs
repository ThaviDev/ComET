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
    private float _axisX;// El valor X del jugador
    private float _axisY;// El valor Y del jugador
    private bool _actionInput;
    private bool _isMoving;
    private Rigidbody2D rb;
    private AbilityMotor _abilMot;
    [SerializeField] private AnimFloatEvent _ungroundedScript; // Es el script que maneja los eventos de animacion cuando flota

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _ungroundedScript = transform.GetChild(0).GetComponent<AnimFloatEvent>();
    }

    void Update()
    {
        //Logica de accion
        _actionInput = Input.GetButton("Jump");
        //Logica de correr
        //Solo puede correr cuando se mueva y no este flotando
        if (_actionInput && _ungroundedScript.isFloating == false && _isMoving)
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
        _axisX = Input.GetAxisRaw("Horizontal");
        _axisY = Input.GetAxisRaw("Vertical");
        Vector3 mov = new Vector3(_axisX, _axisY, 0f);

        Debug.Log(mov);

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, _presentSpeed * Time.deltaTime));
        
        if (Mathf.Abs(_axisX) > 0.1 || Mathf.Abs(_axisY) > 0.1) {
            _isMoving = true;
        }
        else {
            _isMoving = false;
        }
    }
    public float GetAxisX()
    {
        return _axisX;
    }
    public float GetAxisY()
    {
        return _axisY;
    }
    public bool GetActionInput()
    {
        return _actionInput;
    }
}
