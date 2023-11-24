using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class waypointSpawner : MonoBehaviour
{
    public static waypointSpawner instance;
    public  int EnemiesAlibe = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    public TMP_Text waveCountDownText;
    public TMP_Text waveNumberText;
    public TMP_Text countEnemyText;
    public Wave wave;

    public float timeBetweenWave = 5f;
    public float countdown = 5f;
    private int waveNumber = 0;

    public TMP_Text allEnemy;

    GameObject[] FindEnemy;
    bool Dosomthing;
    int waveForText;
    public int enemyCount;

    [SerializeField]
    Animator anim_portal;
    float count = 0;

    private bool canspawn = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        EnemiesAlibe = 1;
        waveNumberText.text = "WAVE " + 1 + "/" + waves.Length;

        anim_portal.SetBool("Open",false);
        if (canspawn)
        {
            countdown -= Time.deltaTime;
        }
    }


    private void FixedUpdate()
    {
        FindEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        
    }

    private void Update()
    {
        /*if(EnemiesAlibe > 0)
        {
            Debug.Log("Test EnemAlive  " );
            return;
        }*/
        if (waveNumber == waves.Length && enemyCount <= 0)
        {
            
            GameManager.gameWin = true;
            this.enabled = false;
        }

        if (countdown <= 0f && canspawn)
        {
            EnemiesAlibe = 1;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
            waveForText += 1;
            waveNumberText.text = "WAVE " + waveForText.ToString() + "/" + waves.Length;
            return;
        }
        if (canspawn && enemyCount <= 0)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 2 && canspawn)
        {
            FindObjectOfType<SoundManager>().PlaySounded("WarpPortal");
            anim_portal.SetBool("Open", true);
            //count += 1f * Time.deltaTime;
        }

        /*if (count >= timeBetweenWave)
        {
            count += 1f * Time.deltaTime;
            count = 0;
        }*/

        if (waveForText == waveNumber)
        {
            waveForText = waveNumber;
        }
      
        /*if (enemyCount <= 0)
        {
            enemyCount = 0;
            allEnemy.text = 0 + "/" + wave.count;
        }*/

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountDownText.text = "Time:"+Mathf.Round(countdown).ToString();
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        wave = waves[waveNumber];

        enemyCount = wave.count;
        
        anim_portal.SetBool("Open", true);
        allEnemy.text = wave.count + "/" + wave.count;
        for (int i = 1; i <= wave.count; i++)
        {         
            SpawnEnemy(wave.enemy);
            canspawn = true;
            
            yield return new WaitForSeconds(1f / wave.rate);
        }

        FindObjectOfType<SoundManager>().PlaySounded("WarpPortal");
        anim_portal.SetBool("Open", false);
        waveNumber++;
       // Debug.Log("wave number : " + waveNumber);
        //Debug.Log("wave lenght : " + waves.Length);

        if (waveNumber == waves.Length)
        {
            canspawn = false;
            //Dosomthing = true;
            //yield return new WaitForSeconds(3f);
            /*if(enemyCount == 0 && Dosomthing)
            {
                GameManager.gameWin = true;
            }*/
            //this.enabled = false;
            //Debug.Log("LEVEL WON !");
        }
        //Debug.Log("EnemiesAlibe : " + EnemiesAlibe);
    }

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
        EnemiesAlibe++;
        Debug.Log("EnemiesAlibe : " + EnemiesAlibe);
        canspawn = false;
    }

    public void UpdateEnemyCountText()
    {
        if (enemyCount < 0)
        {
            return;
        }
        allEnemy.text = enemyCount + "/" + wave.count;
    }
}
