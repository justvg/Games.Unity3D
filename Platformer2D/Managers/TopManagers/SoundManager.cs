using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;
    public AudioSource efxSource;
    public AudioSource musicSource;

    float lowPitch = 0.95f;
    float highPitch = 1.05f;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip) {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);

        float randomPitch = Random.Range(lowPitch, highPitch);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void PlaySingleWithVolume(AudioClip clip, float volume) {
        efxSource.clip = clip;
        efxSource.volume = volume;
        efxSource.Play();

        StartCoroutine(ReturnPitch());
    }

    IEnumerator ReturnPitch() {
        yield return new WaitForSeconds(0.4f);

        efxSource.volume = 1;
    }
}

