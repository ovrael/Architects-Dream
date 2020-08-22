using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] private float fallMultiplier = 10f;
    public Animator animacja;
    public Rigidbody2D rb;
    float predkoscSpadania;
    float predkoscSpadaniaStara;
    float tmpGravityScale;

    public void Awake()
    {
        tmpGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.y >= -0.1) //is falling
        {
            //Debug.Log("Wznosze sie");
            rb.gravityScale = tmpGravityScale;
            predkoscSpadania = rb.velocity.y;
            predkoscSpadaniaStara = predkoscSpadania;
        }
        else
        {
            predkoscSpadania = rb.velocity.y;
            if(predkoscSpadania != predkoscSpadaniaStara)
            {
                Debug.Log("Wywoluje zmiane wartosci animatora w BETTERJUMP");
                animacja.SetBool("czySkacze", false);
                animacja.SetBool("czySpada", true);
                rb.gravityScale = fallMultiplier;
            }
            //Debug.Log("Spadam");
        }
    }
}
