using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpringScript : PowerUps
{
    [SerializeField] float jumpForce; // 1

    public override void PickUp()
    {
        Debug.Log("Zwiekszono redukcje obrazen");

        player.JumpPower += jumpForce;
        pickedItems.Spring++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.JumpPower -= jumpForce;
        pickedItems.Spring--;
    }
}
