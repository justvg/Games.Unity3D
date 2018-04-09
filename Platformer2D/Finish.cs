using UnityEngine;

//При столкновении с игроком запускает новый уровень
public class Finish : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Manager.Mission.ReachObjective();
        }
    }
}
