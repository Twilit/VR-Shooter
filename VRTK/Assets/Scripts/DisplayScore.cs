using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour {

    TextMeshProUGUI scoreCounter;

    void Start()
    {
        scoreCounter = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate()
    {
        scoreCounter.text = "<size=50%>SCORE\n<size=130%>" + GameController.score;
    }
}
