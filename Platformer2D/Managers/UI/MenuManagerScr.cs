using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerScr : MonoBehaviour {
    [SerializeField] Image startButton;
    [SerializeField] Button settingButton;
    [SerializeField] GameObject chooseLevelMenu;

    [SerializeField] GameObject level1;
    [SerializeField] GameObject level1sphere1;
    [SerializeField] GameObject level1sphere2;
    [SerializeField] GameObject level1sphere3;

    [SerializeField] GameObject level2;
    [SerializeField] GameObject level2sphere1;
    [SerializeField] GameObject level2sphere2;
    [SerializeField] GameObject level2sphere3;

    [SerializeField] GameObject level3;
    [SerializeField] GameObject level3sphere1;
    [SerializeField] GameObject level3sphere2;
    [SerializeField] GameObject level3sphere3;

    [SerializeField] GameObject level2locked;
    [SerializeField] GameObject level3locked;

    void Start() {
        Time.timeScale = 1;
    }

    public void ClickMusic(AudioClip clip) {
        SoundManager.instance.PlaySingle(clip);
    }

    public void ChooseLvlMenu() {
        startButton.enabled = false;
        settingButton.gameObject.SetActive(false);

        chooseLevelMenu.gameObject.SetActive(true);
        level1.gameObject.SetActive(true);
        level2locked.gameObject.SetActive(true);
        level3locked.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("openedLevels") >= 1) {
            level2.gameObject.SetActive(true);
            level2locked.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("openedLevels") >= 2) {
            level3.gameObject.SetActive(true);
            level3locked.gameObject.SetActive(false);
        }

        switch(PlayerPrefs.GetInt("level1spheres")) {
            case 1:
                level1sphere1.gameObject.SetActive(true);
                break;
            case 2:
                level1sphere1.gameObject.SetActive(true);
                level1sphere2.gameObject.SetActive(true);
                break;
            case 3:
                level1sphere1.gameObject.SetActive(true);
                level1sphere2.gameObject.SetActive(true);
                level1sphere3.gameObject.SetActive(true);
                break;
        }

        switch (PlayerPrefs.GetInt("level2spheres")) {
            case 1:
                level2sphere1.gameObject.SetActive(true);
                break;
            case 2:
                level2sphere1.gameObject.SetActive(true);
                level2sphere2.gameObject.SetActive(true);
                break;
            case 3:
                level2sphere1.gameObject.SetActive(true);
                level2sphere2.gameObject.SetActive(true);
                level2sphere3.gameObject.SetActive(true);
                break;
        }

        switch (PlayerPrefs.GetInt("level3spheres")) {
            case 1:
                level3sphere1.gameObject.SetActive(true);
                break;
            case 2:
                level3sphere1.gameObject.SetActive(true);
                level3sphere2.gameObject.SetActive(true);
                break;
            case 3:
                level3sphere1.gameObject.SetActive(true);
                level3sphere2.gameObject.SetActive(true);
                level3sphere3.gameObject.SetActive(true);
                break;
        }
    }

    public void SettingsMenu() {

    }

    public void LoadLevel(int number) {
        if (number > 0 && number <= Manager.Mission.maxLevel && number <= PlayerPrefs.GetInt("openedLevels") + 1) {
            Manager.Mission.curLevel = number;
            SceneManager.LoadScene("Level" + number);
        }
    }
}
