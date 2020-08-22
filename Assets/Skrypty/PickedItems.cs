using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedItems : MonoBehaviour
{
    [SerializeField] int blackMagic;
    [SerializeField] int beer;
    [SerializeField] int cola;
    [SerializeField] int indianBand;
    [SerializeField] int practiceShield;
    [SerializeField] int sandwich;
    [SerializeField] int vikingHelmet;
    [SerializeField] int hermesBoots;
    [SerializeField] int spring;
    [SerializeField] int balloon;

    public int BlackMagic { get => blackMagic; set => blackMagic = value; }
    public int Beer { get => beer; set => beer = value; }
    public int Cola { get => cola; set => cola = value; }
    public int IndianBand { get => indianBand; set => indianBand = value; }
    public int PracticeShield { get => practiceShield; set => practiceShield = value; }
    public int Sandwich { get => sandwich; set => sandwich = value; }
    public int VikingHelmet { get => vikingHelmet; set => vikingHelmet = value; }
    public int HermesBoots { get => hermesBoots; set => hermesBoots = value; }
    public int Spring { get => spring; set => spring = value; }
    public int Balloon { get => balloon; set => balloon = value; }

    private void Start()
    {
        BlackMagic = 0;
        Beer = 0;
        Cola = 0;
        IndianBand = 0;
        PracticeShield = 0;
        Sandwich = 0;
        VikingHelmet = 0;
        HermesBoots = 0;
        Spring = 0;
        Balloon = 0;
    }
}
