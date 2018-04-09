using System.Collections;
using UnityEngine;

public class reptile : MonoBehaviour {
    //Поля для дропа
    [SerializeField] GameObject prefabHealth;
    GameObject _health;
    [SerializeField] GameObject prefabMana;
    GameObject _mana;
    [SerializeField] GameObject prefabCoins;
    GameObject _coins;

    [SerializeField] GameObject target;

    //Поля для коллайдера оружия и обычного, наносящего урон
    [SerializeField] Collider2D axeTrigger;
    [SerializeField] Collider2D attackingTrigger;

    //Поле для рептилии, которая повернута в другую сторону
    [SerializeField] GameObject reptileNext;

    private bool canAttack = true;

    public bool isFacingRight;

    private int health;

    private void OnEnable() {
        canAttack = true;
    }

    void Start() {
        health = 40;
        axeTrigger.enabled = false;
    }

    void Update() {
        if (Vector2.Distance(target.transform.position, transform.position) < 15) {

            transform.Translate(0, 0, 0.001f * Time.deltaTime);

            if (canAttack) {
                StartCoroutine(Attack());
            }

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

    //В зависимости от положения персонажа относительно рептилии 
    //включает/выключает нужную рептилию
    void LateUpdate() {
        Vector2 right = transform.TransformDirection(Vector2.right);
        Vector2 left = transform.TransformDirection(Vector2.left);

        Vector2 vTarget = target.transform.position - transform.position;

        if (isFacingRight && (Vector2.Dot(vTarget, right) < 0)) {
            gameObject.SetActive(false);
            reptileNext.gameObject.SetActive(true);
        } else if (!isFacingRight && (Vector2.Dot(vTarget, left) < 0)) {
            gameObject.SetActive(false);
            reptileNext.gameObject.SetActive(true);
        }
    }

    IEnumerator Attack() {
        GetComponent<Animator>().SetBool("Attack", true);
        axeTrigger.enabled = true;
        canAttack = false;

        yield return new WaitForSeconds(0.45f);

        GetComponent<Animator>().SetBool("Attack", false);
        axeTrigger.enabled = false;
        StartCoroutine(Guard());
    }

    IEnumerator Guard() {
        GetComponent<Animator>().SetBool("Guard", true);
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1.0f);

        GetComponent<Animator>().SetBool("Guard", false);
        GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(1.5f);

        canAttack = true;
    }

    //Выключает коллайдер, чтобы перестал наносить урон персонажу
    //убивает объект, уменьшает здоровье, чтобы перестал создаваться дроп.
    IEnumerator Die() {
        GetComponent<Animator>().SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        attackingTrigger.enabled = false;
        axeTrigger.enabled = false;
        health--;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    public void Damage(int value) {
        health -= value;
        reptileNext.GetComponent<reptile>().health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }
}
