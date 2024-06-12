using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaInteraction : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] AreaAbilDetection _playerAreaDet;
    [SerializeField] float _size;
    [Header("Tipo de habilidad:")] // Solo debería de ser modificable por AreaPwrDetection
    public int abilType;
    [Header("Si el jugador esta en area:")] // Solo debería de ser modificable por AreaPwrDetection
    public bool isOnArea;

    void Update()
    {
        // Failsafe de existencia o referencia del jugador
        if (_playerAreaDet != null && _playerTransform != null)
        {
            CheckPlayer();
        } else
        {
            Debug.LogWarning("AreaInteraction no tiene referencias del jugador");
            var _player = FindObjectOfType<PlayerMotor>();
            _playerTransform = _player.transform;
            _playerAreaDet = _player.GetComponent<AreaAbilDetection>();
        }
    }
    void CheckPlayer()
    {
        var _myTransform = transform;
        var _newPlayerTransform = new Vector3(_playerTransform.transform.position.x, _playerTransform.transform.position.y + 0.35f);

        if (_newPlayerTransform.x > _myTransform.position.x - _size / 2 && _newPlayerTransform.x < _myTransform.position.x + _size / 2 &&
            _newPlayerTransform.y > _myTransform.position.y - _size / 2 && _newPlayerTransform.y < _myTransform.position.y + _size / 2)
        {
            _playerAreaDet.myAreaInteraction = this;
            isOnArea = true;
        }
        else
        {
            isOnArea = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (isOnArea)
        {
            Gizmos.color = new Color(1,0,0,0.2f); // Rojo
        }
        else
        {
            Gizmos.color = new Color(1, 0.92f, 0.016f, 0.2f); // Amarillo
        }
        Gizmos.DrawCube(transform.position, new Vector2(_size, _size));
    }
}
