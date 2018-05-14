using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{

    public Image currentHealthBar;
    public Text ratioText;

    public float hitpoint =100;
    private float maxHitpoint = 100;
    // Use this for initialization
    void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void UpdateHealthBar()
    {
        float ratio = (hitpoint / maxHitpoint);//%hp

        if (ratio >= 0)
        {
            currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
            ratioText.text = (ratio * 100).ToString() + "%";
        }
    }
    public void TakeDamage(float demage)
    {
        hitpoint -= demage;
        int hitpoint_int = (int)hitpoint;
        if (hitpoint_int < 0)
        {
            hitpoint = 0;
            hitpoint_int = 0;
            Debug.Log("Dead!");
        }
        UpdateHealthBar();
    }
    
}