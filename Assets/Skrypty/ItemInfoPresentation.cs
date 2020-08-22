using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPresentation : MonoBehaviour
{
    //Dostepne w Unity
    //C#
    [Range(1f, 3f)][SerializeField] float scale; // 
    //UNITY
    [SerializeField] Sprite[] itemInfo;

    //Prywatne
    //UNITY
    static List<Sprite> kolejka = new List<Sprite>();
    //C#
    bool czyPrezentowane;

    // Update is called once per frame
    void Update()
    {
        if(kolejka.Count > 0)
        {
            if(!czyPrezentowane)
            {
                gameObject.GetComponent<Image>().sprite = kolejka[0];
                gameObject.GetComponent<Image>().SetNativeSize();
                gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

                czyPrezentowane = true;
                StartCoroutine("Presentation");

                kolejka.RemoveAt(0);
            }
        }
    }   

    public static void AddToQueue(Sprite info)
    {
        kolejka.Add(info);
    }

    IEnumerator Presentation()
    {
        gameObject.GetComponent<Image>().CrossFadeAlpha(150f, 0.5f, false);
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
        yield return new WaitForSeconds(0.4f);
        czyPrezentowane = false;
    }
}
