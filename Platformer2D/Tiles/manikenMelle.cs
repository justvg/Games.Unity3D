using System.Collections;
using UnityEngine;

public class manikenMelle : MonoBehaviour {
    [SerializeField] GameObject target;
    //Поле для коллайдера с трением равным 0
    [SerializeField] Collider2D col;
    private int health;

    void Awake() {
        health = 30;
    }

    void Update() {
        if (Vector2.Distance(target.transform.position, transform.position) < 15) {
            //Нужно, чтобы коллайдер всегда двигался, то бишь можно было нанести урон.
            transform.Translate(0, 0, 0.001f * Time.deltaTime);

            //Убивает монстра.
            if (health == 0) {
                StartCoroutine(EnemyDie());
            }
        }
    }

    //Выключает коллайдер, убивает объект.
    IEnumerator EnemyDie() {
        GetComponent<Collider2D>().enabled = false;
        col.enabled = false;
        GetComponent<Animator>().SetBool("Die", true);

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    public void Damage(int value) {
        health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }
}
