using System.Collections;
using UnityEngine;

public class mushroomJump : MonoBehaviour {
    public float jumpForce;
    //Отталкивает персонажа вверх.
    void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D player = collision.collider.GetComponent<Rigidbody2D>();
        if (player != null) {
            GetComponent<Animator>().SetBool("Jump", true);
            player.velocity = new Vector2(0, jumpForce);
            StartCoroutine(StopAnim());
        }
    }

    IEnumerator StopAnim() {
        yield return new WaitForSeconds(0.3f);

        GetComponent<Animator>().SetBool("Jump", false);
    }
}
