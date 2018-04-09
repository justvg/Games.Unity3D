using UnityEngine;

public class Poison : MonoBehaviour {
    public float speed = 3.5f;
    //Определяет, в какую сторону лететь яду
    public int direction;

	void Update () {
        transform.Translate(Time.deltaTime * direction * speed, 0, 0);
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
