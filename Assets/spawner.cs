using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class spawner : MonoBehaviour
{

    enum spawnState { SPAWNING, WAITING, COUNTING };

    spawnState state = spawnState.COUNTING;

    public GameObject enemyPrefablv1;
    public GameObject enemyPrefablv2;
    public GameObject spinnePrefab;


    public Text enemiesText;

    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> enemies = new List<GameObject>();
    List<List<GameObject>> waves = new List<List<GameObject>>();

    public Text roundtext;

    public CanvasGroup nextRoundGroup;
    public Text nextRoundTimer;

    int round = 1;
    float timePause = 30.0f;
    float timePauseMax = 30.0f;

    int enemiesSum = 0;
    int currentEnemiesSum = 0;


    void spawn(Vector3 position)
    {
        Instantiate(enemyPrefablv1).transform.position = position;



    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<audioManager>().play("background");

        spawnPoints.Add(GameObject.Find("spawnPointN").transform);
        spawnPoints.Add(GameObject.Find("spawnPointE").transform);
        spawnPoints.Add(GameObject.Find("spawnPointS").transform);
        spawnPoints.Add(GameObject.Find("spawnPointW").transform);

        state = spawnState.COUNTING;
        roundtext.text = "Round " + round.ToString();
        nextRoundGroup.alpha = 0f;
        nextRoundTimer.text = Mathf.Round(timePause).ToString();


    }



    // Update is called once per frame
    void Update()
    {
        //enemies.Remove(enemies.Find(x => x == null));

        //spawnEnemies();


        if (enemies.Count <= 0 && state != spawnState.COUNTING)
        {

            timePause -= Time.deltaTime;
            nextRoundGroup.alpha = 1f;
            nextRoundTimer.text = Mathf.Round(timePause).ToString();
            if (timePause <= 0)
            {
                round++;

                roundtext.text = "Round " + round.ToString();

                nextRoundGroup.alpha = 0f;

                state = spawnState.COUNTING;
                timePause = timePauseMax;
            }



        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

            //Vector3 adjustZ = new Vector3(wp.x, wp.y, enemyPrefablv1.transform.position.z);

            //spawn(adjustZ);

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        /*+Debug.Log("collision.gameObject " + collision.gameObject.tag+ " -------- " + " enemyPrefab : " + enemyPrefab.gameObject.tag);*/
        if (collision.gameObject.layer == 8)
        {

            Collider2D[] c2d = this.GetComponents<Collider2D>();

            for (int i = 0; i < c2d.Length; i++)
            {
                Physics2D.IgnoreCollision(collision.collider, c2d[i]);
            }

        }
    }

    IEnumerator spawn(int lv1, int lv2, int spinneAnz)
    {
        state = spawnState.SPAWNING;

        Statistik.setHighestWave(round);

        int zombielv1BossCounter = 0;

        enemiesSum = lv1 + lv2 + spinneAnz;
        currentEnemiesSum = enemiesSum;
        enemiesText.text = currentEnemiesSum + "/" + enemiesSum;


        int lv1Enemy = 0;
        int lv2Enemy = 0;
        int spinneEnemy = 0;

        int howManyEnemiesGrp = Random.Range(3, 7);
        int howManyEnemiesGrpAtM = 0;

        int range1 = 0;
        int range2 = 3;

        while (true)
        {
            int rnd = Random.Range(range1, range2);
            if (enemiesSum == (lv1Enemy + lv2Enemy + spinneEnemy))
            {
                break;
            }

            switch (rnd)
            {
                case 0:
                    if (lv1Enemy == lv1)
                    {
                        break;
                    }
                    int r1 = Random.Range(0, 4);
                    GameObject enemylv1 = Instantiate(enemyPrefablv1, spawnPoints[r1].transform.position, Quaternion.identity);
                    // enemylv1.GetComponent<AIPath>().canSearch = true;
                    /*GameObject enemylv1 =  ObjectPooler.SharedInstance.GetPooledObject("zombielv1");
                     if (enemylv1 != null)
                     {
                         enemylv1.transform.position = spawnPoints[r1].transform.position;
                         enemylv1.transform.rotation = Quaternion.identity;
                         enemylv1.SetActive(true);
                     }*/
                    enemylv1.GetComponent<EnemyAI>().health = enemylv1.GetComponent<EnemyAI>().startHealth + round;
                    Debug.Log(enemylv1.GetComponent<EnemyAI>().health);

                    if (round == 5)
                    {
                        if (zombielv1BossCounter < 1)
                        {
                            enemylv1.GetComponent<zombielv1>().setBossStats();
                            zombielv1BossCounter++;
                        }

                    }

                    enemies.Add(enemylv1);
                    lv1Enemy++;
                    howManyEnemiesGrpAtM++;
                    break;
                case 1:
                    if (lv2Enemy == lv2)
                    {
                        break;
                    }
                    int r2 = Random.Range(0, 4);
                    GameObject enemylv2 = Instantiate(enemyPrefablv2, spawnPoints[r2].transform.position, Quaternion.identity);
                    /*GameObject enemylv2 = ObjectPooler.SharedInstance.GetPooledObject("zombielv2");
                    if (enemylv2 != null)
                    {
                        enemylv2.transform.position = spawnPoints[r2].transform.position;
                        enemylv2.transform.rotation = Quaternion.identity;
                        enemylv2.SetActive(true);
                    }*/
                    enemylv2.GetComponent<EnemyAI>().health = enemylv2.GetComponent<EnemyAI>().startHealth + round;

                    enemies.Add(enemylv2);
                    lv2Enemy++;
                    howManyEnemiesGrpAtM++;
                    break;

                case 2:
                    if (spinneEnemy == spinneAnz)
                    {
                        break;
                    }
                    int r3 = Random.Range(0, 4);
                    GameObject spinne = Instantiate(spinnePrefab, spawnPoints[r3].transform.position, Quaternion.identity);
                    /*GameObject spinne = ObjectPooler.SharedInstance.GetPooledObject("spinne");
                    if (spinne != null)
                    {
                        spinne.transform.position = spawnPoints[r3].transform.position;
                        spinne.transform.rotation = Quaternion.identity;
                        spinne.SetActive(true);
                    }*/
                    spinne.GetComponent<EnemyAI>().health = spinne.GetComponent<EnemyAI>().startHealth + round;

                    enemies.Add(spinne);
                    spinneEnemy++;
                    howManyEnemiesGrpAtM++;
                    break;
                default:
                    break;
            }

            if (howManyEnemiesGrp == howManyEnemiesGrpAtM)
            {
                howManyEnemiesGrp = Random.Range(1, enemiesSum / 4);
                yield return new WaitForSeconds(2f);
            }

        }


        yield break;
    }

    public void deleteEnemy(int hash)
    {
        enemies.Remove(enemies.Find(x => x.GetHashCode() == hash));
    }

    public void enemyMinusMinus()
    {
        currentEnemiesSum--;
        enemiesText.text = currentEnemiesSum + "/" + enemiesSum;
    }


    void spawnEnemies()
    {
        if (round == 1 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 0, 0));

        }
        if (round == 2 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 1, 0));
        }
        if (round == 3 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 2, 0));

        }
        if (round == 4 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(9, 4, 0));

        }
        if (round == 5 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 4, 0));

        }
        if (round == 6 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(8, 6, 1));

        }
        if (round == 7 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(8, 6, 2));

        }
        if (round == 8 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 6, 3));

        }
        if (round == 9 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(7, 10, 4));

        }
        if (round == 10 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 5));

        }
        if (round == 11 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 6));

        }
        if (round == 12 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 7));

        }
        if (round == 13 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 8));

        }
        if (round == 14 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 9));

        }
        if (round == 15 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(10, 10, 10));

        }
        if (round == 16 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(11, 10, 10));

        }
        if (round == 17 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(12, 10, 10));

        }
        if (round == 18 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(13, 10, 10));

        }
        if (round == 19 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(14, 10, 10));

        }
        if (round == 20 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 10, 10));

        }
        if (round == 21 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 11, 10));

        }
        if (round == 22 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 12, 10));

        }
        if (round == 23 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 13, 10));

        }
        if (round == 24 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 14, 10));

        }
        if (round == 25 && state != spawnState.SPAWNING)
        {

            StartCoroutine(spawn(15, 15, 10));

        }
    }


    public void skip()
    {
        timePause = 0.1f;
    }
}
