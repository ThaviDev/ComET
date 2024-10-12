using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlotPos : MonoBehaviour
{
    // Los slots representan escenarios en el mundo que estan interconectados entre si
    [SerializeField] int pSlotPosX;
    [SerializeField] int pSlotPosY;

    // Determina la escala de la pantalla virtual
    [SerializeField] float xMovementScale; // Default = 8f
    [SerializeField] float yMovementScale; // Default = 4.5f

    // Escala Y offset: Mover el espacio de un slot hacia abajo
    [SerializeField] float yOffScale;
    // Que tanto debe de moverse el punto pivote virtual del jugador que representa el momento en el que cambia de slot
    [SerializeField] float yBodyOffScale;
    // VirtualTrans representa la transformación espacial dentro de solo el espacio de un slot
    float pVirtualTransX;
    float pVirtualTransY;
    CameraMotor pCameraMotor;

    public int GetSlotPosX 
    { 
        get { return pSlotPosX; } 
    }
    public int GetSlotPosY
    {
        get { return pSlotPosY; }
    }

    private void Start()
    {
        pCameraMotor = Camera.main.GetComponent<CameraMotor>();
    }

    void Update()
    {
        // Por cada SlotPos necesita sumarse el transform actual con 8 si es X y 4.5 si es Y, el resultado es pVirtualTrans

        pVirtualTransX = transform.position.x - (pSlotPosX * xMovementScale);
        pVirtualTransY = transform.position.y - (pSlotPosY * yMovementScale) + yOffScale + yBodyOffScale;

        if (pVirtualTransX > (xMovementScale/2) || pVirtualTransX < -(xMovementScale / 2) || pVirtualTransY > (yMovementScale/2) || pVirtualTransY < -(yMovementScale / 2))
        {
            if (pVirtualTransX > (xMovementScale / 2))
            {
                pSlotPosX += 1;
            }
            else if (pVirtualTransX < -(xMovementScale / 2))
            {
                pSlotPosX -= 1;
            }
            else if (pVirtualTransY > (yMovementScale / 2))
            {
                pSlotPosY += 1;
            }
            else if (pVirtualTransY < -(yMovementScale / 2))
            {
                pSlotPosY -= 1;
            }
            pCameraMotor.changeCamPos(pSlotPosX, pSlotPosY, xMovementScale, yMovementScale);
        }

        var realX = xMovementScale/2;
        var realY = yMovementScale/2;

        Debug.DrawLine(new Vector2(realX, realY - yOffScale), new Vector2(realX, -realY - yOffScale), Color.blue);
        Debug.DrawLine(new Vector2(realX, -realY - yOffScale), new Vector2(-realX, -realY - yOffScale), Color.blue);
        Debug.DrawLine(new Vector2(-realX, -realY - yOffScale), new Vector2(-realX, realY - yOffScale), Color.blue);
        Debug.DrawLine(new Vector2(-realX, realY - yOffScale), new Vector2(realX, realY - yOffScale), Color.blue);
    }
}
