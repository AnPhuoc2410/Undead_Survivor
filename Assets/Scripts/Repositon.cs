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

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.input;
        float dirX = playerDir.x == 0 ? 0 : (playerDir.x < 0 ? -1 : 1);
        float dirY = playerDir.y == 0 ? 0 : (playerDir.y < 0 ? -1 : 1);

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * moveDistance);
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * dirY * moveDistance);
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f), 0f));
                }
                break;
            case "ExpOrb":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f), Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}
