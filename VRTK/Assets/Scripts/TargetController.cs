using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour 
{
    [SerializeField]
    Target[] targets;

    List<Target> openTargets = new List<Target>();
    public float targetOpenRate = 2f;

    void Start()
    {
        StartCoroutine("GetOpenTargets");
    }

	void Update ()
	{
        
	}

    IEnumerator GetOpenTargets()
    {
        while (true)
        {
            openTargets.Clear();

            for (int i = 0; i < targets.Length; i ++)
            {
                if (targets[i].currentState == Target.State.Closed)
                {
                    openTargets.Add(targets[i]);
                }
            }

            if (openTargets.Count != 0)
            {
                openTargets[Random.Range(0, openTargets.Count - 1)].TargetOn(true);
            }

            yield return new WaitForSeconds(targetOpenRate);
        }
    }
}
