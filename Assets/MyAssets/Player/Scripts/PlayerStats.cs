using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int _pEnergy = 10000;
    [SerializeField] int _phonePieces;
    [SerializeField] int _candyStored;
    [SerializeField] float _timeToArrive;
    [SerializeField] int[] _abilTimes;

    public int GetEnergy
    { 
        get { return _pEnergy; }
    }
    public int GetPhonePieces
    {
        get { return _phonePieces; }
    }
    public int GetCandyStored
    {
        get { return _candyStored; } 
    }
    public float GetTimeToArrive
    {
        get { return _timeToArrive; }
    }
    public int[] GetAbilityTimes
    {
        get { return _abilTimes; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddCandy()
    {
        if (_candyStored < 9) 
        {
            _candyStored++;
        }
    }

    public void AddPhonePiece()
    {
        if (_phonePieces < 3)
        {
            _phonePieces++;
        }
    }
}
