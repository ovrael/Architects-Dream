using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Health
    [Header("HP")]
    //Dostepne
    //UNITY
    //C#
    [SerializeField] float maxHp;

    //Prywatne
    //UNITY
    //C#
    float currentHealth;
    
    //Wlasciwosci
    public float MaxHp { get => maxHp; set => maxHp = value; }


    #endregion

    #region Fight
    [Header("Fight")]
    //Dostepne
    //UNITY
    //C#
    [Header("Left click atatck")]
    [SerializeField] float attackSpeed;                      // About 100 for start
    [SerializeField] float damage;                           // About 25 for start
    [SerializeField] float criticalStrikeChance;             // About 5 for start
    [SerializeField] float criticalStrikeMultiplier;         // Starts from 1.0, increases to about 5.0? 



    [Header("Defense")]
    [SerializeField] float armor;                            // FLAT dmg reduction
    [SerializeField] float physicalReduction;                // In % where 1.0 = 100%


    //Prywatne
    //UNITY
    //C#

    //Wlasciwosci
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float CriticalStrikeChance { get => criticalStrikeChance; set => criticalStrikeChance = value; }
    public float CriticalStrikeMultiplier { get => criticalStrikeMultiplier; set => criticalStrikeMultiplier = value; }
    
    public float Armor { get => armor; set => armor = value; }
    public float PhysicalReduction { get => physicalReduction; set => physicalReduction = value; }

    #endregion

    #region Gold
    [Header("Gold")]
    //Dostepne
    //UNITY
    //C#
    [SerializeField] long gold;

    //Prywatne
    //UNITY
    //C#

    //Wlasciwosci
    public long Gold { get => gold; }


    #endregion

    #region Movement
    [Header("Movement")]
    //Dostepne
    //UNITY
    //C#
    [SerializeField] float speed;                                    // Start at about 20
    [Range(1f, 2f)] [SerializeField] float sprintModifier;           // Start at about 1.4
    [SerializeField] float jumpPower;                                // Start at about 11  
    [SerializeField] float fallMultiplier;                           // Start at about 3

    //Prywatne
    //UNITY
    //C#

    //Wlasciwosci
    public float Speed { get => speed; set => speed = value; }
    public float SprintModifier { get => sprintModifier; }
    public float JumpPower { get => jumpPower; set => jumpPower = value; }
    public float FallMultiplier { get => fallMultiplier; set => fallMultiplier = value; }

    #endregion

    private void Start()
    {
        currentHealth = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= ((1 - PhysicalReduction) * damage - Armor); 
    }

    public void ChangeAmountOfGold(long goldAmount)
    {
        gold += goldAmount;
    }

}
