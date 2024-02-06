using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFloatEvent : MonoBehaviour
{
    public bool isFloating;
    //public bool isRunning;
    //public Animator animator;
    public void UngroundedEventStart()
    {
        isFloating = true;
    }
    public void UngroundedEventEnd()
    {
        isFloating = false;
    }
}
