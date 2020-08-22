using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlackMagicScript : PowerUps
{
    [SerializeField] float addedHP; // 10
    [SerializeField] float addedArmor; // 1
    [SerializeField] float attackSpeed; // 7
    [SerializeField] float addedDamage; // 5
    [SerializeField] float criticalStrikeChance; // 2
    [SerializeField] float criticalStrikeMultiplier; // 0.08

    public override void PickUp()
    {
        Debug.Log("Math is black magic");

        player.MaxHp += addedHP;
        player.Armor += addedArmor;
        player.AttackSpeed += attackSpeed;
        player.Damage += addedDamage;
        player.CriticalStrikeChance += criticalStrikeChance;
        player.CriticalStrikeMultiplier += criticalStrikeMultiplier;

        pickedItems.BlackMagic++;

        base.PickUp();
    }

    public override void Remove()
    {
        player.MaxHp -= addedHP;
        player.Armor -= addedArmor;
        player.AttackSpeed -= attackSpeed;
        player.Damage -= addedDamage;
        player.CriticalStrikeChance -= criticalStrikeChance;
        player.CriticalStrikeMultiplier -= criticalStrikeMultiplier;

        pickedItems.BlackMagic--;
    }
}
