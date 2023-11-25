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
            //_i.itemsInPlay.Clear();

            return _i;
        }
    }

    [SerializeField]
    int Round = 0;

    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    float[] spawnTimer;

    [SerializeField]
    UIManager ui;

    /*
    [SerializeField]
    List<Transform> spawnerLocation;*/

    [SerializeField]
    Collider2D wallCollider;

    float cameraDim = 18.5f;
    float arenaDim = 25f;

    [SerializeField]
    GameObject player;

    Transform t;

    public HashSet<int> itemsInPlay;


    public int routineThatEnded = 0;

    public void Start()
    {
        t = SpawnManager.i.player.GetComponent<Transform>();
    }

    public void NewRound()
    {
        //new round, show the new round
        SpawnManager.i.Round++;
        SpawnManager.i.ui.ChangeRound(SpawnManager.i.Round);

        Random.InitState((int)System.DateTime.Now.Ticks);
        //for each enemy we calculate how many based on the round
        //each index represents an enemy type;

        //Debug.Log("Enemies: " + SpawnManager.i.enemyPrefabs.Length);

        for (int i = 0; i < SpawnManager.i.enemyPrefabs.Length; i++)
        {
            //int howManyEnemies = CalculateNumberFromEnemyType(enemyPrefabs[i].name);
            int howManyEnemies = 3;
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
            //reset the number
            SpawnManager.i.routineThatEnded = 0;
            //next round;
            SpawnManager.i.NewRound();
        }
    }

    public static bool IsInside(Collider2D c, Vector3 point)
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(point, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("WallCollider"))
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator SpawnEnemy(GameObject enemy, float spawnTimer, int howManyToSpawn)
    {
        //this is called n time for each enemy, where n is the number of enemies this round
        for (int i = 0; i < howManyToSpawn; i++)
        {
            Vector2 pos;
            //a cycle to find the position to spawn, must be inside the arena and outside the camera view
            /*do
            {*/
                //random point in an annulus (difference between 2 concentric circles) that is scaled by a random amount around the player 
            Vector2 randomDir = (Random.insideUnitCircle * SpawnManager.i.t.position).normalized;
            float randomDist = Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim);
            Vector2 origin = new Vector2(SpawnManager.i.t.position.x, SpawnManager.i.t.position.y);

            pos = origin + randomDir * randomDist;
                
            //pos = new Vector2(Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim), Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim))
            //} while (!IsInside(SpawnManager.i.wallCollider, pos) && securityValue <= 20);
            
            GameObject enemyCreated = Instantiate(enemy, pos, new Quaternion());
            yield return new WaitForSeconds(spawnTimer);
        }

        SpawnManager.i.CheckIfNewRoundCanStart();
    }

    public IEnumerator SpawnDrops()
    {
        while(SpawnManager.i.spawnDrops)
        {
            Vector2 pos;
            int securityValue = 0;
            //a cycle to find the position to spawn, must be inside the arena and outside the camera view
            do
            {
                //random point in an annulus (difference between 2 concentric circles) that is scaled by a random amount around the player 
                Vector2 randomDir = (Random.insideUnitCircle * SpawnManager.i.t.position).normalized;
                float randomDist = Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim);
                Vector2 origin = new Vector2(SpawnManager.i.t.position.x, SpawnManager.i.t.position.y);

                pos = origin + randomDir * randomDist;

                //pos = new Vector2(Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim), Random.Range(SpawnManager.i.cameraDim, SpawnManager.i.arenaDim))
                Debug.Log(pos);
                securityValue++;
            } while (!IsInside(SpawnManager.i.wallCollider, pos) && securityValue <= 20);

            //create the drop
            GameObject drop = Instantiate(GameAssets.i.dropPrefab, pos, new Quaternion());
            //find the id to create
            int id;
            int count = 0;            

            do
            {
                id = Random.Range(0, ItemGeneratorMap.itemCount);
                count++;
            //check if its an item that already exists
            } while (itemsInPlay.Contains(id) || count <= 20);
                        
            //create the item
            if (drop != null)
            {
                drop.GetComponent<DropBehaviour>().Setup(id);
            }

            printItemsInPlay();

            yield return new WaitForSeconds(SpawnManager.i.dropTimer);
        }
    }

    void printItemsInPlay()
    {
        string items = "";

            foreach (int id in itemsInPlay)
            {
                items += id + " ";
            }
        Debug.Log(items);
    }
}
