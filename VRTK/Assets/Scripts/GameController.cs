using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour 
{
    public TargetController targetCon;
    public TextMeshProUGUI tutorial;

    public static float time;
    public static float money;
    public static float score;
    public static bool started;    

    public GameController ()
    {
        time = 90;
        money = 0;
        score = 0;
        started = false;
    }


    void Start () 
	{

	}

    void Update()
    {
        if (started && time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time <= 0)
        {
            time = 0;
            started = false;
        }

        TargetRiseRates();
    }

    void LateUpdate () 
	{
        if (started)
        {
            tutorial.text = "";
        }
        else if (!started && score != 0)
        {
            tutorial.text = "Your final score is:" + score + "\n<size=70%>Hit the chicken to restart!";
        }
	}

    void TargetRiseRates()
    {
        if (time > 60)
        {
            targetCon.targetOpenRate = 2f;
        }
        else if (time > 40)
        {
            targetCon.targetOpenRate = 1.5f;
        }
        else if (time > 20)
        {
            targetCon.targetOpenRate = 1f;
        }
        else if  (time > 10)
        {
            targetCon.targetOpenRate = 0.5f;
        }
    }
}
