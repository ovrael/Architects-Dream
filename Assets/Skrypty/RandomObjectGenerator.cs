using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    [SerializeField] bool canRotate = true;
    [SerializeField] GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        float[] randomDirection = { -1.0f, 1.0f }; 
        int random = Random.Range(0, objects.Length);
        if(canRotate)
            objects[random].transform.localScale = new Vector3(randomDirection[Random.Range(0, 2)], 1f, 1f);
            //objects[random].transform.GetComponent<SpriteRenderer>().flipX = true;
        Instantiate(objects[random], transform.position, Quaternion.Euler(0f,0f,transform.rotation.eulerAngles.z), transform);
    }
}
