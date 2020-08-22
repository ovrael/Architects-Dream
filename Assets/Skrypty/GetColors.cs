using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColors : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    public List<Color> fullAlphaColors = new List<Color>();
    public int[] countColors;
    Color[] gottenColors;

    // Start is called before the first frame update
    void Start()
    {
        //texture = (Texture2D) transform.GetComponent<SpriteRenderer>().material.mainTexture;
        Debug.Log(texture.name);
        gottenColors = texture.GetPixels();
        for (int i = 0; i < gottenColors.Length; i++)
        {
            if (gottenColors[i].a == 1)
            {
                if(fullAlphaColors.FindIndex(x => x == gottenColors[i]) < 0)
                    fullAlphaColors.Add(gottenColors[i]);
            }
        }

        countColors = new int[fullAlphaColors.Count];

        for (int i = 0; i < gottenColors.Length; i++)
        {
            if (gottenColors[i].a == 1)
            {
                countColors[fullAlphaColors.FindIndex(x => x == gottenColors[i])]++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
