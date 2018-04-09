using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScr : MonoBehaviour {
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    bool pause = false;
    public Image buttonLeft;
    public Image buttonRight;
    public Color colorButton;
    public Color colorButtonActive;

    public void ChangeColorButtonLeft() {
        buttonLeft.color = colorButtonActive;
        RemoveColorButtonRight();
    }

    public void RemoveColorButtonLeft() {
        buttonLeft.color = colorButton;
    }

    public void ChangeColorButtonRight() {
        buttonRight.color = colorButtonActive;
        RemoveColorButtonLeft();
    }

    public void RemoveColorButtonRight() {
        buttonRight.color = colorButton;
    }

    public void Pause() {
        pause = !pause;

        if (pause) {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        } else {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMenu() {
        Pause();
        SceneManager.LoadScene("Startup");
    }

    public void RestartGame() {
        Pause();
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGameAfterDie() {
        SceneManager.LoadScene("GameScene");
    }
}
