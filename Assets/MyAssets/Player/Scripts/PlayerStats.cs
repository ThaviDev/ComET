using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int _pEnergy = 10000;
    [SerializeField] bool[] _phonePieces;
    [SerializeField] int _candyStored;
    [SerializeField] float _timeToArrive;
    [SerializeField] int[] _abilTimes;

    public int GetEnergy
    { 
        get { return _pEnergy; }
    }
    public bool[] GetPhonePieces
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
        _phonePieces = new bool[3] { false, false, false };
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

    public void AddPhonePiece(int _phoneID)
    {
        _phonePieces[_phoneID] = true;
        /* if (_phonePieces < 3)
        {
            _phonePieces++;
        }*/
    }
}
