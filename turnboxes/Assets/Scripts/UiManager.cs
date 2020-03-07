using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public LevelManager levelManager;
    public GameManager gameManager;
    public GameObject gameOverPanel;
    public GameObject startPanel;
    public Button startButton;
    public GameObject gamePanel;
    public Slider scoreSlider;
    public Transform sliderPivot;
    public Text currentScore;
    public Text currentLevelText;
    public Text nextLevelText;
    public Text clickLeft;
    public Text headerOnEndPanel;
    public Text levelTextOnEndPanel;
    public Text scoreTextOnEndPanel;
    public Button restartButton;
    public Button startSceneButton;
    public Button nextLevelButton;
    public bool increase;
    public GameObject pointPrefab;

    float destination;


    private void Awake()
    {
        Instance = this;
        LevelManager.OnFigureClick += ClicksLeft;
        // LevelManager.OnFigureClick += ScoreSlider;
        LevelManager.OnLevelFinish += EndPanel;
    }

    private void Start()
    {
        currentLevelText.text = levelManager.currentLevel.ToString();
        nextLevelText.text = (levelManager.currentLevel + 1).ToString();
        scoreSlider.maxValue = levelManager.maxSxore;

        startButton.onClick.AddListener(startGame);
        restartButton.onClick.AddListener(gameManager.LoadMain);
        nextLevelButton.onClick.AddListener(gameManager.LoadMain);
        startSceneButton.onClick.AddListener(gameManager.LoadMain);
        ClicksLeft();

        if(PlayerPrefs.GetInt("IsPlaying") == 1)
        {
            startGame();
        }
    }

    private void startGame()
    {
        levelManager.gameStarted = true;
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    private void Update()
    {
        // if (increase)
        // {
        //     scoreSlider.value = Mathf.Lerp(scoreSlider.value, levelManager.currentScore, Time.deltaTime * 3);
        //     if (levelManager.currentScore - scoreSlider.value < .2f)
        //     {
        //         increase = false;
        //     }
        //     if (scoreSlider.value >= destination - .4f)
        //     {
        //         currentScore.text = ((int)destination).ToString();
        //         destination++;
        //     }
        // }

        scoreSlider.value = Mathf.Lerp(scoreSlider.value, destination, Time.deltaTime*3);
        currentScore.text = ((int)Mathf.Round(scoreSlider.value)).ToString();
    }

    private void OnDestroy()
    {
        LevelManager.OnFigureClick -= ClicksLeft;
        // LevelManager.OnFigureClick -= ScoreSlider;
        LevelManager.OnLevelFinish -= EndPanel;
    }

    void ClicksLeft()
    {
        var n = levelManager.clickAmount - levelManager.clicked;
        clickLeft.text = n.ToString();
    }

    // void ScoreSlider()
    // {
    //     // var n = levelManager.currentScore;
    //     // scoreSlider.value = n;

    // }

    void EndPanel(bool success)
    {
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        // levelTextOnEndPanel.text = levelManager.currentLevel + " Level";
        // scoreTextOnEndPanel.text = levelManager.currentScore.ToString() + " / " + levelManager.maxSxore.ToString();
        if (success)
        {
            restartButton.gameObject.SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            headerOnEndPanel.text = "Level " + levelManager.currentLevel +" Completed";
        }
        else
        {
            restartButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
            headerOnEndPanel.text = "You lost";
        }
    }

    public void CreateCoin(Vector3 position)
    {
        StartCoroutine(PointTransform(position));
    }

    IEnumerator PointTransform(Vector3 position)
    {
        var pointCoin = Instantiate(pointPrefab, position, Quaternion.identity);
        var slidPos = (Vector2)Camera.main.ScreenToWorldPoint(sliderPivot.position);
        while (((Vector2)pointCoin.transform.position - (Vector2)slidPos).magnitude >= .5f)
        {
            pointCoin.transform.Translate(((Vector2)slidPos - (Vector2)pointCoin.transform.position).normalized * .3f);
            yield return null;
        }
        increase = true;
        destination = levelManager.currentScore;
        Destroy(pointCoin);
    }
}
