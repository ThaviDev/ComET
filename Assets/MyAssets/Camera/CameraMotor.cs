using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    //[SerializeField] private int camSlotPosX; // La posicion del la camara en el mapa del juego
    //[SerializeField] private int camSlotPosY; // La posicion del la camara en el mapa del juego
    //Vector3 myPos;
    void Start()
    { 
        //myPos = transform.position;

    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.V))
        {
            changeCamPos();
        }
        */
        /*
        myPos.x = camSlotPosX * 8;
        myPos.y = camSlotPosY * 4.5f;
        transform.position = myPos;
        */

    }

    public void changeCamPos(int newCamSlotX, int newCamSlotY, float newXMovementScale, float newYMovementScale)
    {
        //camSlotPosX = newCamSlotX;
        //camSlotPosY = newCamSlotY;
        // Se crea un nuevo Vector3
        var myPos = transform.position;
        // Se igualan los puntos de X y Y a la posicion de los slots por la escala virtual del movimiento dentro de estos
        myPos.x = newCamSlotX * newXMovementScale;
        myPos.y = newCamSlotY * newYMovementScale;
        // La nueva posicion es ese Vector3
        transform.position = myPos;
    }
}
