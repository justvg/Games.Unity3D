using System.Collections;
using UnityEngine;

public class meleeEnemy : MonoBehaviour {
    //Поля для дропа
    [SerializeField] GameObject prefabHealth;
    GameObject _health;
    [SerializeField] GameObject prefabMana;
    GameObject _mana;
    [SerializeField] GameObject prefabCoins;
    GameObject _coins;

    [SerializeField] GameObject target;

    private int health;

    void Awake() {
        health = 20;
    }

    void Update() {
        if (Vector2.Distance(target.transform.position, transform.position) < 15) {
            transform.Translate(0, 0, 0.001f * Time.deltaTime);

            //Убивает монстра, генирируя рандомный дроп. 
            if (health == 0) {
                float randomDrop = Random.Range(0.0f, 4.0f);
                if (randomDrop <= 0.4f) {
                    _health = Instantiate(prefabHealth) as GameObject;
                    _health.transform.position = transform.position;
                }
                else if (randomDrop <= 2.0f && randomDrop > 1.0f) {
                    _mana = Instantiate(prefabMana) as GameObject;
                    _mana.transform.position = transform.position;
                }
                else if (randomDrop > 2.0f && randomDrop <= 3.5f) {
                    _coins = Instantiate(prefabCoins) as GameObject;
                    _coins.transform.position = transform.position;
                }
                StartCoroutine(Die());
            }
        }
    }

    //Выключает коллайдер, чтобы перестал наносить урон персонажу
    //убивает объект, уменьшает здоровье, чтобы перестал создаваться дроп.
    IEnumerator Die() {
        GetComponent<Animator>().SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
        health--;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    public void Damage(int value) {
        health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }
}
