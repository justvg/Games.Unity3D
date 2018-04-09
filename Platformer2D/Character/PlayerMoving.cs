using UnityEngine;

public class PlayerMoving : MonoBehaviour {
    public float maxSpeed = 6.0f;
    private float deltaSpeed;
    public bool candoubleJump;
    private bool hitGround = false;
    private float groundRadius = 0.2f;

    //Слой пола
    public LayerMask whatIsGround;

    public AudioClip jump;

    //Поле для объекта, проверяющего соприкосновении с полом
    [SerializeField] Transform groundCheck;

    Rigidbody2D m_RigidBody2D;
    Animator anim;

    public bool IsFacingRight = true;

    private void Awake() {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deltaSpeed = 0;
    }

    private void FixedUpdate() {
        //Если здоровье меньше 1 или персонаж атакует, то не выполнять
        if (Manager.Player.health < 1 || GetComponent<Animator>().GetBool("Attacking")) {
            return;
        }

        hitGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", hitGround);

        anim.SetFloat("vSpeed", m_RigidBody2D.velocity.y);     

        // Движение персонажа и поворот спрайта.
        m_RigidBody2D.velocity = new Vector2(maxSpeed * deltaSpeed, m_RigidBody2D.velocity.y);

        if (deltaSpeed > 0 && !IsFacingRight) {
            Flip();
        }
        else if (deltaSpeed < 0 && IsFacingRight) {
            Flip();
        }
    }

    //Поворот персонажа
    private void Flip() {
        IsFacingRight = !IsFacingRight;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void Move(float delta) {
        deltaSpeed = delta;
        anim.SetFloat("Speed", Mathf.Abs(deltaSpeed));
    }

    public void Jump() {
        //Если здоровье меньше 1 или персонаж атакует, то не выполнять
        if (Manager.Player.health < 1 || GetComponent<Animator>().GetBool("Attacking")) {
            return;
        }

        if (hitGround) {
            SoundManager.instance.PlaySingleWithVolume(jump, 0.5f);
            m_RigidBody2D.AddForce(new Vector2(0, 800));
            candoubleJump = true;
        } else if (candoubleJump) {
            SoundManager.instance.PlaySingleWithVolume(jump, 0.5f);
            candoubleJump = false;
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0);
            m_RigidBody2D.AddForce(new Vector2(0, 700));
        }
    }
}

