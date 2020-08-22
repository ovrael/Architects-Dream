using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SandwichScript : PowerUps
{
    [SerializeField] float addedHP; //25

    public override void PickUp()
    {
        Debug.Log("Zwiekszono maksymalne zdrowie");

        player.MaxHp += addedHP;
        pickedItems.Sandwich++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.MaxHp -= addedHP;
        pickedItems.Sandwich--;
    }
}
