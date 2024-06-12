using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMotor : MonoBehaviour
{
    [SerializeField] AreaAbilDetection _myArea;
    [SerializeField] PlayerMotor _playerMotor;

    private void Awake()
    {
        if (_myArea == null)
        {
            Debug.LogWarning("AbilityMotor no tiene referencias de Area Abil Detection");
            _myArea = gameObject.GetComponent<AreaAbilDetection>();
            if (_myArea == null)
            {
                Debug.LogWarning("Area Abil Detection no existe en Player, creando uno por AbilityMotor");
                gameObject.AddComponent<AreaAbilDetection>();
                _myArea = gameObject.GetComponent<AreaAbilDetection>();
            }
        }
        if (_playerMotor == null)
        {
            Debug.LogWarning("AbilityMotor no tiene referencias de PlayerMotor");
            _playerMotor = gameObject.GetComponent<PlayerMotor>();
        }
    }

    private void Update()
    {
        
    }
}
