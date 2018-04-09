using UnityEngine;

public class goldCoins : MonoBehaviour {
    public AudioClip gold1Sound;
    public AudioClip gold2Sound;
    public AudioClip gold3Sound;
    public AudioClip gold4Sound;
    public AudioClip gold5Sound;

    private void OnTriggerEnter2D(Collider2D collision) {
        //Если касается земли, то выключить гравитацию
        if (collision.CompareTag("Ground")) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;       
        }

        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            player.coinsUpdate(50);
            SoundManager.instance.RandomizeSfx(gold1Sound, gold2Sound, gold3Sound, gold4Sound, gold5Sound);
            gameObject.SetActive(false);
        }
    }
}
