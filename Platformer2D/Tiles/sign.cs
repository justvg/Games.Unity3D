using UnityEngine;
using UnityEngine.UI;

public class sign : MonoBehaviour {
    //Поля для подсказок
    [SerializeField] Image tipe;
    [SerializeField] Text tipeText;

    private void Start() {
        tipe.gameObject.SetActive(false);
        tipeText.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            tipe.gameObject.SetActive(true);
            tipeText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            tipe.gameObject.SetActive(false);
            tipeText.gameObject.SetActive(false);
        }
    }
}
