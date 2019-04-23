using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyChickenScript : MonoBehaviour
{

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Oar")
        {
            if (!GameController.started)
            {
                GameController.time = 90;
                GameController.score = 0;
                GameController.money = 0;
                GameController.started = true;
            }
            else
            {
                GameController.money += 10;
            }            
        }
    }
}
