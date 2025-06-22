using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        //rb.AddForce(dir * 10f, ForceMode2D.Impulse); // TEST

        if(per >= 0)
        {
            rb.linearVelocity = dir * 15f; // * 15f
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) return;

        per--;

        if(per < 0)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100) return;

        gameObject.SetActive(false);
    }


}
