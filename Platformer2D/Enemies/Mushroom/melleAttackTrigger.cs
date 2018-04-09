using System.Collections;
using UnityEngine;

public class melleAttackTrigger : MonoBehaviour {
    private bool canAttack = true;

    void Update() {
        if (canAttack) {
            GetComponentInParent<Animator>().SetBool("Attacking", true);
            StartCoroutine(attack());
        }
    }

    //Увеличивает атакующий коллайдер, а потом уменьшает
    IEnumerator attack() {
        canAttack = false;

        GetComponent<BoxCollider2D>().size *= 2;

        yield return new WaitForSeconds(1.5f);

        GetComponentInParent<Animator>().SetBool("Attacking", false);
        GetComponent<BoxCollider2D>().size /= 2;

        StartCoroutine(sleep());
    }

    IEnumerator sleep() {
        yield return new WaitForSeconds(2.5f);

        canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D col) {
        PlayerCharacter player = col.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.Hurt(1);
        }
    }
}
