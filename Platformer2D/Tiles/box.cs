using System.Collections;
using UnityEngine;

public class box : MonoBehaviour {
    //Поля для дропа
    [SerializeField] GameObject prefabMana;
    GameObject _mana;
    [SerializeField] GameObject prefabCoins;
    GameObject _coins;
    [SerializeField] GameObject target;

    private int health;

    void Awake() {
        health = 10;
    }

    void Update() {
        if (Vector2.Distance(target.transform.position, transform.position) < 15) {
            //Нужно, чтобы коллайдер всегда двигался, то бишь можно было нанести урон.
            transform.Translate(0, 0, 0.001f * Time.deltaTime);

            if (health == 0) {
                float randomDrop = Random.Range(0.0f, 4.0f);
                if (randomDrop <= 3.0f) {
                    _coins = Instantiate(prefabCoins) as GameObject;
                    _coins.transform.position = transform.position;
                }
                else {
                    _mana = Instantiate(prefabMana) as GameObject;
                    _mana.transform.position = transform.position;
                }
                StartCoroutine(EnemyDie());
            }
        }
    }

    //Выключает коллайдер, убивает объект, уменьшает здоровье, чтобы перестал создаваться дроп.
    IEnumerator EnemyDie() {
        GetComponent<Animator>().SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        health--;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    public void Damage(int value) {
        health -= value;
    }
}
