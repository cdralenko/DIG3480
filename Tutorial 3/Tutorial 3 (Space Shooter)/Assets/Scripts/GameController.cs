using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject pickup;
    public Vector3 spawnValues;
    public Vector3 pspawnValues;
    public int hazardCount;
    public int pickupCount;
    public float spawnWait;
    public float pspawnWait;
    public float startWait;
    public float pstartWait;
    public float waveWait;
    public float pwaveWait;

    public AudioClip victory;
    public AudioClip loss;
    public AudioSource musicSource;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text easyText;

    private bool gameOver;
    private bool restart;
    private bool winner;
    private bool noise;

    private int score;

    private void Start()
    {
        gameOver = false;
        restart = false;
        winner = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        easyText.text = "SELECT DIFFICULTY: \r\n \r\n press E for EASY \r\n or press H for HARD";
        score = 0;
        UpdateScore();
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
        }
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (score >= 100)
        {
            winText.text = "WINNER! Game created by Ainslee Flowers";
            winner = true;
            Destroy(GameObject.FindWithTag("Enemy"));
            Destroy(GameObject.FindWithTag("Pickup"));
            WinWin();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpawnWaves());
            StartCoroutine(PickupSpawn());
            easyText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(SpawnWaves());
            StartCoroutine(PickupSpawn());
            easyText.text = "";
            hazardCount = 30;
            spawnWait = 0.15f;
        }

    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(startWait);
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press SPACE to RESTART";
                restart = true;
                break;
            }

            if (winner)
            {
                restartText.text = "Press SPACE to PLAY AGAIN";
                restart = true;
                break;
            }

        }
    }

    IEnumerator PickupSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(pstartWait);
            for (int i = 0; i < pickupCount; i++)
            {
                Vector3 pspawnPosition = new Vector3(Random.Range(-pspawnValues.x, pspawnValues.x), pspawnValues.y, pspawnValues.z);
                Quaternion pspawnRotation = Quaternion.identity;
                Instantiate(pickup, pspawnPosition, pspawnRotation);
                yield return new WaitForSeconds(pspawnWait);
            }
            yield return new WaitForSeconds(pwaveWait);

            if (gameOver)
            {
                break;
            }

            if (winner)
            {
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER!";
        gameOver = true;
        winner = false;
        musicSource.clip = loss;
        musicSource.Play();
    }

    public void WinWin()
    {
        if (winner == true && noise == false)
        {
            musicSource.clip = victory;
            musicSource.Play();
            noise = true;
        }
    }

}
