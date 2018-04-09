using UnityEngine;

public class axeTrigger : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collision) {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
    }
}
