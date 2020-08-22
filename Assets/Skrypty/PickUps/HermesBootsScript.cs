using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HermesBootsScript : PowerUps
{
    [SerializeField] float additionalSpeed;

    public override void PickUp()
    {
        Debug.Log("Zwiekszono szybkosc ataku");

        player.Speed += additionalSpeed;
        pickedItems.HermesBoots++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.Speed -= additionalSpeed;
        pickedItems.HermesBoots--;
    }
}
