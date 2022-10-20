using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player player = collision.transform.GetComponent<Player>();
            OnPickup(player);
            Destroy(gameObject);
        }
    }

    public abstract void OnPickup(Player player);
}
