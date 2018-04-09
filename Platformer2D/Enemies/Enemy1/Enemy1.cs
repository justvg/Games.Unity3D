using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
    //Поля для дропа.
    [SerializeField] GameObject prefabHealth;
    GameObject _health;
    [SerializeField] GameObject prefabMana;
    GameObject _mana;
    [SerializeField] GameObject prefabCoins;
    GameObject _coins;

    [SerializeField] GameObject target;

    private int health;
    private float speed = 1.8f;
    private float direction = -1;

    private bool IsFacingRight = true;

    void Awake() {
        health = 30;
    }

    void Update () {
        if (Vector2.Distance(target.transform.position,transform.position) < 15) { 
        //Ходит в разные стороны
        transform.Translate(speed * Time.deltaTime * direction, 0, 0);     

        if (direction > 0 && !IsFacingRight) {
            Flip();
        } else if (direction < 0 && IsFacingRight) {
            Flip();
        }

            //Убивает монстра, генирируя рандомный дроп. 
            if (health == 0) {
                direction = 0;
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
                StartCoroutine(EnemyDie());
            }
        }
	}

    //Выключает коллайдер, чтобы перестал наносить урон персонажу
    //убивает объект, уменьшает здоровье, чтобы перестал создаваться дроп.
    IEnumerator EnemyDie() {
        GetComponent<Animator>().SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        health--;

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    //Поворачивает спрайт.
    private void Flip() {
        IsFacingRight = !IsFacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    public void Damage(int value) {
        health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }

    //При столкновение с персонажем наносит ему урон
    //При столкновении с WallForEnemy меняет сторону движения
    void OnTriggerStay2D(Collider2D collision) {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("WallForEnemy")) {
            direction *= -1f;
        }
    }
}
