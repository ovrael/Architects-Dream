using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IndianBandScript :PowerUps
{
    [SerializeField] float criticalStrikeMultiplier; //0.15

    public override void PickUp()
    {
        Debug.Log("Zwiekszono współczynnik ataków krytycznych");

        player.CriticalStrikeMultiplier += criticalStrikeMultiplier;
        pickedItems.IndianBand++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.CriticalStrikeMultiplier -= criticalStrikeMultiplier;
        pickedItems.IndianBand--;
    }
}
