using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Options : MonoBehaviour
{
    public static Options instance;

    [Header("Pause/Resume")]
    [SerializeField] private DOTweenAnimation pauseHolderDOT;
    [SerializeField] private Button resumeBTN;
    private bool resumePlay = false;

    [Header("Restart")]
    [SerializeField] private Toggle autoRestartToggle;

    [Header("Quality")]
    [SerializeField] private Sprite[] qualitySprites;
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private Image qualityImage;
    [SerializeField] private AssetsGraphicState assetsGraphicsState;
    private int qualityValue;

    [Header("Audio")]
    [SerializeField] private Sprite[] audioSprites;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Image audioImage;
    private int audioValue;

    [Header("Continue AD")]
    [SerializeField] private GameObject continueButton;
    private int _continuesCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        LoadAutoRestart_Toggle();
    }

    private void Start()
    {
        LoadAudio();
        LoadQuality();
    }

    #region Start/Pause/Resume
    public void StartPause()
    {
        if (!pauseHolderDOT.gameObject.activeInHierarchy)
        {
            AudioManager.instance.Play("Select");
            pauseHolderDOT.gameObject.SetActive(true);
            if (Game_Manager.isPlaying)
                resumePlay = true;

            Game_Manager.instance.PauseGame();
            pauseHolderDOT.DORestart();
        }
    }

    public void PauseDead()
    {
        resumeBTN.interactable = false;
        StartPause();
        CheckAutoRestart();

        if(_continuesCount < 1)
        {
            _continuesCount++;
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        AudioManager.instance.Play("Select");
        pauseHolderDOT.DOPlayBackwards();
        if (resumePlay)
        {
            Game_Manager.isPlaying = true;
            Game_Manager.instance.StartGame();
            resumePlay = false;
        }
    }

    public void ContinueReward()
    {
        continueButton.SetActive(false);
        resumeBTN.interactable = true;
    }

    #endregion

    #region AutoRestart
    public void RestartScene_Button(bool isPlayerState)
    {
        FindObjectOfType<AudioManager>().Play("Select");
        pauseHolderDOT.DOPlayBackwards();
        Game_Manager.isPlaying = isPlayerState;
        if (isPlayerState)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            FindObjectOfType<Transition>().PlayOutTransition(SceneManager.GetActiveScene().name);
    }

    public void AutoRestart_Toggle()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        int currentToggleValue = 0;
        if (autoRestartToggle.isOn) currentToggleValue = 1;
        else currentToggleValue = 0;
        PlayerPrefs.SetInt("autoRestart", currentToggleValue);
    }

    private void LoadAutoRestart_Toggle()
    {
        if (PlayerPrefs.HasKey("autoRestart"))
        {
            if (PlayerPrefs.GetInt("autoRestart") == 1) autoRestartToggle.SetIsOnWithoutNotify(true);
            else autoRestartToggle.SetIsOnWithoutNotify(false);
        }
    }

    private void CheckAutoRestart()
    {
        if(PlayerPrefs.HasKey("autoRestart"))
        {
            if (PlayerPrefs.GetInt("autoRestart") == 1)
                RestartScene_Button(true);
        }
    }
    #endregion

    #region Quality
    public void ChangeQualityValue()
    {
        AudioManager.instance.Play("Select");
        if (qualityValue < 2)
            qualityValue++;
        else
            qualityValue = 0;


        qualityImage.sprite = qualitySprites[qualityValue];
        SetQualityState();
    }

    private void SetQualityState()
    {
        //High
        if(qualityValue == 0)
        {
            globalVolume.SetActive(true);
        }
        //Low
        else
        {
            globalVolume.SetActive(false);
        }

        //Save Quality
        PlayerPrefs.SetInt("Quality", qualityValue);
        assetsGraphicsState.CheckGraphicsState(qualityValue);
    }

    private void LoadQuality()
    {
        if (PlayerPrefs.HasKey("Quality"))
            qualityValue = PlayerPrefs.GetInt("Quality");
        else
            qualityValue = 0;

        qualityImage.sprite = qualitySprites[qualityValue];
        SetQualityState();
    }
    #endregion

    #region Audio
    public void ChangeAudioValue()
    {
        AudioManager.instance.Play("Select");
        audioValue = audioValue == 0 ? audioValue = 1 : audioValue = 0;
        SetAudioState();
    }
    
    void SetAudioState()
    {
        //Unmute
        if (audioValue == 1)
        {
            audioImage.sprite = audioSprites[1];
            audioMixer.SetFloat("SoundVolume", 0);
        }
        //Mute
        else
        {
            audioImage.sprite = audioSprites[0];
            audioMixer.SetFloat("SoundVolume", -80);
        }

        //Save sound
        PlayerPrefs.SetInt("Audio", audioValue);
    }

    void LoadAudio()
    {
        //Load sound value
        if (PlayerPrefs.HasKey("Audio"))
            audioValue = PlayerPrefs.GetInt("Audio");
        else
            audioValue = 1;

        SetAudioState();
    }
    #endregion

    #region PlayerPrefsRemove
    public void RemoveAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        FindObjectOfType<Transition>().PlayOutTransition("Loading");
    }

    #endregion
}
