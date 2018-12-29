using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public GameObject gun;

    public GameObject playerMove;
    public GameObject actorEyesScriptCamera;

    public Canvas canvas;
    public Canvas canvasRestart;
    

    public Image currentHealthBar;
    public Text ratioText;

    private const float maxHitpoint = 100;
    public float hitpoint = maxHitpoint;

    public Texture damageindicator;
    private bool take_damage = false;

    public AudioClip medicinesound;
    public AudioSource audioSource;
    public AudioClip takingdamage;

    int sound_cooldown = 0;
    // Use this for initialization
    void Start()
    {
        //wył canvasu restart na starcie
        canvasRestart.GetComponent<Canvas>().enabled = false;
        UpdateHealthBar();
    }
    public void Update()
    {
      
        if (this.name == "Player" && hitpoint<=0)
        {
            if (Input.GetKeyDown(KeyCode.R) == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("loooooool!");
            }
        }
        else if (this.name == "Player" && hitpoint <= 0)
        {
            if (Input.GetKeyDown(KeyCode.O) == true)
            {
                Application.Quit();
            }
        }

        if (sound_cooldown > 0)
        {
            sound_cooldown--;
        }

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
            if(this.name=="Player")
            {
                //ukrycie glownego canvasu
                canvas.GetComponent<Canvas>().enabled = false;
                //pokazanie napisu restartu
                canvasRestart.GetComponent<Canvas>().enabled = true;
                //wył obiektu całego obiektu gun
                gun.SetActive(false);
                //wyl ruchu kamera - wyłączyłbym cały obiekt actorEyes, ale jak przeniose kamere do
                //obiektu player to nie działa poprawnie
                actorEyesScriptCamera.GetComponent<camMouseLook>().enabled = false;
                // wył ruchu gracza
                playerMove.GetComponent<PlayerMovement>().enabled = false;
            }else
                Destroy(gameObject);
            Debug.Log("Dead!");
        }
        UpdateHealthBar();
        if (this.tag == "Player" && sound_cooldown == 0)
            PlaySound(takingdamage);
        sound_cooldown = 40;

    }
    public void TakeHealth(float health)
    {
        float temHealth = maxHitpoint - hitpoint;

        if (health > maxHitpoint)
        {
            health += temHealth;
            PlaySound(medicinesound);
        }
        if (hitpoint < maxHitpoint)
        {
            hitpoint += health;
            PlaySound(medicinesound);
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
    void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();

    }

    void OnGUI()
    {
        if (take_damage == true && this.tag == "Player")
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), damageindicator, ScaleMode.ScaleToFit);
        }
    }
}