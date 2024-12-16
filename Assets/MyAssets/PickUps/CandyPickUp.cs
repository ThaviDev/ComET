using UnityEngine;

public class CandyPickUp : PickUp
{
    [SerializeField] Sprite _sprite;
    protected override void Start()
    {
        base.Start();
        base._sr.sprite = _sprite;
    }

    protected override void WasPickedUp(PlayerStats _pyr)
    {
        //base.WasPickedUp(_pyr);
        _pyr.AddCandy();
    }
}
