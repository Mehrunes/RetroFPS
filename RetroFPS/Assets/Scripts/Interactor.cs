using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour 
{
	public const float reach = 10f;

    public AudioClip audioClip;
    public AudioSource audioSource;

    void Start () 
	{
		
	}
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			var ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, reach))
			{
                var interactedObject = hit.collider.GetComponent<INteracts>();
                if(interactedObject != null) 
                {
                    interactedObject.OnUse();
                    PlaySound();
                }
                
			}
		}
	}
    void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();

    }
}
