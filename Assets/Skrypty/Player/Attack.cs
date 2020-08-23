using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Prywatne dostepne w Unity
    //Unity
    [SerializeField] Animator animacja;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform heroSprite;

    //C#
    [SerializeField] float negativAttackSpeed = 100;          // x / PlayerAttackSpeed


    //Prywatne
    //Unity
    Player player;

    //C#
    float attackTime;
    float timeBetweenAttacks;


    //C#

    //Metody
    public void Shoot()
    {
        Instantiate(arrow, firePoint.position, firePoint.rotation, transform);
    }
    public void Atak()
    {
        if (attackTime <= 0)
        {
            animacja.SetBool("czyAtakuje", true);
            Shoot();
            animacja.SetFloat("AttackSpeed", player.AttackSpeed / 100);
            attackTime = timeBetweenAttacks;
        }
        else
        {
            animacja.speed = 1f;
            attackTime -= Time.deltaTime;
            animacja.SetBool("czyAtakuje", false);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Player>();
        timeBetweenAttacks = negativAttackSpeed / player.AttackSpeed;
    }

    private void Update()
    {
        timeBetweenAttacks = negativAttackSpeed / player.AttackSpeed;
        if (Input.GetButton("LeftClick"))
        {
            Atak();
        }
        else
        {
            attackTime -= Time.deltaTime;
            animacja.SetBool("czyAtakuje", false);
        }
    }
}

