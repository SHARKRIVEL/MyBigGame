using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{   
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_Text musicText;
    [SerializeField] Slider otherSoundsSlider;
    [SerializeField] TMP_Text otherSoundsText;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioListener audioListener;
    [SerializeField] GameObject audioMB;

    [SerializeField] MusicPlayer musicPlayer;
    public MusicPlayer musicPlayerRef { get { return musicPlayer; }}

    public static AudioManager audioListenerScript;

    void Awake()
    {
        if(audioListenerScript != null && audioListenerScript != this)
        {
            Destroy(gameObject);
            return;
        }

        audioListenerScript = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        otherSoundsSlider.value = PlayerPrefs.GetFloat("GameVolume",1f);
        AudioListener.volume = otherSoundsSlider.value;
        otherSoundsText.text = Mathf.RoundToInt(otherSoundsSlider.value*100).ToString();

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume",1f);
        musicAudioSource.volume = musicSlider.value;
        musicText.text = Mathf.RoundToInt(musicSlider.value*100).ToString();
    }

    void Update()
    {
        otherSoundsSlider.onValueChanged.AddListener(SetotherSoundsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicAudioVolume);
    }

    void SetotherSoundsVolume(float volume)
    {
        PlayerPrefs.SetFloat("GameVolume",volume);
        AudioListener.volume = volume;
        otherSoundsText.text = Mathf.RoundToInt(volume*100).ToString();
    }

    void SetMusicAudioVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume",volume);
        musicAudioSource.volume = volume;
        musicText.text = Mathf.RoundToInt(volume*100).ToString();
    }

    public void AudioManagerButtons(bool activate)
    {
        audioMB.SetActive(activate);
    }
}
