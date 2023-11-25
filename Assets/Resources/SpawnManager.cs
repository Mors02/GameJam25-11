using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _i;

    public static SpawnManager i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<SpawnManager>("SpawnManager"));
            if (_i.player == null) _i.player = GameObject.FindGameObjectWithTag("Player");

            return _i;
        }
    }

    [SerializeField]
    int Round = 0;

    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    float[] spawnTimer;

    /*
    [SerializeField]
    List<Transform> spawnerLocation;*/

    float cameraDim = 18.5f;
    float arenaDim = 30f;

    [SerializeField]
    GameObject player;

    Transform t;

    public int routineThatEnded = 0;

    public void Start()
    {
        t = SpawnManager.i.player.GetComponent<Transform>();
    }

    public void NewRound()
    {
        //new round, show the new round
        SpawnManager.i.Round++;

        Random.InitState((int)System.DateTime.Now.Ticks);
        //for each enemy we calculate how many based on the round
        //each index represents an enemy type;

        //Debug.Log("Enemies: " + SpawnManager.i.enemyPrefabs.Length);

        for (int i = 0; i < SpawnManager.i.enemyPrefabs.Length; i++)
        {
            //int howManyEnemies = CalculateNumberFromEnemyType(enemyPrefabs[i].name);
            int howManyEnemies = 6;
            StartCoroutine(SpawnManager.i.SpawnEnemy(SpawnManager.i.enemyPrefabs[i], SpawnManager.i.spawnTimer[i], howManyEnemies));
        }
    }

    public void CheckIfNewRoundCanStart()
    {
        //if we call this function it means a routine has ended
        SpawnManager.i.routineThatEnded++;

        //we count how many have ended. Since we started one for each enemyPrefab, if the number
        //that ended is the same (or more idk) then they all ended.
        if (routineThatEnded >= SpawnManager.i.enemyPrefabs.Length)
        {
            Debug.Log("NEW ROUND: " + (SpawnManager.i.Round+1));
            //reset the number
            SpawnManager.i.routineThatEnded = 0;
            //next round;
            SpawnManager.i.NewRound();
        }
    }

    public IEnumerator SpawnEnemy(GameObject enemy, float spawnTimer, int howManyToSpawn)
    {
        //this is called n time for each enemy, where n is the number of enemies this round
        for (int i = 0; i < howManyToSpawn; i++)
        {
            Vector2 pos;
            if (SpawnManager.i.t == null)
                SpawnManager.i.t = SpawnManager.i.player.transform;
            //a cycle to find the position to spawn, must be inside the arena and outside the camera view
            //random point in an annulus (difference between 2 concentric circles) that is scaled by a random amount around the player 
            Vector2 randomDir = (Random.insideUnitCircle).normalized;
            float randomDist = Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim);
            Vector2 origin = new Vector2(SpawnManager.i.t.position.x, SpawnManager.i.t.position.y);

            pos = (origin + randomDir) * randomDist;

            //pos = new Vector2(Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim), Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim))
                
            
            GameObject enemyCreated = Instantiate(enemy, pos, new Quaternion());
            yield return new WaitForSeconds(spawnTimer);
        }

        SpawnManager.i.CheckIfNewRoundCanStart();
    }
    
}
