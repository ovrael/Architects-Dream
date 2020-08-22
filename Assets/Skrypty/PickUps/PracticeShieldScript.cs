using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PracticeShieldScript : PowerUps
{
    [SerializeField] float criticalStrikeChance; // 4

    public override void PickUp()
    {
        Debug.Log("Zwiekszono szanse na zadanie ciosu krytyczynego");

        player.CriticalStrikeChance += criticalStrikeChance;
        pickedItems.PracticeShield++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.CriticalStrikeChance -= criticalStrikeChance;
        pickedItems.PracticeShield--;
    }
}
