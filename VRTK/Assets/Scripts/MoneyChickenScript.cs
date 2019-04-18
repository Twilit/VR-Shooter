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
            GameController.money += 10;
        }
    }
}
