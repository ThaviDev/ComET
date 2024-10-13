using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    // Determina la escala de la pantalla virtual
    float _xMovementScale; // Default = 8f
    float _yMovementScale; // Default = 4.5f

    int _slotPosX;
    int _slotPosY;

    // Informacion que se guarda del ultimo slot pos
    int _lastSlotX;
    int _lastSlotY;

    [SerializeField] int _holeSlotX;
    [SerializeField] int _holeSlotY;

    [SerializeField] PlayerSlotPos _playerSlot;

    [SerializeField] float _waitForTeleport; // Este es para desactivar el movimiento de PlayerSlotPos y dejar la teletransportacion
    void Start()
    {
        _xMovementScale = _playerSlot.GetXMovementScale;
        _yMovementScale = _playerSlot.GetYMovementScale;
        _slotPosX = _playerSlot.GetSlotPosX;
        _slotPosY = _playerSlot.GetSlotPosY;
        MyGameManager.HoleFallEvent += GoToHole;
        MyGameManager.HoleClimbEvent += ReturnToSurface;
    }

    void Update()
    {
        if (_waitForTeleport > 0)
        {
            _waitForTeleport -=  Time.deltaTime;
            _playerSlot.SetCanChange = false;
        } else
        {
            _playerSlot.SetCanChange = true;
        }
    }

    private void GoToHole(Vector3 pPos)
    {
        _waitForTeleport = 0.2f;
        _lastSlotX = _slotPosX;
        _lastSlotY = _slotPosY;

        changeCamPos(_holeSlotX, _holeSlotY);
    }

    private void ReturnToSurface()
    {
        _waitForTeleport = 0.2f;
        changeCamPos(_lastSlotX,_lastSlotY);
    }

    public void changeCamPos(int _newCamSlotX, int _newCamSlotY)
    {
        // Se crea un nuevo Vector3
        var _myPos = transform.position;
        // Se igualan los puntos de X y Y a la posicion de los slots por la escala virtual del movimiento dentro de estos
        _myPos.x = _newCamSlotX * _xMovementScale;
        _myPos.y = _newCamSlotY * _yMovementScale;
        // La nueva posicion es ese Vector3
        transform.position = _myPos;
    }


}
