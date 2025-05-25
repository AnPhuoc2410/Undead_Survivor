using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Game Settings")]
    public float gameTime;
    public float gameTimeLimit = 60f; // 1 minute
    [Header("Player Settings")]
    public int level = 0;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 550 };
    [Header("References")]
    public Player player;
    public PoolManager poolManager;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > gameTimeLimit)
        {
            gameTime = gameTimeLimit;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp >= nextExp[level - 1])
        {
            level++;
            exp = 0;
            // Level up logic here, e.g., increase player stats
        }
    }
}
