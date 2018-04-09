using UnityEngine;

public class Fireball : MonoBehaviour {
    private float speed = 7.5f;
    public int damage = 10;
    private int direction;

    void Start() {
        if (fireballHelper.IsFacingRight) {
            direction = 1;
        } else {
            direction = -1;
        }
    }

    void Update () {
        transform.Translate(Time.deltaTime * speed * direction, 0, 0);
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") || collision.CompareTag("manaManiken")) {
            collision.SendMessageUpwards("Damage", damage);
        }
        if (!collision.CompareTag("WallForEnemy") && !collision.CompareTag("Player") && !collision.CompareTag("AttackTrigger") && !collision.CompareTag("Drop")) {
            Destroy(gameObject);
        }
    }
}
