using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Game Settings")]
    public bool isLive;
    public float gameTime;
    public float gameTimeLimit = 60f; // 1 minute
    [Header("Player Settings")]
    public int playerIndex = 0;
    public float health;
    public float maxHealth = 100;
    public int level = 1;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 550 };
    [Header("References")]
    public Player player;
    public PoolManager poolManager;
    [Header("Orb Settings")]
    public int expOrbPrefabIndex = 3;
    [Header("UI Settings")]
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }
    public void GameStart(int id)
    {
        playerIndex = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        isLive = true;
        Resume();

        AudioManager.instance.PlayBGM(true);
        AudioManager.instance.PlaySFX(SFX.Select);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Game Over");

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(SFX.Lose);
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(SFX.Win);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive) return;
        gameTime += Time.deltaTime;
        if (gameTime > gameTimeLimit)
        {
            gameTime = gameTimeLimit;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive) return;

        GetExp(1); // Default to 1 exp
    }

    public void GetExp(int expAmount)
    {
        exp += expAmount;

        if (exp >= nextExp[level - 1])
        {
            level++;
            exp = 0;

            AudioManager.instance.PlaySFX(SFX.LevelUp);
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public void SpawnExpOrb(Vector3 position, int expValue = 1)
    {

        GameObject prefabToSpawn = poolManager.prefabs[expOrbPrefabIndex];

        GameObject expOrb = poolManager.Get(expOrbPrefabIndex);
        if (expOrb == null)
        {
            GetExp(expValue);
            return;
        }

        expOrb.transform.position = position;

        ExpOrb orbComponent = expOrb.GetComponent<ExpOrb>();
        orbComponent.expValue = expValue;
    }
}
