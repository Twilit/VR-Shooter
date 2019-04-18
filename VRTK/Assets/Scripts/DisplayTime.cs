using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour {

    TextMeshProUGUI timeCounter;
    
	void Start ()
    {
        timeCounter = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
	}
	
	void LateUpdate ()
    {
        timeCounter.text = GameController.time + " <size=60%>seconds";
	}
}
