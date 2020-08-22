using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VikingHelmetScript : PowerUps
{
    [SerializeField] float armor; // 2

    public override void PickUp()
    {
        Debug.Log("Zwiększono pancerz");

        player.Armor += armor;
        pickedItems.VikingHelmet++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.Armor -= armor;
        pickedItems.VikingHelmet--;
    }

}
