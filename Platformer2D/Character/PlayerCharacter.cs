using System.Collections;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    private bool canDamage = true;
    private float timeoff = 1.0f;
    public AudioClip die1Sound;
    public AudioClip die2Sound;

    private void Update() {
        if (Manager.Player.health == 0) {
            GetComponent<Animator>().SetBool("Death", true);
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
    }

    //При получении урона снимает жизни и краснеет.
    public void Hurt(int damage) {
        if (canDamage) {
            SoundManager.instance.RandomizeSfx(die1Sound, die2Sound);
            Manager.Player.ChangeHealth(-damage);
            GetComponent<Animation>().Play("RedFlash");
            canDamage = false;
            StartCoroutine(DelayOff());
        }
    }

    //Уязвимость на время timeoff
    IEnumerator DelayOff() {
        yield return new WaitForSeconds(timeoff);

        canDamage = true;
    }

    public void Heal(int healing) {
        Manager.Player.ChangeHealth(healing);
        GetComponent<Animation>().Play("PinkFlash");
    }

    public void manaHurt(int mana) {
        Manager.Player.ChangeMana(-mana);
    }

    public void manaHeal() {
        Manager.Player.ChangeMana(3);
        GetComponent<Animation>().Play("BlueFlash");
    }

    public void coinsUpdate(int coins) {
        Manager.Player.ChangeCoins(coins);
    }

    public void menUpdate() {
        Manager.Player.ChangeMen();
    }
}
