using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    public TargetController targetCon;
    public static float time;
    public static float money;
    public static float score;
    public static bool started;    

    public GameController ()
    {
        time = 0;
        money = 0;
        score = 0;
        started = false;
    }


    void Start () 
	{

	}

	void LateUpdate () 
	{
        
	}
}
