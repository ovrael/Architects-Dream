using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpTextManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] bool pickUpTextOn;
    [SerializeField] GameObject pickUpText;
    [SerializeField] bool openChestTextOn;
    [SerializeField] GameObject openChestText;
    [SerializeField] bool requiredGoldTextOn;
    [SerializeField] TextMeshProUGUI requiredGoldText;

    private void OnTriggerEnter2D(Collider2D what)
    {
        switch (what.tag)
        {
            case "PickUp":
                if(pickUpTextOn)
                    pickUpText.SetActive(true);
                break;

            case "Chest":
                if(openChestTextOn)
                    openChestText.SetActive(true);
                if(requiredGoldTextOn)
                {
                    requiredGoldText.SetText("-" + string.Format("{0:n0}",what.GetComponent<ChestScript>().RequiredGold));
                    requiredGoldText.gameObject.SetActive(true);
                }
                break;


            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D what)
    {
        switch (what.tag)
        {
            case "PickUp":
                pickUpText.SetActive(false);
                break;

            case "Chest":
                requiredGoldText.gameObject.SetActive(false);
                openChestText.SetActive(false);
                break;


            default:
                break;
        }
    }
}