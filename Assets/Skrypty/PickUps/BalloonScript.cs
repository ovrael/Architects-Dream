using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BalloonScript : PowerUps
{
    [SerializeField] float lessFallMultiplier; // 0.05

    public override void PickUp()
    {
        Debug.Log("Zwiekszono redukcje obrazen");

        if(pickedItems.Balloon < 10)
            player.FallMultiplier -= lessFallMultiplier;

        pickedItems.Balloon++;

        base.PickUp();
    }

    public override void Remove()
    {
        if (pickedItems.Balloon <= 10)
            player.FallMultiplier += lessFallMultiplier;
        pickedItems.Balloon--;
    }
}
