using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float gameTimeLimit = 60f; // 1 minute

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
}
