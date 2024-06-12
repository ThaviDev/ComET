using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFloatEvent : MonoBehaviour
{
    public bool isFloating;
    //public bool isRunning;
    //public Animator animator;
    public void UngroundedEventStart() // Esta funcion es manejada por el aminator con un evento
    {
        isFloating = true;
    }
    public void UngroundedEventEnd() // Esta funcion es manejada por el aminator con un evento
    {
        isFloating = false;
    }
}
