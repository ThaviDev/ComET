using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _pEnergy = 10000;
    [SerializeField] bool[] _phonePieces;
    [SerializeField] int _candyStored;
    [SerializeField] float _timeToArrive;
    [SerializeField] int[] _abilTimes;
    [SerializeField] float _energyDecreaseWalk;
    [SerializeField] float _energyDecreaseRun;
    [SerializeField] float _energyDecreaseActing;
    int _pStatus; // 0 idle, 1 caminando, 2 corriendo, 3 actuando
    public void SetPlayerStatus(int value)
    {
        _pStatus = value;
    }

    public float GetEnergy
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
        switch (_pStatus)
        {
            case 0:
                print("no reducir energia");
                break;
            case 1:
                print("reducir energia por caminar");
                _pEnergy -= Time.deltaTime * _energyDecreaseWalk;
                break;
            case 2:
                print("reducir energia por correr");
                _pEnergy -= Time.deltaTime * _energyDecreaseRun;
                break;
            case 3:
                print("reducir energia por flotar");
                _pEnergy -= Time.deltaTime * _energyDecreaseActing;
                break;
            default:
                Debug.LogWarning("No clear Player Status on PlayerStats()");
                break;
        }
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
