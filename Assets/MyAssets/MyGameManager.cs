using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private static MyGameManager _instance;
    // Evento cuando el jugador cae en un hoyo
    public static Action<Vector3,Vector2> HoleFallEvent;
    public static MyGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar una instancia existente en la escena.
                _instance = FindObjectOfType<MyGameManager>();

                if (_instance == null)
                {
                    // Crear un nuevo GameObject con el script adjunto si no se encuentra ninguna instancia.
                    GameObject singletonObject = new GameObject("MyGameManager");
                    _instance = singletonObject.AddComponent<MyGameManager>();

                    // Opcional: Evitar que el objeto sea destruido al cambiar de escena.
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que el objeto sea destruido al cambiar de escena.
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destruir instancias adicionales si ya existe una instancia.
        }
    }

    public void HoleFallEventTrigger(Vector3 ogPos,Vector2 slotPos) // Detonado por Hole Motor
    {
        HoleFallEvent(ogPos,slotPos);
    }
}
