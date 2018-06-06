using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{

    public Image currentHealthBar;
    public Text ratioText;

    private const float maxHitpoint = 100;
    public float hitpoint = maxHitpoint;

    public Texture img_demage;
    private bool take_damage = false;

    public AudioClip audioClip;
    public AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        UpdateHealthBar();
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void UpdateHealthBar()
    {
        float ratio = (hitpoint / maxHitpoint);//%hp

        if (ratio >= 0)
        {
            currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            ratioText.text = System.Math.Round((ratio * 100), 0).ToString() + "%";
        }
    }
    public void TakeDamage(float damage)
    {
        StartCoroutine(Example());
        hitpoint -= damage;
        int hitpoint_int = (int)hitpoint;
        if (hitpoint_int <= 0)
        {
            hitpoint = 0;
            hitpoint_int = 0;
            Destroy(gameObject);
            Debug.Log("Dead!");
        }
        UpdateHealthBar();
    }
    public void TakeHealth(float health)
    {
        float temHealth = maxHitpoint - hitpoint;

        if (health > maxHitpoint)
        {
            health += temHealth;
            PlaySound();
        }
        if (hitpoint < maxHitpoint)
        {
            hitpoint += health;
            PlaySound();
        }
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
     
        int hitpoint_int = (int)hitpoint;

        UpdateHealthBar();
    }

    ///do apteczkki
    public float getMaxHiPoint()
    {
        return maxHitpoint;
    }
    public float getHiPoint()
    {
        return hitpoint;
    }

    IEnumerator Example()
    {
        take_damage = true;
        yield return new WaitForSeconds(2);
        take_damage = false;
    }
    void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        
    }

    void OnGUI()
    {
        if (take_damage == true)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), img_demage, ScaleMode.ScaleToFit);
        }
    }
}