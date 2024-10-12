using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMotor : MonoBehaviour
{
    [SerializeField] AreaAbilDetection _myAreaDet;
    [SerializeField] PlayerMotor _playerMotor;
    /*
    [Header("GameObjects que aparecen en ciertas habilidades:")]
    [SerializeField] List<GameObject> _abilObj; */

    public bool GetIsOnArea
    {
        get { return _myAreaDet.myAreaInteraction.isOnArea; }
    }

    public AreaInteraction GetCurrentArea
    {
        get { return _myAreaDet.myAreaInteraction; }
    }

    public int GetAbilityType
    {
        get { return _myAreaDet.myAreaInteraction.abilType; }
    }
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
            case 1: // Teletransportar izquierda
                print("Teletransportacion abajo");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x + -9, myVec.y);
                break;
            case 2: // Teletransportar abajo
                print("Teletransportacion izquierda");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x, myVec.y + -5);
                break;
            case 3: // Teletransportar derecha
                print("Teletransportacion derecha");
                _playerMotor.gameObject.transform.position = new Vector3(myVec.x + 9, myVec.y);
                break;
            case 4: // Llamar Felix
                print("Llamar Felix");
                break;
            case 5: // Ubicar Pieza
                print("Ubicar Pieza");
                break;
            case 6: // Comer Dulce
                print("Comer Dulce");
                break;
            case 7: // Hipnosis Adultos
                print("Hipnosis Adultos");
                break;
            case 8: // Llamar Nave de regreso
                print("Llamar Nave de regreso");
                break;
            case 9: // Lugar de aterrizaje
                // En este caso no se hace realmente nada pero es necesario tenerlo
                // como ID para el UI Manager, igual puede ser util para otra cosa
                print("Lugar de aterrizaje");
                break;
            case 10: // Ver Iconos de habilidades
                print("Ver Iconos de habilidades");
                break;
            case 11: // Volverse Intangible
                print("Volverse Intangible");
                break;
            case 12: // Energía Infinita
                print("Energía Infinita");
                break;
            case 13: // Congelamiento
                print("Congelamiento");
                break;
            case 14: // Gigantismo
                print("Gigantismo");
                break;
            case 15: // Llenar hoyos
                print("Llenar hoyos");
                break;
            case 16: // Super Caliente
                print("Super Caliente");
                break;
            case 17: // Seniuelo
                print("Seniuelo");
                break;
            case 18: // Campo de proteccion
                print("Campo de proreccion");
                break;
            default:
                break;
        }
    }
}
