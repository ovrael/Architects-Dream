using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChestScript : MonoBehaviour
{
    //Dostepne w Unity
    //C#
    [SerializeField] long requiredGold;
    [SerializeField] bool drawGizmo;
    [SerializeField] float interactRadius;
    [SerializeField] float yOffset;
    //UNITY
    [SerializeField] GameObject goldRqText;
    [SerializeField] Animator animator;
    [SerializeField] PowerUps[] whatPowerUpsSpawn;

    //Prywatne
    //UNITY
    AudioSource audioSource;
    GameObject player;
    //C#
    bool isOpen = false;

    //Wlasciwosci
    public long RequiredGold { get => requiredGold; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        drawGizmo = true;
        isOpen = false;
    }

    private void Update()
    {
         if(Vector2.Distance(transform.position, player.transform.position) < interactRadius)
            {
                if(Input.GetButtonDown("Interact"))
                {
                    if(!isOpen)
                    {
                        if (player.GetComponentInChildren<Player>().Gold >= RequiredGold)
                        {
                            player.GetComponent<Player>().ChangeAmountOfGold(-RequiredGold);
                            Open();
                        }
                    }
                }
         }
    }

    private void Open()
    {
        isOpen = true;
        Debug.Log("Otwarto skrzynie!");
        animator.SetTrigger("Open");
        //AudioManager.instance.Play("OpeningChest");
        audioSource.Play();
        transform.GetComponent<CapsuleCollider2D>().enabled = false;

        int whatSpawn = UnityEngine.Random.Range(0, whatPowerUpsSpawn.Length);

        //Jakies particle?
        Instantiate(whatPowerUpsSpawn[whatSpawn], transform.position + new Vector3(0f, yOffset, 0f), Quaternion.identity);
    }

    private void OnTriggerEnter2D (Collider2D what)
    {
        if (what.gameObject.tag == "Player")
        {
            string goldString = Convert.ToString(requiredGold);

            if(goldString.Length >= 6)
            {
                if(goldString.Length >= 9)
                {
                    goldString = goldString.Substring(0, goldString.Length - 6);
                    goldString += "KK";
                }
                else
                {
                    goldString = goldString.Substring(0, goldString.Length - 3);
                    goldString += "K";
                }
            }

            goldRqText.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 0f);
            goldRqText.GetComponent<TextMeshPro>().SetText(goldString);
            goldRqText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D what)
    {
        if (what.gameObject.tag == "Player")
        {
            goldRqText.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        if(drawGizmo)
            Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
