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
    private Vector3 _fallenHolePos;
    [SerializeField] Transform _spawnPosHole;


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
        MyGameManager.HoleFallEvent += PlayerFellInHole;
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
    bool isPlayerOnHole;
    void FixedUpdate()
    {
        /* -- En el momento en el que se aniade la gravedad hay que recordar --
         * no sobreescribir la velocidad de y, una persona en internet lo soluciono asi
         * characterVelocity = new Vector2 (xMov * speed, rigidbody2D.velocity.y); // where y is gravity
         * rigidbody2D.velocity = characterVelocity;
         */
        if (isPlayerOnHole)
        {
            PlayerInHole();
        } else
        {
            PlayerInWorld();
        }

    }

    bool isFalling;
    private void PlayerInHole() {
        HorizontalMovement();
        if (_ungroundedScript.isFloating)
        {
            FreeMovement();
        }
        /*
        if (!isFalling)
        {
        }
        else
        {
            // Si cae no puede moverse
        }*/

    }
    private void PlayerInWorld()
    {
        FreeMovement();
        //Axis de movimiento
        //_axisX = Input.GetAxisRaw("Horizontal");
        //_axisY = Input.GetAxisRaw("Vertical");
    }

    private void FreeMovement()
    {
        rb.gravityScale = 0;

        var _axisX = 0;
        var _axisY = 0;
        // Detector de Inputs
        if (_inptMoveUp) { _axisY = 1; } else if (_inptMoveDown) { _axisY = -1; } else { _axisY = 0; }
        if (_inptMoveRight) { _axisX = 1; } else if (_inptMoveLeft) { _axisX = -1; } else { _axisX = 0; }

        // Vector de direccion de movimiento
        Vector3 mov = new Vector3(_axisX, _axisY, 0f);

        // -- Moviendo el Rigid Body con MovePosition(MoveTowards) --
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards
            (transform.position, new Vector3(transform.position.x, transform.position.y) 
            + mov, _presentSpeed * Time.deltaTime));

        if (Mathf.Abs(_axisX) > 0.1 || Mathf.Abs(_axisY) > 0.1)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    private void HorizontalMovement()
    {
        rb.gravityScale = 1;

        var _axisX = 0;
        // Detector de Inputs
        if (_inptMoveRight) { _axisX = 1; } else if (_inptMoveLeft) { _axisX = -1; } else { _axisX = 0; }

        // Vector de direccion de movimiento
        Vector3 mov = new Vector3(_axisX, 0f, 0f);

        // -- Moviendo el Rigid Body con MovePosition(MoveTowards) --
        /*rb.MovePosition(Vector3.MoveTowards
            (transform.position, new Vector3(transform.position.x, transform.position.y)
            + mov, _presentSpeed * Time.deltaTime));*/

        // Obtén la velocidad actual del Rigidbody
        Vector3 velocity = rb.velocity;

        // Actualiza el componente X de la velocidad para el movimiento horizontal
        velocity.x = mov.x * _presentSpeed;

        // La gravedad actuará sobre el eje Y por sí sola si no lo tocas directamente
        // Solo actualiza la velocidad en el eje X para que la gravedad siga su curso.

        // Aplica la velocidad actualizada al Rigidbody
        rb.velocity = velocity;


        if (Mathf.Abs(_axisX) > 0.1)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    // Suscrito a evento de Game Manager
    private void PlayerFellInHole(Vector3 HolePos, Vector2 OgSlotPos)
    {
        // Se guarda la informacion del hoyo caido
        _fallenHolePos = HolePos;
        isPlayerOnHole = true;
        transform.position = _spawnPosHole.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Aqui se tiene que detectar
        // Si el jugador esta en hoyo, esta cayendo y la colision tiene cierto script, entonces podemos cambiar de caer a no caer
        // Incluso detonar el quitar energia
        // Podria tomarse en cuenta la posibilidad de contar el tiempo de vuelo para la cantidad de energia que se pierde

    }
}
