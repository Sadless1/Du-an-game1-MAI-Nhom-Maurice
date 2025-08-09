using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;   // Nguồn phát nhạc nền
    public Slider musicSlider;        // Thanh chỉnh âm lượng (nếu có)
    public AudioClip sceneMusic;      // Nhạc nền cho scene này

    void Start()
    {
        // Gán nhạc cho scene
        if (sceneMusic != null)
        {
            musicSource.clip = sceneMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Nếu có Slider âm lượng thì gán sự kiện
        if (musicSlider != null)
        {
            float savedVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSource.volume = savedVol;
            musicSlider.value = savedVol;

            musicSlider.onValueChanged.AddListener(v =>
            {
                musicSource.volume = v;
                PlayerPrefs.SetFloat("MusicVolume", v);
            });
        }
    }
}