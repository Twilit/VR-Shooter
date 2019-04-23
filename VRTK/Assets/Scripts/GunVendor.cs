using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;
using VRTK.Controllables;

public class GunVendor : ControllableReactor
{
    public Transform gunSpawnPoint;
    public GameObject Gun;
    float gunCost;

    private void Start()
    {
        if (Gun.tag == "Pistol")
        {
            gunCost = 50;
        }
        else if (Gun.tag == "Rifle")
        {
            gunCost = 270;
        }
        else if (Gun.tag == "Shotgun")
        {
            gunCost = 120;
        }
    }

    protected override void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        base.MaxLimitReached(sender, e);        

        if (/*GameController.started && */GameController.money >= gunCost)
        {
            Instantiate(Gun, gunSpawnPoint.position, Quaternion.identity);
            GameController.money -= gunCost;
        }
    }
}
