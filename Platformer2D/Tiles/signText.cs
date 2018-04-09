using UnityEngine;
using UnityEngine.UI;

public class signText : MonoBehaviour {
    //Поле для подсказки
    [SerializeField] Text tipeText;

    private void Start() {
        tipeText.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            tipeText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            tipeText.gameObject.SetActive(false);
        }
    }
}
