using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    //Поля для хп, маны и "спасенных людей".
    [SerializeField] Image[] healthmanaImages;
    [SerializeField] Image[] menImages;

    //Количество золота, лого смерти.
    [SerializeField] Text coinsLabel;
    [SerializeField] Image deathLogo;

    //Панель паузы.
    [SerializeField] GameObject pausePanel;
    private bool paused = false;

    //Меню после окончания уровня.
    [SerializeField] GameObject chooseNextMenu;
    bool levelCompleted = false;

    //Сферы в игре
    [SerializeField] GameObject sphere1;
    [SerializeField] GameObject sphere2;
    [SerializeField] GameObject sphere3;

    //Присоединяет всех подписчиков.
    void Awake() {
        pausePanel.SetActive(false);
        chooseNextMenu.SetActive(false);
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.MANA_UPDATED, OnManaUpdated);
        Messenger.AddListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.AddListener(GameEvent.MEN_UPDATED, OnMenUpdated);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETED, OnLevelComplete);
    }

    void OnDestroy() {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.MANA_UPDATED, OnManaUpdated);
        Messenger.RemoveListener(GameEvent.COINS_UPDATED, OnCoinsUpdated);
        Messenger.RemoveListener(GameEvent.MEN_UPDATED, OnMenUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETED, OnLevelComplete);
    }

    void Start() {
        OnHealthUpdated();
        OnManaUpdated();
        OnMenUpdated();

        switch (Manager.Mission.curLevel) {
            case 1:
                if (PlayerPrefs.GetInt("sphere1level1") == 1) {
                    sphere1.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere2level1") == 1) {
                    sphere2.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere3level1") == 1) {
                    sphere3.gameObject.SetActive(false);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("sphere1level2") == 1) {
                    sphere1.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere2level2") == 1) {
                    sphere2.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere3level2") == 1) {
                    sphere3.gameObject.SetActive(false);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("sphere1level3") == 1) {
                    sphere1.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere2level3") == 1) {
                    sphere2.gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("sphere3level3") == 1) {
                    sphere3.gameObject.SetActive(false);
                }
                break;
        }
    }

    // Вызывает корутину, чтобы оповестить игрока
    // о поражении и перезагрузить уровень.
    private void OnLevelFailed() {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel() {
        deathLogo.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        Manager.Player.Respawn();
        Manager.Mission.RestartCurrent();
    }

    // Вызывает корутину, чтобы оповестить игрока
    // о прохождении уровня и перейти на новый уровень.
    private void OnLevelComplete() {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel() {
        yield return new WaitForSeconds(1);

        levelCompleted = true;
        Time.timeScale = 0;

        chooseNextMenu.SetActive(true);
    }

    //Метод для кнопки
    public void NextLevelGo() {
        Manager.Mission.GoToNext();
    }

    private void OnCoinsUpdated() {
        coinsLabel.text = Manager.Player.coins.ToString();
    }

    public void Pause() {
        paused = !paused;
        
        if (levelCompleted) { return; }

        if (paused) {
            pausePanel.SetActive(true);

            Time.timeScale = 0;
        } else {
            pausePanel.SetActive(false);

            Time.timeScale = 1;
        }
    }

    public void Restart() {
        Pause();
        Manager.Player.Respawn();
        Manager.Mission.RestartCurrent();
    }

    public void MainMenu() {
        Pause();
        SceneManager.LoadScene("Level0");
    }

    public void ChangeColor(Image imageButton) {
        imageButton.color = Color.grey;
        imageButton.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void RemoveColor(Image imageButton) {
        imageButton.color = Color.white;
        imageButton.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void ClickMusic(AudioClip clip) {
        SoundManager.instance.PlaySingle(clip);
    }

    private void OnHealthUpdated() {
        switch (Manager.Player.health) {
            case 3:
                healthmanaImages[0].gameObject.SetActive(true);
                healthmanaImages[1].gameObject.SetActive(true);
                healthmanaImages[2].gameObject.SetActive(true);
                break;
            case 2:
                healthmanaImages[0].gameObject.SetActive(true);
                healthmanaImages[1].gameObject.SetActive(true);
                healthmanaImages[2].gameObject.SetActive(false);
                break;
            case 1:
                healthmanaImages[0].gameObject.SetActive(true);
                healthmanaImages[1].gameObject.SetActive(false);
                healthmanaImages[2].gameObject.SetActive(false);
                break;
            default:
                healthmanaImages[0].gameObject.SetActive(false);
                healthmanaImages[1].gameObject.SetActive(false);
                healthmanaImages[2].gameObject.SetActive(false);
                break;
        }
    }

    private void OnManaUpdated() {
        switch (Manager.Player.mana) {
            case 3:
                healthmanaImages[3].gameObject.SetActive(true);
                healthmanaImages[4].gameObject.SetActive(true);
                healthmanaImages[5].gameObject.SetActive(true);
                break;
            case 2:
                healthmanaImages[3].gameObject.SetActive(true);
                healthmanaImages[4].gameObject.SetActive(true);
                healthmanaImages[5].gameObject.SetActive(false);
                break;
            case 1:
                healthmanaImages[3].gameObject.SetActive(true);
                healthmanaImages[4].gameObject.SetActive(false);
                healthmanaImages[5].gameObject.SetActive(false);
                break;
            default:
                healthmanaImages[3].gameObject.SetActive(false);
                healthmanaImages[4].gameObject.SetActive(false);
                healthmanaImages[5].gameObject.SetActive(false);
                break;
        }
    }

    private void OnMenUpdated() {
        switch(Manager.Mission.curLevel) {
            case 1:
                switch (PlayerPrefs.GetInt("level1spheres")) {
                    case 3:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(true);
                        break;
                    case 2:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    case 1:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    default:
                        menImages[0].gameObject.SetActive(false);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                }
                break;
            case 2:
                switch (PlayerPrefs.GetInt("level2spheres")) {
                    case 3:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(true);
                        break;
                    case 2:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    case 1:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    default:
                        menImages[0].gameObject.SetActive(false);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                }
                break;
            case 3:
                switch (PlayerPrefs.GetInt("level3spheres")) {
                    case 3:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(true);
                        break;
                    case 2:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(true);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    case 1:
                        menImages[0].gameObject.SetActive(true);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                    default:
                        menImages[0].gameObject.SetActive(false);
                        menImages[1].gameObject.SetActive(false);
                        menImages[2].gameObject.SetActive(false);
                        break;
                }
                break;
        }
    }
}
