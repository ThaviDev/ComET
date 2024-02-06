using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedOG;
    [SerializeField]
    private float moveSpeedRun;
    private float presentSpeed;
    private float axisX;// El valor X del jugador
    private float axisY;// El valor Y del jugador
    private bool actionInput;
    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private AnimFloatEvent ungroundedScript; // Es el script que maneja los eventos de animacion cuando flota
    
    void Start()
    {
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        ungroundedScript = gameObject.transform.GetChild(0).GetComponent<AnimFloatEvent>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Logica de accion
        actionInput = Input.GetButton("Jump");
        if (actionInput)
        {
            anim.SetBool("IsActing", true);
        } else
        {
            anim.SetBool("IsActing", false);
        }
        //Logica de correr
        //Solo puede correr cuando se mueva y no este flotando
        if (actionInput && ungroundedScript.isFloating == false && anim.GetBool("IsMoving"))
        {
            anim.SetBool("IsRunning", true);
            presentSpeed = moveSpeedRun;
        }
        else
        {
            presentSpeed = moveSpeedOG;
        }
        //Deja de estar en el estado de correr cuando deja de presionar accion
        //PD: Puede estar en el estado de correr mientras este quieto,
        //esto es para que no empieze actuar mientras esta cambiando de direccion
        if (!actionInput && anim.GetBool("IsRunning"))
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        //Axis de movimiento
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        Vector3 mov = new Vector3(axisX, axisY, 0f);

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, movementSpeedIG * Time.deltaTime );
        rb.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y) + mov, presentSpeed * Time.deltaTime));
        
        if (Mathf.Abs(axisX) > 0.1 || Mathf.Abs(axisY) > 0.1) {
            anim.SetBool("IsMoving", true);
        }
        else {
            anim.SetBool("IsMoving", false);
        }
        if (axisX > 0) {
            sprite.flipX = true;
        }
        else if (axisX < 0) {
            sprite.flipX = false;
        }
    }
}
