using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BeerScript : PowerUps
{
    [SerializeField] float physicalReduction; // 0.1

    // Start is called before the first frame update
    public override void PickUp()
    {
        Debug.Log("Zwiekszono redukcje obrazen");

        player.PhysicalReduction *= physicalReduction;
        pickedItems.Beer++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.PhysicalReduction /= physicalReduction;
        pickedItems.Beer--;
    }
}
