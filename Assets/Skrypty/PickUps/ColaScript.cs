using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ColaScript : PowerUps
{
    [SerializeField] float attackSpeed; //15

    public override void PickUp()
    {
        Debug.Log("Zwiekszono szybkosc ataku");

        player.AttackSpeed += attackSpeed;
        pickedItems.Cola++;

        base.PickUp();
    }
}
