using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;



public class MaulSaber : VRTK_InteractableObject
{
    GameObject topBlade;
    GameObject bottomBlade;

    Vector3 startsize = new Vector3(0, 0, 0);
    Vector3 endsize = new Vector3(0.8f, 2.6f, 0.8f);

    void Start()
    {
        topBlade = transform.GetChild(0).GetChild(0).gameObject;
        bottomBlade = transform.GetChild(1).GetChild(0).gameObject;

        topBlade.transform.localScale = startsize;
        bottomBlade.transform.localScale = startsize;
    }

    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);

        topBlade.transform.localScale = endsize;
        bottomBlade.transform.localScale = endsize;
    }

    public override void StopUsing(VRTK_InteractUse previousUsingObject = null, bool resetUsingObjectState = true)
    {
        base.StopUsing(previousUsingObject, resetUsingObjectState);

        topBlade.transform.localScale = startsize;
        bottomBlade.transform.localScale = startsize;
    }
}
