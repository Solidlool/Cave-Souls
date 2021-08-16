using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{

    public GameObject[] spawnPoints;


    public enum SpawnType { Random, OneByOne}
    public SpawnType spawnType;
    public string toSpawnResourceName = "Enemy";
    public int numberOfObjectsToSpawnOnContact = 1;
    public int maxGameobjectsToSpawn = 5;
    private int nextSpawnPointIndex = 0;
    private int spawnedObjects = 0;
    private GameObject toSpawn;
    public GameObject Ghost;

    


    // Start is called before the first frame update
    void Start()
    {
        toSpawn = Ghost;
    }

    // Update is called once per frame
    void Update()
    {
     

            if (spawnedObjects >= maxGameobjectsToSpawn)
            {

                Destroy(this.gameObject);

            }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                
            SpawnAnObject();

        }
    }
    private void SpawnAnObject()
    {

        Invoke("SpawnAnObject", 3);

        if (spawnedObjects >= maxGameobjectsToSpawn)
        {
            return;

        }

        for (int i = 0; i < numberOfObjectsToSpawnOnContact; i++)
        {
            GameObject spawnPoint = spawnPoints[0];


            if (spawnType == SpawnType.Random)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            }
            else if (spawnType == SpawnType.OneByOne)
            {
                spawnPoint = spawnPoints[nextSpawnPointIndex];
                nextSpawnPointIndex++;
                if (nextSpawnPointIndex >= spawnPoints.Length)
                    nextSpawnPointIndex = 0;
            }

            Instantiate(toSpawn, spawnPoint.transform.position, Quaternion.identity);
            spawnedObjects++;

        }



    }
}
