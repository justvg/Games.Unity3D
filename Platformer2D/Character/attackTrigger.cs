using UnityEngine;

public class attackTrigger : MonoBehaviour {
    private int dmg = 10;
    
    //Если касается врага, то наносит ему урон
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.SendMessageUpwards("Damage", dmg);
        }
    }
}
