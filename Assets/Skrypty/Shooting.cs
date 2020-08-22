using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Dostepne
    //UNITY
    [SerializeField] GameObject arrow;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform heroSprite;
    //C#

    public void Shoot()
    {
        Instantiate(arrow, firePoint.position, firePoint.rotation, transform);
    }
}
