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
        if (GameController.started)
        {
            timeCounter.text = GameController.time.ToString("F2") + "\n<size=60%>seconds";
        }
	}
}
