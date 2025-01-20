using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject agent;
    public int numberOfAgents;
    public float timeBetweenSpawns;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAgents());
    }

    public IEnumerator SpawnAgents()
    {
        while (numberOfAgents != 0)
        {
            GoToTarget spawned = Instantiate(agent.GetComponent<GoToTarget>());
            spawned.target = transform;
            numberOfAgents -= 1;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }


}
