using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class PowerUps : MonoBehaviour
{
    //Dostepne w Unity
    //UNITY
    [SerializeField] protected Sprite itemInfo;
    //C#

    //PRYWATNE
    //UNITY
    protected ParticleSystem pickUpParticle;
    protected Player player;
    protected PickedItems pickedItems;
    new CircleCollider2D collider;
    ParticleSystem[] particles;
    //C#
    float timeToAutoPickUp;
    float autoPickUpRadius;
    float pickUpRadius;
    float startY;
    float limit;
    float speed = 3f;
    float height = 0.14f;

    bool goUp = false;


    private void UpAndDownMovement()
    {
        if(transform.position.y >= limit)
        {
            goUp = false;          
        }
        if(transform.position.y <= startY)
        {
            goUp = true;
        }
        
        if(goUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + speed / 1000, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - speed / 1000, transform.position.z);
    }

    public virtual void PickUp()
    {
        //Debug.Log("Podniesiono " + gameObject.name);


        ItemInfoPresentation.AddToQueue(itemInfo);
        Instantiate(pickUpParticle, GameObject.FindGameObjectWithTag("Player").transform);

        Destroy(gameObject);
    }

    public virtual void Remove()
    {

    }


    private void Start()
    {
        //Setting pickup
        gameObject.tag = "PickUp";
        gameObject.layer = 15;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";

        collider = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
        collider.radius = 0.8f;
        collider.isTrigger = true;

        pickUpRadius = collider.radius;
        autoPickUpRadius = pickUpRadius / 3;
        timeToAutoPickUp = 2f;

        startY = transform.position.y;
        limit = startY + height;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pickedItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PickedItems>();

        particles = FindObjectsOfType<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            if (particle.name == "PickUpParticle")
                pickUpParticle = particle;
        }
        pickUpParticle.playOnAwake = true;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distance <= pickUpRadius)
        {
            if(Input.GetButtonDown("Interact") || (distance <= autoPickUpRadius && timeToAutoPickUp <= 0))
            {
                PickUp();
            }
        }
    }

    private void FixedUpdate()
    {
        timeToAutoPickUp -= Time.deltaTime;
        UpAndDownMovement();
    }
}
