using UnityEngine;

//Если сталкивается с персонажем, то вызывает метод лечения.
public class Health : MonoBehaviour {
    public AudioClip bonus;

    private void OnTriggerEnter2D(Collider2D collision) {
        //Если касается земли, то выключить гравитацию
        if (collision.CompareTag("Ground")) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            SoundManager.instance.PlaySingle(bonus);
            player.Heal(1);
            gameObject.SetActive(false);
        }
    }
}
