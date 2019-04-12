using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;
using VRTK.Controllables;

public class GunVendor : ControllableReactor
{
    public Transform gunSpawnPoint;
    public GameObject Pistol;

    protected override void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        base.MaxLimitReached(sender, e);

        Instantiate(Pistol, gunSpawnPoint.position, Quaternion.identity);
    }
}
