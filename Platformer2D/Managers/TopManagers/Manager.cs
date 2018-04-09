using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(MissionManager))]
public class Manager : MonoBehaviour {
    public static PlayerManager Player { get; private set; }
    public static MissionManager Mission { get; private set; }
    public static Manager instance = null;

    private List<IGameManager> _startSequence;

    void Awake() {
      /*  PlayerPrefs.SetInt("openedLevels", 0);
        PlayerPrefs.SetInt("level1spheres", 0);
        PlayerPrefs.SetInt("level2spheres", 0);
        PlayerPrefs.SetInt("level3spheres", 0);
        PlayerPrefs.SetInt("sphere1level1", 0);
        PlayerPrefs.SetInt("sphere2level1", 0);  */
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Player = GetComponent<PlayerManager>();
        Mission = GetComponent<MissionManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Mission);

        StartCoroutine(StartupManagers()); 
	}


    //Запускает все менеджеры из коллекции _startSequence
	IEnumerator StartupManagers() {
        foreach (IGameManager manager in _startSequence) {
            manager.StartUp();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence) {
                if (manager.status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);

            yield return null;
        }

        Debug.Log("All managers started up");
    }
}
