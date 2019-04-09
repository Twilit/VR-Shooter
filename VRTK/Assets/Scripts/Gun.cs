using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    VRTK_ControllerEvents leftCon;
    VRTK_ControllerEvents rightCon;
    Transform muzzle;
    LayerMask layermask;

    bool canShoot;

    bool leftHeld;
    bool rightHeld;

    bool LeftHeld
    {
        get
        {
            return leftHeld;
        }

        set
        {
            leftHeld = value;
        }
    }

    bool RightHeld
    {
        get
        {
            return rightHeld;
        }

        set
        {
            rightHeld = value;
        }
    }

    bool triggerIsPressed;

    bool TriggerIsPressed
    {
        get
        {
            return triggerIsPressed;
        }

        set
        {
            if (value == true && triggerIsPressed != true)
            {
                Shoot(Vector3.zero);
            }

            triggerIsPressed = value;
        }
    }

    void Start()
    {
        muzzle = transform.GetChild(1).GetChild(2);

        leftCon = GameObject.FindGameObjectWithTag("LeftController").GetComponent<VRTK_ControllerEvents>();
        rightCon = GameObject.FindGameObjectWithTag("RightController").GetComponent<VRTK_ControllerEvents>();

        layermask = LayerMask.GetMask("Bullet", "Interactable");
        layermask = ~layermask;

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

    void IgnoreThisRubbish()
    {
        LeftHeld = GetComponent<VRTK_InteractableObject>().IsGrabbed(grabbedBy: leftCon.gameObject);
        RightHeld = GetComponent<VRTK_InteractableObject>().IsGrabbed(grabbedBy: rightCon.gameObject);

        if ((leftCon.triggerPressed && leftHeld) ||
            (rightCon.triggerPressed && rightHeld))
        {
            TriggerIsPressed = true;
        }
        else
        {
            TriggerIsPressed = false;
        }
    }

    public void Shoot(Vector3 bulletOffset)
    {
        if (canShoot)
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
        for (int i = 0; i < 9; i++)
        {
            //print("Pellet number " + i);

            Vector3 bulletOffset = Random.insideUnitCircle * 0.03f;
            //print("Bullet offset of " + bulletOffset);


            Shoot(bulletOffset);
        }

        StartCoroutine("LimitRoF", 68);
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
}
