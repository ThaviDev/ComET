using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _moveSpeedOG;
    [SerializeField]
    private float _moveSpeedRun;
    private float _presentSpeed;
    private bool _isMoving;
    private Rigidbody2D rb;
    private AbilityMotor _abilMot;
    //private AnimFloatEvent _ungroundedScript;
    [SerializeField] BoolSCOB _isFloating;
    /* Es el script que maneja los eventos de animacion cuando flota
    * Esta ubicado en los eventos de la animacion en el hijo que se encarga de la animacion
    * */

    private float _energyUsedAtRate; // Energia gastada por segundo
    [SerializeField] private float[] _energyUsedIn; // 0: Walking, 1: Running, 2: Floating
    [SerializeField] private float _energyUsedAtAbility; // Energia gastada al usar una habilidad
    [Header("References")]
    [SerializeField] MyInputManager _inputMan;
    bool _frame1Ability; // 
    private Vector3 _fallenHolePos;
    [SerializeField] Transform _spawnPosHole;
    [SerializeField] Collider2D _myHoleTrigger;
    [SerializeField] PlayerStats _stats;

    [SerializeField] float _gravityScale;

    // Inputs
    bool _inptMoveRight;
    bool _inptMoveLeft;
    bool _inptMoveUp;
    bool _inptMoveDown;
    bool _inptInteract;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //_ungroundedScript = transform.GetChild(0).GetComponent<AnimFloatEvent>();
        _abilMot = gameObject.GetComponent<AbilityMotor>();
        MyGameManager.HoleFallEvent += PlayerFellInHole;
        MyGameManager.HoleClimbEvent += PlayerClimedHole;
    }
    void Update()
    {
        _inptMoveRight = _inputMan.MoveRightButton;
        _inptMoveLeft = _inputMan.MoveLeftButton;
        _inptMoveUp = _inputMan.MoveUpButton;
        _inptMoveDown = _inputMan.MoveDownButton;
        _inptInteract = _inputMan.InteractButton;

        // Interaccion de abilidad
        //if (_ungroundedScript.isFloating && _frame1Ability == true)
        if (_isFloating.SCOB_Value && _frame1Ability == true)
        {
            _frame1Ability = false;
            _abilMot.ActivateAbility();
        }
        //if (!_ungroundedScript.isFloating)
        if (!_isFloating.SCOB_Value)
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

        //Logica de correr ---------------------

        //Solo puede correr cuando se mueva y no este flotando
        //if (_inptInteract && _ungroundedScript.isFloating == false && _isMoving)
        if (_inptInteract && _isFloating.SCOB_Value == false && _isMoving)
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

        //if (!_ungroundedScript.isFloating)
        if (!_isFloating.SCOB_Value)
        {
            _myHoleTrigger.enabled = true;
        } else
        {
            _myHoleTrigger.enabled = false;
        }
        _energyUsedAtRate -= Time.deltaTime;
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
    private void PlayerInHole() {
        HorizontalMovement();
        //if (_ungroundedScript.isFloating)
        if (_isFloating.SCOB_Value == true)
        {
            FreeMovement();
        }
    }
    private void PlayerInWorld()
    {
        FreeMovement();
        //Axis de movimiento
        //_axisX = Input.GetAxisRaw("Horizontal");
        //_axisY = Input.GetAxisRaw("Vertical");
    }

    bool restartYVelocity;
    private void FreeMovement()
    {
        // este bool sirve para que no aumente la velocidad de la gravedad cambiando de free movement a horizontal movement
        restartYVelocity = true;
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
        if (restartYVelocity)
        {
            Vector3 _vel = rb.velocity;
            _vel.y = 0;
            rb.velocity = _vel;
            restartYVelocity = false;
        }
        rb.gravityScale = _gravityScale;

        var _axisX = 0;
        // Detector de Inputs
        if (_inptMoveRight) { _axisX = 1; } else if (_inptMoveLeft) { _axisX = -1; } else { _axisX = 0; }

        // -- Moviendo el Rigid Body con MovePosition(MoveTowards) --
        /*rb.MovePosition(Vector3.MoveTowards
            (transform.position, new Vector3(transform.position.x, transform.position.y)
            + mov, _presentSpeed * Time.deltaTime));*/

        // Vector de direccion de movimiento
        Vector3 mov = new Vector3(_axisX, 0f, 0f);

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
    private void PlayerFellInHole(Vector3 HolePos)
    {
        // este bool sirve para que no aumente la velocidad de la gravedad cambiando de free movement a horizontal movement
        restartYVelocity = true;
        // Se guarda la informacion del hoyo caido
        _fallenHolePos = HolePos;
        isPlayerOnHole = true;
        transform.position = _spawnPosHole.position;
    }

    // Suscrito a evento de Game Manager
    private void PlayerClimedHole()
    {
        transform.position = _fallenHolePos;
        isPlayerOnHole = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Aqui se tiene que detectar
        // Si el jugador esta en hoyo, esta cayendo y la colision tiene cierto script, entonces podemos cambiar de caer a no caer
        // Incluso detonar el quitar energia
        // Podria tomarse en cuenta la posibilidad de contar el tiempo de vuelo para la cantidad de energia que se pierde
        if (col.gameObject.transform.GetComponent<HoleCeilingTrigger>())
        {
            // Detonar evento de regresar a la superficie
        }

    }
}
