using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentInfo : MonoBehaviour
{
    SpriteRenderer infoGraphic;

    private void Awake()
    {
        Destroy(gameObject, 3.5f);  
        infoGraphic = transform.GetComponent<SpriteRenderer>();
        infoGraphic.material.color = new Color(255f, 255f, 255f, 1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Presentation");
    }

    private void Update()
    {
        Debug.Log("Color: " + infoGraphic.color.ToString());
    }

    //IEnumerator Presentation()
    //{
    //    infoGraphic.CrossFadeAlpha(255f, 0.5f, false);
    //    yield return new WaitForSeconds(2f);
    //    infoGraphic.CrossFadeAlpha(1f, 0.5f, false);
    //}

    //IEnumerator FadeTo(float aValue, float aTime)
    //{
    //    float alpha = infoGraphic.material.color.a;
    //    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    //    {
    //        Color newColor = new Color(255f, 255f, 255f, Mathf.Lerp(alpha, aValue, t));
    //        infoGraphic.material.color = newColor;
    //        yield return null;
    //    }
    //}

    IEnumerator Presentation()
    {
        //Color newColor = new Color(255f, 255f, 255f, Mathf.Lerp(1f, 255f, 255f));
        //infoGraphic.color = newColor;

        //float fade = Mathf.SmoothStep(1f, 255f, 0.5f);

        for(float t = 0f; t <= 0.5f; t+= Time.deltaTime)
        {
            Color newColor = new Color(255f, 255f, 255f, t * 2);
            infoGraphic.color = newColor;
        }

        yield return new WaitForSeconds(2f);
        //newColor = new Color(255f, 255f, 255f, Mathf.Lerp(255f, 0f, 255f));
        //infoGraphic.color = newColor;
    }

}
