using UnityEngine;

//Если сталкивается с персонажем, то вызывает метод восстановления хп
public class Mana : MonoBehaviour {
    public AudioClip bonus;

    private void OnTriggerEnter2D(Collider2D collision) {
        //Если касается земли, то выключить гравитацию
        if (collision.CompareTag("Ground")) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null && Manager.Player.mana < Manager.Player.maxMana) {
            SoundManager.instance.PlaySingle(bonus);
            player.manaHeal();
            gameObject.SetActive(false);
        }
    }
}
