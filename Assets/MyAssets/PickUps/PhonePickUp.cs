using UnityEngine;

public class PhonePickUp : PickUp
{
    [SerializeField] int _iD;
    [SerializeField] Sprite[] _phoneSprites;
    protected override void Start()
    {
        base.Start();
        base._sr.sprite = _phoneSprites[_iD];
    }

    protected override void WasPickedUp(PlayerStats _pyr)
    {
        //base.WasPickedUp(_pyr);
        _pyr.AddPhonePiece(_iD);
    }
}
