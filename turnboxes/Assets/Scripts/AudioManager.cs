using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource audioSource;

    private void Start()
    {
        LevelManager.OnLevelFinish += PlaySound;
    }

    private void OnDestroy() {
        LevelManager.OnLevelFinish -= PlaySound;
    }

    public void PlaySound(bool ifwin)
    {
        audioSource.clip = ifwin ? winSound : loseSound;
        audioSource.Play();
    }
}