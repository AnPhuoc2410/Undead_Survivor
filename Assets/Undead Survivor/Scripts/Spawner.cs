using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    float timer;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();//Index 0 is parent
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; //Start from 1 cause the transform index 0 is the parent


    }
}
