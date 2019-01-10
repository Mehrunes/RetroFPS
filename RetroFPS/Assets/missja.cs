using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missja : MonoBehaviour
{
    public string flowtext = "";
    public Text ObjText;
    // Use this for initialization

    /* IEnumerator ExecuteAfterTime(float time)
     {
         yield return new WaitForSeconds(time);

         // Code to execute after the delay
     }*/

    void ObjectiveText()
    {
        ObjText.text = flowtext;

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            ObjectiveText();
            //ExecuteAfterTime(1);
            Destroy(this.gameObject);
        }
    }
}
