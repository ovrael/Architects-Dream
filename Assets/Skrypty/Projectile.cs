using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    //Dostepne
    //UNITY
    [SerializeField] Rigidbody2D projectileBody;
    [SerializeField] LayerMask whatIsSolid;

    [Header("Particles")]
    [SerializeField] ParticleSystem bloodParticle;
    [SerializeField] ParticleSystem groundHitParticle;

    //C#
    [Header("Arrow Physics")]
    [SerializeField] float lifeTime;
    [SerializeField] float shotPower;
    [SerializeField] AudioClip[] arrowSounds;
    [SerializeField] AudioClip[] hitSounds;

    //Prywatne
    //UNITY
    Player gracz;
    Transform arrowHead;
    AudioSource audioSource;
    //C#
    float yVelocity;
    float xVelocity;
    float actualAngle;

    bool trafiony = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        projectileBody.AddRelativeForce(Vector2.right * shotPower, ForceMode2D.Force);  
    }

    private void Start()
    {
        audioSource.clip = arrowSounds[UnityEngine.Random.Range(0, arrowSounds.Length)];
        audioSource.Play();
        //PlaySound("ArrowSound", 1);


        actualAngle = transform.rotation.eulerAngles.z; //Kat strzaly
        transform.rotation = Quaternion.Euler(0f, 0f, actualAngle);
        gracz = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        arrowHead = transform.Find("ArrowHead").transform;
    }

    private void Update()
    {
        if(!trafiony)
        {
            yVelocity = projectileBody.velocity.y;
            xVelocity = projectileBody.velocity.x;
            actualAngle = Mathf.Atan2(yVelocity, xVelocity) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, actualAngle);
    }
    
    private void HitSomething()
    {
        Invoke("DestroyProjectile", lifeTime);
        projectileBody.velocity = Vector2.zero;
        projectileBody.gravityScale = 0;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private float CalculateDamage(float playerDamage)
    {
        if (UnityEngine.Random.Range(0, 101) <= gracz.CriticalStrikeChance)
        {
            return playerDamage * gracz.CriticalStrikeMultiplier;
        }
        else
        {
            return playerDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trafiono: " + collider.tag);
        if(!collider.CompareTag("Player") && !collider.CompareTag("NoCollisionProjectile"))
        {
            trafiony = true;
            HitSomething();

            if(collider.CompareTag("Enemy"))
            {
                //collider.GetComponent<Enemy>();
            }
            else if(collider.CompareTag("Practice"))
            {

                audioSource.clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)];
                audioSource.Play();

                collider.GetComponent<PracticeTarget>().TakeDamage(CalculateDamage(gracz.Damage));
                Instantiate(bloodParticle, arrowHead.position, Quaternion.identity, transform);
            }
            else if(collider.CompareTag("Ground"))
            {
                audioSource.clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Length)];
                audioSource.Play();

                Instantiate(groundHitParticle, arrowHead.position, Quaternion.identity, transform);
                groundHitParticle.Stop();
                groundHitParticle.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player") && !collider.CompareTag("NoCollisionProjectile"))
        {
            DestroyProjectile();
        }
    }

    private void PlaySound(string name, int amount)
    {
        int rand = UnityEngine.Random.Range(0, amount);
        AudioManager.instance.Play(name + Convert.ToString(rand));
    }

}
