using UnityEngine;

public class fire : MonoBehaviour {
    //Поле определяет сторону, в которую летит огонь
    public int direction;

    void Start() {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(500.0f * direction, 0));
    }

    void OnTriggerEnter2D(Collider2D collision) {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
        if ((!collision.CompareTag("WallForEnemy")) && (!collision.CompareTag("Enemy") && (!collision.CompareTag("AttackTrigger")))) {
            Destroy(gameObject);
        }
    }
}
