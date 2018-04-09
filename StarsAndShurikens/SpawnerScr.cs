using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScr : MonoBehaviour {
    [SerializeField] GameObject starPrefab;
    GameObject star;

    [SerializeField] GameObject shurikenPrefab;
    GameObject shuriken;

    float starTimer;

    float shurikenTimer;
    float shurikenRandomRange = 60.0f;
    float randomPlus;

	void Start () {
        StartCoroutine("StarSpawner");
        StartCoroutine("ShurikenSpawner");
	}
	
	void Update () {
        if (shurikenRandomRange > 5.1f) {
            shurikenRandomRange -= Time.deltaTime;
        }
	}

    IEnumerator StarSpawner() {
        while (true) {
            starTimer = Random.Range(1.0f, 5.0f);

            yield return new WaitForSeconds(starTimer);

            star = Instantiate(starPrefab) as GameObject;
            star.transform.position = transform.position;

            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator ShurikenSpawner() {
        while(true) {

            randomPlus = Random.Range(0.5f, 4.0f);
            shurikenTimer = Random.Range(5.0f, shurikenRandomRange + randomPlus);

            yield return new WaitForSeconds(shurikenTimer);

            shuriken = Instantiate(shurikenPrefab) as GameObject;
            shuriken.transform.position = transform.position;

            yield return new WaitForSeconds(0.5f);
        }
    }
}