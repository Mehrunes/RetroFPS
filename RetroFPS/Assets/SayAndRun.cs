using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayAndRun : MonoBehaviour {

    // Use this for initialization
    public string flowtext = "";
    public AudioSource CoMaMowic;
    public AudioClip kwestia;
    public bool deletus = true;
    // Use this for initialization

    /* IEnumerator ExecuteAfterTime(float time)
     {
         yield return new WaitForSeconds(time);

         // Code to execute after the delay
     }*/

    void Powiedz()
    {
        CoMaMowic.clip = kwestia;
        CoMaMowic.Play();

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            Powiedz();
                Destroy(this.gameObject);
        }
    }
}
