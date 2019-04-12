using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using TMPro;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    TextMeshProUGUI bulletNumber;
    public Material[] gunColours;
    Material currentColour;

    int bulletCount;
    int maxBullet;

    Transform muzzle;
    LayerMask layermask; 

    SDK_BaseController leftCon;
    SDK_BaseController rightCon;

    bool canShoot;

    void Start()
    {
        currentColour = gunColours[Random.Range(0, 5)];

        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).GetComponent<Renderer>().material = currentColour;
        }

        muzzle = transform.GetChild(1).GetChild(2);

        layermask = LayerMask.GetMask("Bullet", "Interactable");
        layermask = ~layermask;

        if (gameObject.tag == "Pistol")
        {
            maxBullet = 20;
        }
        else if (gameObject.tag == "Rifle")
        {
            maxBullet = 30;
        }
        else if (gameObject.tag == "Shotgun")
        {
            maxBullet = 8;
        }

        bulletCount = maxBullet;

        bulletNumber = transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        UpdateBulletNumber(0);

        canShoot = true;
    }

	void Update () 
	{

	}

    void OldShoot()
    {
        GameObject bulletInstance;
        bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);

        bulletInstance.GetComponent<Rigidbody>().AddForce(muzzle.forward * 3000f);
    }

    public void Shoot(Vector3 bulletOffset)
    {
        if (canShoot && bulletCount > 0)
        {
            RaycastHit hit;

            Vector3 bulletDir = muzzle.forward + bulletOffset;

            if (Physics.Raycast(muzzle.position, bulletDir, out hit, Mathf.Infinity, layermask))
            {
                Debug.DrawRay(muzzle.position, bulletDir * hit.distance, Color.red);

                GameObject bulletInstance;
                bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);

                bulletInstance.GetComponent<Bullet>().StartCoroutine("BulletMoveTarget", hit.point);
            }
            else
            {
                GameObject bulletInstance;
                bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);

                bulletInstance.GetComponent<Bullet>().StartCoroutine("BulletMoveDirection", bulletDir);
            }

            if (gameObject.tag != "Shotgun")
            {
                UpdateBulletNumber(1);
            }

            //gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 5f);
        }
    }

    public void Shoot()
    {
        Shoot(Vector3.zero);

        StartCoroutine("LimitRoF", 400);
    }

    public void RifleShoot(bool on)
    {
        if (on)
        {
            StartCoroutine("AutoFire", 600);
        }
        else
        {
            StopCoroutine("AutoFire");
        }
    }

    IEnumerator AutoFire(float RPM)
    {
        while (true)
        {
            Shoot(Vector3.zero);

            yield return new WaitForSeconds(1 / (RPM / 60));
        }
    }

    public void ShotgunShoot()
    {
        if (canShoot && bulletCount > 0)
        {
            for (int i = 0; i < 9; i++)
            {
                //print("Pellet number " + i);

                Vector3 bulletOffset = Random.insideUnitCircle * 0.03f;
                //print("Bullet offset of " + bulletOffset);


                Shoot(bulletOffset);
            }

            UpdateBulletNumber(1);

            StartCoroutine("LimitRoF", 68);
        }
    }

    IEnumerator LimitRoF(float RPM)
    {
        if (canShoot)
        {
            canShoot = false;

            yield return new WaitForSeconds(1 / (RPM / 60));

            canShoot = true;
        }
    }

    void UpdateBulletNumber(int subtraction)
    {
        bulletCount -= subtraction;
        bulletNumber.SetText("{0}", bulletCount);

        if (bulletCount == maxBullet)
        {
            bulletNumber.color = Color.green;
        }
        else if (bulletCount > 0)
        {
            bulletNumber.color = Color.white;
        }
        else
        {
            bulletNumber.color = Color.red;
        }
    }
}
