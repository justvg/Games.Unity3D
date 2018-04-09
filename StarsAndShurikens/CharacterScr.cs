using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScr : MonoBehaviour {
    [SerializeField] Text scoreLabel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject diePanel;

    [SerializeField] UIScr uiController;

    [SerializeField] Text dieScoreLabel;
    [SerializeField] Text dieBestScoreLabel;

    AudioSource audioSource;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip pointSound;

    public float speed;
    int direction;
    bool alive;
    bool isFacingRight = true;
    int score;
    int dieScore;

    Rigidbody2D m_rigidbody2D;
    Animator anim;

    void Start () {
        audioSource = GetComponent<AudioSource>();

        dieBestScoreLabel.text = PlayerPrefs.GetInt("Record").ToString();
        Time.timeScale = 1;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        direction = 0;
        alive = true;
	}
	
	void Update () {
		if (alive) {
            if (isFacingRight && direction < 0) {
                Flip();
            } else if (!isFacingRight && direction > 0) {
                Flip();
            }

            m_rigidbody2D.velocity = new Vector2(speed * direction, 0);
        }
	}

    void Flip() {
        isFacingRight = !isFacingRight;

        transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
    }

    public void Move(int i) {     
        direction = i;

        anim.SetFloat("Speed", Mathf.Abs(direction));
    }

    void OnTriggerEnter2D(Collider2D col) {      
        if (col.CompareTag("Star")) {
            audioSource.PlayOneShot(pointSound);
            score = int.Parse(scoreLabel.text) + 1;
            scoreLabel.text = score.ToString();
            col.gameObject.SetActive(false);
        }

        if (col.CompareTag("Shuriken")) {
            audioSource.PlayOneShot(dieSound);
            alive = false;
            Time.timeScale = 0;
            gamePanel.SetActive(false);
            diePanel.SetActive(true);
            dieScoreLabel.text = scoreLabel.text;

            dieScore = int.Parse(scoreLabel.text);

            if (dieScore > int.Parse(dieBestScoreLabel.text)) {
                PlayerPrefs.SetInt("Record", dieScore);
                PlayerPrefs.Save();
                dieBestScoreLabel.text = PlayerPrefs.GetInt("Record").ToString();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Wall")) {
            direction = 0;
            anim.SetFloat("Speed", Mathf.Abs(direction));

            uiController.RemoveColorButtonLeft();
            uiController.RemoveColorButtonRight();
        }
    }
}