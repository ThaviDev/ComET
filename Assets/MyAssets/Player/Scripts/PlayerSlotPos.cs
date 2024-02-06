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

    // Que tanto debe de moverse el punto pivote virtual que representa el momento en el que cambia de slot
    [SerializeField] float yOffscale;
    // VirtualTrans representa la transformación espacial dentro de solo el espacio de un slot
    float pVirtualTransX;
    float pVirtualTransY;
    CameraMotor pCameraMotor;

    private void Start()
    {
        pCameraMotor = Camera.main.GetComponent<CameraMotor>();
    }

    void Update()
    {
        // Por cada SlotPos necesita sumarse el transform actual con 8 si es X y 4.5 si es Y, el resultado es pVirtualTrans

        pVirtualTransX = transform.position.x - (pSlotPosX * xMovementScale);
        pVirtualTransY = transform.position.y - (pSlotPosY * yMovementScale) + yOffscale;

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
    }
}
