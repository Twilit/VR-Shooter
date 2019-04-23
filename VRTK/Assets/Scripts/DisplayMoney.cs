using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    TextMeshProUGUI moneyCounter;

	void Start ()
    {
        moneyCounter = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
	
	void LateUpdate ()
    {
        if (GameController.started)
        {
            moneyCounter.text = "<size=60%>£<size=100%>" + GameController.money;
        }
	}
}
