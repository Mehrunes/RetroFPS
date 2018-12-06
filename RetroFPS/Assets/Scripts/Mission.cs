using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mission : MonoBehaviour,IMission {
    public int CrnObj = 0;
    public Text ObjText;
    // Use this for initialization
    void ObjectiveOne()
    {
        ObjText.text = "Idź do sklepu i weź bułki";
    }

    void ObjectiveTwo()
    {
        ObjText.text = "Wróź do domu";

    }

    void QuestCompleted()
    {
        ObjText.text = "Misja wykonana";
    }

    public void NextObjective()
    {
        if (CrnObj==0) { ObjectiveTwo(); }
        if (CrnObj == 1) { QuestCompleted(); }
        CrnObj++;

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player"&&CrnObj==1) { NextObjective(); }
    }
}
