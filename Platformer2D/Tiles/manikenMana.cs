using System.Collections;
using UnityEngine;

public class manikenMana : MonoBehaviour {
    private int health;

    void Awake() {
        health = 30;
    }

    void Update() {
        //Убивает монстра
        if (health == 0) {
            StartCoroutine(EnemyDie());
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

    public void Damage(int value) {
        health -= value;
        GetComponent<Animation>().Play("RedFlash");
    }
}
