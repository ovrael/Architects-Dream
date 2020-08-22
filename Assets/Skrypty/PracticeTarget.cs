using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PracticeTarget : MonoBehaviour
{
    //Widziane w Unity
    //UNITY
    [SerializeField] Animator animacja;
    [SerializeField] ParticleSystem destroyParticle;
    [SerializeField] Slider hpSlider;
    [SerializeField] Gradient hpGradient;
    [SerializeField] GameObject dmgText;

    //C#
    [SerializeField] int minGoldForKill;
    [SerializeField] int maxGoldForKill;

    [SerializeField] float maxHealth;
    [SerializeField] float timeToColor;

    //Prywatne
    //UNITY
    SpriteRenderer sprite;
    Color defaultColor;
    Player player;
    //C#
    int goldForKill;

    float currentHealth;
    float newMaxHealth;

    float randX;
    float randY;

    private void OnValidate()
    {
        if(newMaxHealth != maxHealth)
        {
            newMaxHealth = maxHealth;
            hpSlider.maxValue = newMaxHealth;

            if (currentHealth > newMaxHealth)
            {
                currentHealth = newMaxHealth;
                hpSlider.value = currentHealth;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        newMaxHealth = maxHealth;
        currentHealth = maxHealth;
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
        hpSlider.GetComponentInChildren<Image>().color = hpGradient.Evaluate(1);

        goldForKill = Random.Range(minGoldForKill, maxGoldForKill);

        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        defaultColor = sprite.color;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!hpSlider.gameObject.activeSelf)
            hpSlider.gameObject.SetActive(true);

        PopUpDmgText(damage);

        //Debug.Log(gameObject.name + " oberwal za: " + damage);
        currentHealth -= damage;
        hpSlider.value = currentHealth;

        float hpPercent = currentHealth / maxHealth;

        hpSlider.GetComponentInChildren<Image>().color = hpGradient.Evaluate(hpPercent);

        StartCoroutine("SwitchColor");     
    }

    public void Die()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeAmountOfGold(goldForKill);
    }

    private void PopUpDmgText(float damage)
    {
        Color textColor;

        randX = Random.Range(-0.1f, 0.1f);
        randY = Random.Range(0.1f, 0.2f);




        GameObject createdText = Instantiate(dmgText, new Vector3(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);

        TextMeshProUGUI text = createdText.GetComponentInChildren<TextMeshProUGUI>();

        if (player.Damage < damage)
        {
            Debug.LogError(createdText.transform.localScale.ToString());
            createdText.transform.localScale = new Vector3(0.0016f, 0.0016f, 0.0016f);
            textColor = new Color(1f, 0.18f, 0.18f, 1f);
        }
        else
        {
            textColor = new Color(1f, 1f, 0.3f, 1f);
        }
        text.color = textColor;
        text.text = string.Format("{0}", damage);

        //createdText.GetComponentInChildren<TextMeshProUGUI>().UpdateVertexData();

        Destroy(createdText, 1.2f);
    }

    IEnumerator SwitchColor()
    {
        sprite.color = new Color(1f, 0.2f, 0.2f);
        yield return new WaitForSeconds(timeToColor);
        sprite.color = defaultColor;
    }
}
