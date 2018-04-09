using UnityEngine;

public class Thorns : MonoBehaviour {
    //Если касается персонажа, то наносит ему 1 урон.
    private void OnCollisionStay2D(Collision2D collision) {
        PlayerCharacter player = collision.collider.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
    }
}



