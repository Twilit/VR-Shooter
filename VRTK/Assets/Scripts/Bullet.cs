using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    float speed = 50f;
    Vector3 start;

    Rigidbody rb;

    //use static variable for bullet count

    void Awake ()
	{
        start = transform.position;
        rb = GetComponent<Rigidbody>();
	}	

	void Update () 
	{
        
	}

    void PhysicsOn()
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.velocity = (transform.forward + (Random.insideUnitSphere * 0.1f)) * speed;

            StartCoroutine("Decay", 70f);
        }
    }

    IEnumerator BulletMoveTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }

        PhysicsOn();
    }

    IEnumerator BulletMoveDirection(Vector3 direction)
    {
        while (true)
        {
            transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            //print(collision.gameObject.name);
            PhysicsOn();
        }
    }

    IEnumerator Decay(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
