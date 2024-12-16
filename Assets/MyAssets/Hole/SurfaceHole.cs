using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.GetComponent<HoleTrigger>())
        {
            // Aqui obtenemos la posicion de los slots del jugador,
            // Esto es para comunicarselo al evento que le enviara la informacion a la camara
            // Se llama al evento con la posicion del hoyo y el slot del jugador
            MyGameManager.Instance.HoleFallEventTrigger(transform.position);
        }
    }
}
