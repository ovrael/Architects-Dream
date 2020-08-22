using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Zmienne Unity")]
    //public Player gracz;
    [SerializeField] CharacterController2D kontroler;
    [SerializeField] Player player;
    [SerializeField] Animator animacja;
    [SerializeField] Attack skryptAtaku;

    Transform sprite;
    Vector3 myszka = Vector3.zero;

    float ruchPoziomy = 0.0f;
    float predkoscRuchu;
    float rotZ;

    bool skok = false;
    bool ciagleSkacze = false;
    bool czySprintuje = false;
    bool wcisnietyLPM = false;
    bool przerwano = false;

    public void OnLanding()
    {
        animacja.SetBool("czySpada", false);
        animacja.SetBool("czyWyladowal", true);
    }

    private void Start()
    {
        predkoscRuchu = player.Speed;
        sprite = transform.Find("PlayerSprite").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        predkoscRuchu = player.Speed;
        #region Ruch
        ruchPoziomy = Input.GetAxisRaw("Horizontal") * predkoscRuchu;   

        animacja.SetFloat("Predkosc", Mathf.Abs(ruchPoziomy));

        if (Input.GetButtonDown("Jump")) //Input.GetButtonDown("Jump")
        {
            StartCoroutine("Jump");
            //skok = true;
            //animacja.SetBool("czySkacze", true);
            //animacja.SetBool("czyWyladowal", false);
        }

        if (Input.GetButtonUp("Jump")) //Input.GetButtonDown("Jump")
        {
            skok = false;
            ciagleSkacze = false;
            przerwano = true;
        }

        if (Input.GetButton("Jump")) //Input.GetButtonDown("Jump")
        {
            ciagleSkacze = true;
        }

        if (Input.GetButtonDown("Sprint"))
        {
            czySprintuje = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            czySprintuje = false;
        }
        #endregion
    }


    void FixedUpdate()
    { 
        kontroler.LewoPrawo(ruchPoziomy, czySprintuje);
        kontroler.Skok(skok, ciagleSkacze, ref przerwano);
    }

    IEnumerator Jump()
    {
        skok = true;
        animacja.SetBool("czySkacze", true);
        animacja.SetBool("czyWyladowal", false);
        yield return new WaitForSeconds(0.5f);
        skok = false;
    }
}
