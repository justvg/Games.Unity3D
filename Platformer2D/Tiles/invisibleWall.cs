using UnityEngine;

//Добавляется к прозрачной стене
public class invisibleWall : MonoBehaviour {
    //Поле для непрозрачной стены
    [SerializeField] GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            wall.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            wall.gameObject.SetActive(true);
        }
    }
}
