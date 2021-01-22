using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float minTime, maxTime; 
    float timeCount, spawnTime;
    public GameObject enemyPrefab1, enemyPrefab2;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount+= Time.deltaTime;

        if(timeCount >= spawnTime){
            
            spawnTime = Random.Range(minTime, maxTime);
            if(Random.Range(0,100) <= 65){
                Instantiate(enemyPrefab2, transform.position, transform.rotation);
            }
            else{
                Instantiate(enemyPrefab1, transform.position, transform.rotation);
            }
            timeCount = 0f;
        }
    }
}
