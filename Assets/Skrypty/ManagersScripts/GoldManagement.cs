using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoldManagement : MonoBehaviour
{
    //Dostepne w Unity
    //UNITY
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI goldText;
    //C#
    


    //Prywatne
    //UNITY
    
    //C#
    string goldString;

    // Start is called before the first frame update
    void Start()
    {
        goldString = string.Format("{0:n0}", player.Gold);
    }

    // Update is called once per frame
    void Update()
    {
        goldString = string.Format("{0:n0}", player.Gold);
        goldText.SetText(goldString);
    }
}
