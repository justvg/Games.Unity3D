using UnityEngine;

//Если сталкивается с персонажем, добавляет звездочку и золото
public class men : MonoBehaviour {
    public AudioClip bonus;
    public int id;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
        if (player != null) {
            SoundManager.instance.PlaySingle(bonus);

            switch (id) {
                case 1:
                    switch (Manager.Mission.curLevel) {
                        case 1:
                            PlayerPrefs.SetInt("sphere1level1", 1);                   
                            break;
                        case 2:
                            PlayerPrefs.SetInt("sphere1level2", 1);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("sphere1level3", 1);
                            break;
                    }
                    break;
                case 2:
                    switch (Manager.Mission.curLevel) {
                        case 1:
                            PlayerPrefs.SetInt("sphere2level1", 1);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("sphere2level2", 1);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("sphere2level3", 1);
                            break;
                    }
                    break;
                case 3:
                    switch (Manager.Mission.curLevel) {
                        case 1:
                            PlayerPrefs.SetInt("sphere3level1", 1);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("sphere3level2", 1);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("sphere3level3", 1);
                            break;
                    }
                    break;
            }

            switch(Manager.Mission.curLevel) {
                case 1:
                    int n = PlayerPrefs.GetInt("level1spheres") + 1;
                    PlayerPrefs.SetInt("level1spheres", n);
                    if (PlayerPrefs.GetInt("level1spheres") > 3) {
                        PlayerPrefs.SetInt("level1spheres", 3);
                    }
                    break;
                case 2:
                    int q = PlayerPrefs.GetInt("leve21spheres") + 1;
                    PlayerPrefs.SetInt("level2spheres", q);
                    if (PlayerPrefs.GetInt("level2spheres") > 3) {
                        PlayerPrefs.SetInt("level2spheres", 3);
                    }
                    break;
                case 3:
                    int r = PlayerPrefs.GetInt("level3spheres") + 1;
                    PlayerPrefs.SetInt("level3spheres", r);
                    if (PlayerPrefs.GetInt("level3spheres") > 3) {
                        PlayerPrefs.SetInt("level3spheres", 3);
                    }
                    break;
            }

            player.menUpdate();
            player.coinsUpdate(100);
            gameObject.SetActive(false);
        }
    }
}
