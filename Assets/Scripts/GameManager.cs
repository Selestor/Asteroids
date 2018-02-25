using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PhonePanel; 
    public GameObject asteroidSmall;
    public GameObject asteroidMedium;
    public GameObject asteroidLarge;
    public GameObject player;
    public AudioClip explosion;
    public GameObject explosionFX;
    public GameObject playerSpawnFX;
    public AudioClip playerSpawnAudio;
 
    public Image lifeImage;
    public Text scoreText;
    public Transform asteroidHolder;
    public static GameManager instance = null;

    public GameObject playerInstance;
    private GameObject lifePanel;

    private int score;
    private int level;

    public int life = 3;
    public int scoreBonus;

    private void Update()
    {
        if (asteroidHolder != null && asteroidHolder.transform.childCount == 0)
        {
            AdjustScore(level * scoreBonus);
            level++;
            SpawnAsteroids();
        }
    }

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        lifePanel = GameObject.FindWithTag("LifePanel");

        for (int i = 0; i < life; i++)
        {
            Instantiate(lifeImage, lifePanel.transform);
        }
        level = 1;

#if UNITY_ANDROID || UNITY_IOS
              PhonePanel.SetActive(true);
#else
        PhonePanel.SetActive(false);
#endif

        SetupGame();
    }

    void SetupGame()
    {
        asteroidHolder = new GameObject("Asteroids").transform;
        RefreshScoreText();
        SpawnAsteroids();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        playerInstance = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(playerSpawnFX, playerInstance.transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(playerSpawnAudio, Camera.main.transform.position);
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < level; i++)
        {
            int coinToss = Random.Range(0, 1);
            float screenPos = Random.Range(0f, 1f);
            Vector2 asteroidSpawnPosition;

            if (coinToss == 0)
            {
                asteroidSpawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, screenPos));
            }
            else
            {
                asteroidSpawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(screenPos, 0));
            }
            GameObject instance = Instantiate(asteroidLarge, asteroidSpawnPosition, Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            instance.transform.SetParent(asteroidHolder);
        }
    }

    public void GotHit()
    {
        Instantiate(explosionFX, playerInstance.transform.position, Quaternion.identity);
        foreach (Transform child in asteroidHolder.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(asteroidHolder.gameObject);
        Destroy(playerInstance.gameObject);
        StartCoroutine(DelayedSpawn());
    }

    public void SubstractLife()
    {
        life--;
        AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position);
        level = 1;
        Destroy(lifePanel.transform.GetChild(0).gameObject);
        if (life <= 0 )
        {
            Invoke("GameOver", 1.5f);
        }
    }

    public void AdjustScore(int points)
    {
        score += points;
        RefreshScoreText();
    }

    void RefreshScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Destroy(gameObject);
        StoreHighscore(score);
        LoadGameOverScene(2);
    }

    public void LoadGameOverScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void StoreHighscore(int newHighscore)
    {
        PlayerPrefs.SetInt("score", newHighscore);
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore)
            PlayerPrefs.SetInt("highscore", newHighscore);
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(2);
        SetupGame();
    }
}
