using UnityEngine;

public class Repositon : MonoBehaviour
{
    public float moveDistance = 40f;

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
            case "Enemy":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * moveDistance);
                else
                    transform.Translate(Vector3.up * dirY * moveDistance);
                break;
        }
    }
}
