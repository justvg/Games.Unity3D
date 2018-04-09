using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }

    public int curLevel { get; set; }
    public int maxLevel { get; private set; }

    public void StartUp() {
        Debug.Log("Mission manager starting...");

        curLevel = 0;
        maxLevel = 3;

        status = ManagerStatus.Started;
    }

    // Переход на следующий уровень.
    public void GoToNext() {
        Time.timeScale = 1;

        Manager.Player.UpdateData(3, 0, Manager.Player.coins);
        if (curLevel < maxLevel) {           
            curLevel++;
            SceneManager.LoadScene("Level" + curLevel);
        }
    }

    // Перезагрузка уровня.
    public void RestartCurrent() {
        SceneManager.LoadScene("Level" + curLevel);
    }

    public void ReachObjective() {
        if (PlayerPrefs.GetInt("openedLevels") < curLevel) {
            PlayerPrefs.SetInt("openedLevels", curLevel);
        }
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETED);
    }
}
