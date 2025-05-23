using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    
    void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(speed * Time.deltaTime * Vector3.forward);
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
            bullet.localPosition = new Vector3(bullet.localPosition.x, bullet.localPosition.y, 0f);
            bullet.parent = transform;
            Debug.Log(bullet.name);
            Debug.Log(bullet.parent.name);

            Vector3 rotateVec = 360 * i * Vector3.forward / count;
            bullet.Rotate(rotateVec);
            bullet.Translate(Vector3.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); //-1 is infinity


        }
    }
}
