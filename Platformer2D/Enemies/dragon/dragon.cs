using System.Collections;
using UnityEngine;

public class dragon: MonoBehaviour {
    // Поля для дропа
    [SerializeField] GameObject prefabHealth;
    GameObject _health;
    [SerializeField] GameObject prefabMana;
    GameObject _mana;
    [SerializeField] GameObject prefabCoins;
    GameObject _coins;

    [SerializeField] GameObject target;

    //Префаб огня
    [SerializeField] GameObject prefabFire;
    GameObject fire;

    private int health;
    private bool canAttack = true;

    void Awake() {
        health = 40;
    }

    void Update() {
        if (Vector2.Distance(target.transform.position, transform.position) < 15) {
            //Двигает коллайдер по Z, чтобы всегда можно было нанести урон
            transform.Translate(0, 0, 0.001f * Time.deltaTime);

            if (fire == null && health > 0 && canAttack) {
                StartCoroutine(fireUse());
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

    IEnumerator fireUse() {
        GetComponent<Animator>().SetBool("Attacking", true);
        fire = Instantiate(prefabFire) as GameObject;
        canAttack = false;

        yield return new WaitForSeconds(0.5f);
        
        fire.transform.position = transform.TransformPoint(new Vector2(16.0f, 0.2f));
        fire.transform.rotation = transform.rotation;

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Attacking", false);
        StartCoroutine(sleep());
    }

    IEnumerator sleep() {
        yield return new WaitForSeconds(3.0f);

        canAttack = true;
    }

    //Выключает коллайдер, чтобы перестал наносить урон персонажу
    //убивает объект, уменьшает здоровье, чтобы перестал создаваться дроп.
    IEnumerator Die() {
        GetComponent<Animator>().SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        health--;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
    }

    public void Damage(int value) {
        health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }
}

