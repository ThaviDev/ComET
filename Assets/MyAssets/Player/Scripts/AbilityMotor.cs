using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMotor : MonoBehaviour
{
    [SerializeField] AreaAbilDetection _myAreaDet;
    [SerializeField] PlayerMotor _playerMotor;
    [Header("GameObjects que aparecen en ciertas habilidades:")]
    [SerializeField] List<GameObject> _abilObj;

    private void Awake()
    {
        if (_myAreaDet == null)
        {
            Debug.LogWarning("AbilityMotor no tiene referencias de Area Abil Detection");
            _myAreaDet = gameObject.GetComponent<AreaAbilDetection>();
            if (_myAreaDet == null)
            {
                Debug.LogWarning("Area Abil Detection no existe en Player, creando uno por AbilityMotor");
                gameObject.AddComponent<AreaAbilDetection>();
                _myAreaDet = gameObject.GetComponent<AreaAbilDetection>();
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

    public void ActivateAbility()
    {
        print("Me Activo");
        if (_myAreaDet.myAreaInteraction == null || !_myAreaDet.myAreaInteraction.isOnArea)
        {
            // !!! Tengo que pensar sobre cómo lidiaré con la llegada de la nave, ya que no es un area que se active
            print("Aqui no hay nada");
            return;
        }
        var _abilID = _myAreaDet.myAreaInteraction.abilType;

        var myVec = _playerMotor.gameObject.transform.position;

        switch (_abilID)
        {
            case 0: // Teletransportar arriba
                print("Teletransportacion arriba");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x, myVec.y + 5);
                break;
            case 1: // Teletransportar abajo
                print("Teletransportacion abajo");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x, myVec.y + -5);
                break;
            case 2: // Teletransportar izquierda
                print("Teletransportacion izquierda");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x + -9, myVec.y);
                break;
            case 3: // Teletransportar derecha
                print("Teletransportacion derecha");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x + 9, myVec.y);
                break;
            case 4: // Hipnosis adultos
                print("Hipnosis adultos");
                break;
            case 5: // Llamar a Felix
                print("Llamar a Felix");
                break;
            case 6: // Comer Dulce
                print("Comer Dulce");
                break;
            case 7: // Ubicar Pieza de telefono
                print("Ubicar Pieza de telefono");
                break;
            case 8: // Llamar nave de regreso
                print("Llamar Nave de regreso");
                break;
            default:
                break;
        }
    }
}
