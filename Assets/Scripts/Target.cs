using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject agent;
    public int numberOfAgents;
    public float timeBetweenSpawns;

    public Transform[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        if (spawns.Length == 0)
        {
            spawns = new Transform[1];
            spawns[0] = transform;
            print("Working");
        }
        StartCoroutine(SpawnAgents());
    }

    public IEnumerator SpawnAgents()
    {
        while (numberOfAgents != 0)
        {
            int randomSpawn = Random.Range(0, spawns.Length);
            GoToTarget spawned = Instantiate(agent.GetComponent<GoToTarget>(), spawns[randomSpawn].position, spawns[randomSpawn].rotation);
            spawned.target = transform;
            numberOfAgents -= 1;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }


}
