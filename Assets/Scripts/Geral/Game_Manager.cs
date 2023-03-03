using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    #region Variables
    public static bool isPlaying;
    public static Game_Manager instance;

    [Header("Player")]
    [SerializeField] private Transform playerPos;
    public PlayerHealth playerHealth;
    public InfinitePlayerController infinitePlayerController;

    [Header("UI")]
    [SerializeField] private DOTweenAnimation touchPlayDOT;
    [SerializeField] private GameObject pauseHolder;

    [Header("Coins")]
    [SerializeField] private Prop_Scriptable props;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private DOTweenAnimation shopButtonDOT;
    [SerializeField] private DOTweenAnimation coinDOT;
    [SerializeField] private GameObject shopHolder;
    public float coins;
    private float _coinsTimer = 0;
    private bool canAddCoinIndex = false;
    [HideInInspector] public int coinIndex;

    [Header("ADS")]
    [SerializeField] private AdsManager adMan;
    [SerializeField] private SaveLastPositions savePositions;
    public static int adCounter;
    #endregion

    #region Methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        StartGame();
        LoadCoins();
    }

    private void Update()
    {
        CheckPlayTouch();
        //CoinsTimer();
    }
    #endregion

    #region Custom Methods
    //Game
    public void StartGame()
    {
        if (isPlaying)
        {
            infinitePlayerController.SetAllControls(true);
            shopButtonDOT.DOPlay();
            coinDOT.DOPlay();
            touchPlayDOT.DOPlay();
        }
    }

    public void PauseGame()
    {
        if (isPlaying)
        {
            isPlaying = false;
            infinitePlayerController.SetAllControls(false);
            infinitePlayerController.IdlePlayerAnim();
        }
    }

    private void CheckPlayTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && CheckCanPlay())
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                isPlaying = true;
                StartGame();
            }
        }

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && CheckCanPlay())
        {
            isPlaying = true;
            StartGame();
        }
        #endif
    }

    private bool CheckCanPlay()
    {
        if (!isPlaying && !pauseHolder.activeInHierarchy && !shopHolder.activeInHierarchy)
            return true;
        return false;
    }

    //Coin
    private void CoinsTimer()
    {
        if (isPlaying)
        {
            _coinsTimer += 0.1f * Time.deltaTime;
            canAddCoinIndex = true;
        }

        if (_coinsTimer >= 60)
        {
            AddCoinIndex();
            _coinsTimer = 0;
        }
    }

    private void AddCoinIndex()
    {
        if (canAddCoinIndex && coinIndex < props.propType.Length)
        {
            coinIndex++;
            canAddCoinIndex = false;
        }
    }

    private void CoinText()
    {
        coinsText.text = "<sprite=0>" + FormaterNumber.FormatNumber(coins);
    }

    public void AddCoin(float valueToAdd)
    {
        coins += valueToAdd;
        PlayerPrefs.SetFloat("coins", coins);
        CoinText();
    }

    public void RemoveCoin(float valueToRemove)
    {
        coins -= valueToRemove;
        PlayerPrefs.SetFloat("coins", coins);
        CoinText();
    }

    public void LoadCoins()
    {
        coins = PlayerPrefs.GetFloat("coins");
        CoinText();
    }

    //Distance
    public void SaveBestDistance(float meters)
    {
        if (PlayerPrefs.GetFloat("bestMeters") < meters)
            PlayerPrefs.SetFloat("bestMeters", meters);
        savePositions.SaveLastPlayerPosition();
    }

    public float LoadBestDistance()
    {
        if (PlayerPrefs.HasKey("bestMeters"))
            return PlayerPrefs.GetFloat("bestMeters");
        else
            return 0;
    }

    //ADS
    public void ADD_ADCount()
    {
        adCounter++;
        CheckAdCount();
    }

    private void CheckAdCount()
    {
        if(adCounter >= 15)
        {
            adCounter = 0;
            adMan.PlayAd();
            PauseGame();
        }
    }
    #endregion
}
