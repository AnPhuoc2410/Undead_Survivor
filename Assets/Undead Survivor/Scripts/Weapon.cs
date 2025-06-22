using System;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;
    private float cycleTimer;
    private bool isWeaponActive;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive) return; // Check if the game is live

        switch (id)
        {
            case 0:
                // Simple cycling: 3 seconds on, 2 seconds off
                cycleTimer += Time.deltaTime;

                if (cycleTimer < 3f) // Active for 3 seconds
                {
                    transform.Rotate(speed * Time.deltaTime * Vector3.back);
                    if (!isWeaponActive)
                    {
                        isWeaponActive = true;
                        SetWeaponVisibility(true);
                    }
                }
                else if (cycleTimer < 5f) // Inactive for 2 seconds (3+2=5 total)
                {
                    if (isWeaponActive)
                    {
                        isWeaponActive = false;
                        SetWeaponVisibility(false);
                    }
                }
                else
                {
                    cycleTimer = 0f; // Reset cycle
                }
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            Batch();
            // Reset the cycle to start fresh after upgrade
            cycleTimer = 0f;
            isWeaponActive = true;
            SetWeaponVisibility(true);
        }
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150 * CharacterTrait.WeaponSpeed;
                cycleTimer = 0f;
                isWeaponActive = true;
                Batch();
                break;
            default:
                speed = 0.3f * CharacterTrait.WeaponFireRate;
                break;
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget) return;
        Transform bullet;

        Vector3 targerPos = player.scanner.nearestTarget.position;
        Vector3 dir = (targerPos - transform.position).normalized;


        bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySFX(SFX.Range);

    }

    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                bullet.parent = transform;

            }


            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotateVec = 360 * i * Vector3.forward / count;
            bullet.Rotate(rotateVec);
            bullet.Translate(Vector3.up * 1.5f, Space.Self);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); //-100 is infinity
        }
    }

    void SetWeaponVisibility(bool isVisible)
    {
        // Toggle visibility of all child bullets for Case 0 weapon
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform bullet = transform.GetChild(i);
            bullet.gameObject.SetActive(isVisible);
        }
    }
}
