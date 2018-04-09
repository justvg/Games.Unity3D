using UnityEngine;

public class playerAttack : MonoBehaviour {
    //Поля для атакующего тригера и файрбола
    [SerializeField] Collider2D attackTriger;
    [SerializeField] GameObject fireballPrefab;
    private GameObject _fireball;

    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioClip fireball1Sound;
    public AudioClip fireball2Sound;

    private bool attacking = false;
    private bool attackbutton = false;

    private float attackTimer = 0;
    private float attackCd = 0.25f;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        attackTriger.enabled = false;
    }

    //Если нажата кнопка, то персонаж бьёт с кулдауном attackCd;
    public void Update() {
        if (attackbutton) {
            if (!attacking) {
                attacking = true;
                attackTimer = attackCd;

                SoundManager.instance.RandomizeSfx(attack1Sound, attack2Sound);

                attackTriger.enabled = true;
            }

            if (attackTimer > 0) {
                attackTimer -= Time.deltaTime;
            } else {
                attackbutton = false;
                attacking = false;
                attackTriger.enabled = false;
            }

            anim.SetBool("Attacking", attacking);
        }
    }

    public void Attack() {
        attackbutton = true;
    }

    public void UseFireball() {
        if (_fireball == null && Manager.Player.mana > 0) {
            SoundManager.instance.RandomizeSfx(fireball1Sound, fireball2Sound);
            GetComponent<PlayerCharacter>().manaHurt(1);
            _fireball = Instantiate(fireballPrefab) as GameObject;
            _fireball.transform.position = transform.TransformPoint(new Vector2(0.1f, 0));
            _fireball.transform.rotation = transform.rotation;

            if (!GetComponent<PlayerMoving>().IsFacingRight) {
                _fireball.transform.localScale *= -1;
            }
        }
    }
}
