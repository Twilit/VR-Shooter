using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour 
{
    public enum State { Open, Transition, Closed };

    public Material red;
    public Material yellow;

    public State currentState;
    public float delay;
    public float openSpeed;

    Quaternion closedAngle;
    Quaternion openAngle;

    private void Start()
    {
        closedAngle = Quaternion.Euler(new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z));
        openAngle = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z));

        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = yellow;
    }

    public Target()
    {
        currentState = State.Closed;
        delay = 3f;
        openSpeed = 2f;
    }

    public IEnumerator TargetMoveUp()
    {
        if (currentState == State.Closed)
        {
            currentState = State.Transition;

            for (float t = 0f; t < 1f; t += Time.deltaTime * openSpeed)
            {
                transform.rotation = Quaternion.Slerp(closedAngle, openAngle, t);
                yield return null;
            }

            currentState = State.Open;
            transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = red;
        }

        yield return null;
    }

    public IEnumerator TargetMoveDown()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = yellow;

        if (currentState == State.Open)
        {
            currentState = State.Transition;

            for (float t = 0f; t < 1f; t += Time.deltaTime * 4f)
            {
                transform.rotation = Quaternion.Slerp(openAngle, closedAngle, t);
                yield return null;
            }

            currentState = State.Closed;           
        }

        yield return null;
    }

    public void TargetOn(bool open)
    {
        if (open)
        {
            StartCoroutine("TargetMoveUp");
        }
        else if (!open)
        {
            StartCoroutine("TargetMoveDown");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.transform.GetComponent<Rigidbody>().velocity *= 0.4f; 

            TargetOn(false);
        }
    }
}
