using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Game Settings")]
    public float gameTime;
    public float gameTimeLimit = 60f; // 1 minute
    [Header("Player Settings")]
    public int health;
    public int maxHealth = 100;
    public int level = 1;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 550 };
    [Header("References")]
    public Player player;
    public PoolManager poolManager;
    [Header("Orb Settings")]
    public int expOrbPrefabIndex = 3;

    void Awake()
    {
        instance = this;
        health = maxHealth;
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
        GetExp(1); // Default to 1 exp
    }

    public void GetExp(int expAmount)
    {
        exp += expAmount;

        if (exp >= nextExp[level - 1])
        {
            level++;
            exp = 0;
            // Level up logic here, e.g., increase player stats
        }
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
