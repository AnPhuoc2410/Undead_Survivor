using UnityEngine;

public class Repositon : MonoBehaviour
{
    public float moveDistance = 40f;
    private Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;


        switch (transform.tag)
        {
            case "Ground":

                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                    transform.Translate(dirX * moveDistance * Vector3.right);
                else if (diffX < diffY)
                    transform.Translate(dirY * moveDistance * Vector3.up);
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 playerDir = (playerPos - myPos).normalized;
                    Vector3 ran = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);

                    transform.Translate(ran + playerDir * 2);
                }
                break;
            case "ExpOrb":
                if (coll.enabled)
                {
                    Vector3 playerDir = (playerPos - myPos).normalized;
                    Vector3 ran = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
                    transform.Translate(ran + playerDir * 5);
                }
                break;
        }
    }
}
