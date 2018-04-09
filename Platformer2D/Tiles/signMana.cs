using UnityEngine;
using UnityEngine.UI;

public class signMana : MonoBehaviour {
    //Поля для подсказок и маны.
    [SerializeField] Image tipe;
    [SerializeField] Text tipeText;
    [SerializeField] GameObject prefabMana;
    GameObject mana;

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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (Manager.Player.mana == 0) {
                mana = Instantiate(prefabMana) as GameObject;
                mana.transform.position = transform.position;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            tipe.gameObject.SetActive(false);
            tipeText.gameObject.SetActive(false);
        }
    }
}
