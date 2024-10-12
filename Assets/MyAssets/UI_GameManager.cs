using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    [Header("Elementos UI")]
    [SerializeField] Slider[] _energySliders;
    [SerializeField] Slider[] _abilitiesSlider;
    [SerializeField] Image _topIcon;
    [SerializeField] Slider _timeSliderFast;
    [SerializeField] Slider _timeSliderSlow;
    [SerializeField] TMP_Text _candyText;
    [SerializeField] Slider _phoneSlider;
    [Header("Variables")]
    [SerializeField] Sprite[] _topIconSprites;
    /* 0: Nada
     * 1: teletransportacion 2: Hipnosis 3: Llamar Felix
     * 4: Comer Dulce 5: Encontrar Pieza 6: Llamar a casa
     * 7: Lugar de aterrizaje
     */
    [Header("Referencias")]
    [SerializeField] AbilityMotor _pAbil;
    [SerializeField] PlayerStats _pStats;

    void Update()
    {
        var _pEnergy = _pStats.GetEnergy;



        CalculateEnergyToBar(_pEnergy);
        CalculateCurrentTopIcon();
    }

    void CalculateCurrentTopIcon()
    {
        var _iD_Icon = _pAbil.GetAbilityType;
        var _isOnArea = _pAbil.GetIsOnArea;
        var _currentArea = _pAbil.GetCurrentArea;

        if (_currentArea == null)
        {
            _topIcon.sprite = _topIconSprites[19];
            return;
        }

        if (_isOnArea)
        {
            _topIcon.sprite = _topIconSprites[_iD_Icon];
        }
        else
        {
            _topIcon.sprite = _topIconSprites[19];
        }
    }

    void CalculateEnergyToBar(int _myEnergy)
    {
        int[] _barras = new int[4];
        int _virtualEnergy = _myEnergy;

        for (int i = 0; i <_barras.Length; i++)
        {
            if (_virtualEnergy >= _energySliders[i].maxValue)
            {
                _barras[i] = (int)_energySliders[i].maxValue;
                _virtualEnergy -= (int)_energySliders[i].maxValue;
            } else
            {
                _barras[i] = _virtualEnergy;
                _virtualEnergy = 0;
            }

            _energySliders[i].value = _barras[i];
        }
    }
}
