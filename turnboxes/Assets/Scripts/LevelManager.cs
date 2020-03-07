using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // public delegate void FigureClick();
    public static event System.Action OnFigureClick;
    public delegate void LevelFinish(bool ifWin);
    public static event LevelFinish OnLevelFinish;

    public static LevelManager Instance;
    public Transform board;
    public BoxBehaviour[,] boxContainer;
    public BoxValue[] boxIdentity;
    public GameObject boxPrefab;
    public int currentLevel;
    public int boardLength = 9;
    public bool canClick;
    public int clickAmount;
    public int maxSxore;
    public int currentScore;
    public int clicked;
    public int point;

    public bool gameStarted;
    public GameManager gameManager;

    // Vector3 startPosition;
    internal int respondTileCount;
    internal int pieceTileCount;
    private bool forceOneTime;

    public delegate void QueueFunc(BoxBehaviour box);
    public Queue<System.Action> queue;



    private void Awake()
    {
        Instance = this;
        currentLevel = 12;// PlayerPrefs.GetInt("CurrentLevel", 1);
        // currentLevel = 1;
        boardLength = Mathf.Min(9, (currentLevel / 2 + 5));
        boxContainer = new BoxBehaviour[boardLength, boardLength];
        queue = new Queue<System.Action>();
        // startPosition = boxPrefab.transform.position;// new Vector3(camPos.x - actionZoneLength / 2, camPos.y + actionZoneLength / 2, 0);
        // Debug.Log("start position - " + startPosition);
        maxSxore = (int)(currentLevel * currentLevel * 0.9f) + 5;
        clickAmount = (int)(currentLevel * 1.2f) + 5;
        BoxBehaviour.OnFinishAnimation += CanClickChecker;
        pieceTileCount = boardLength * boardLength - 1;


    }

    private void OnDestroy()
    {
        BoxBehaviour.OnFinishAnimation -= CanClickChecker;

    }
    private void Start()
    {
        PutBoxes();
        forceOneTime = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick && forceOneTime && gameStarted)
        {
            point = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var boxInfo = hit.transform.GetComponentInParent<BoxBehaviour>();
                if (boxInfo != null)
                {
                    if (clicked <= clickAmount)
                    {

                        // canClick = false;

                        if (boxInfo.Trigger(boxInfo))
                        {
                            canClick = false;
                            clicked++;
                            
                            if (OnFigureClick != null)
                                OnFigureClick();
                        }


                    }
                }
            }
        }

        if(canClick)
        {
            if (queue.Count > 0)
            {
                // Debug.LogError("Deque" + queue.Count);
                queue.Dequeue().Invoke();
                canClick = false;
            }
        }
    }

    void PutBoxes()
    {
        var offset = 0.1f;
        var scale = Vector3.one * (9f / (boardLength + offset));
        var startPosition = (boardLength / 2f * (scale.x + offset) - scale.x / 2) * new Vector2(-1f, 1f);// + scale.x/2 * Vector2.one;
        // var startPosition = Vector3.zero;
        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardLength; j++)
            {
                // var position = new Vector3(startPosition.x + j * (actionZoneLength / (boardLength - 1)), startPosition.y - i * (actionZoneLength / (boardLength - 1)), 0);
                var position = new Vector3(startPosition.x + j * (scale.x + offset), startPosition.y - i * (scale.x + offset), 0);


                var boxClone = Instantiate(boxPrefab, board.transform);
                boxClone.transform.localPosition = position;
                boxClone.transform.localScale = scale;
                var box = boxClone.GetComponent<BoxBehaviour>();
                box.address.x = j;
                box.address.y = i;
                boxContainer[j, i] = box;
            }
        }
    }


    private void CanClickChecker()
    {
        respondTileCount++;
        // Debug.LogError("Finish Anim" + respondTileCount);
        // Debug.Log(respondTileCount);
        if (respondTileCount == pieceTileCount + 1)
        {
            respondTileCount = 0;
            pieceTileCount = 0;
        
            canClick = true;
            if(queue.Count == 0)
                CheckIfFinish();

        }
    }

    void CheckIfFinish()
    {
        if (currentScore >= maxSxore && forceOneTime)
        {
            Finish();
            forceOneTime = false;
            canClick = false;
        }
        else if (clickAmount - clicked <= 0)
        {
            OnLevelFinish(false);
            canClick = false;
            forceOneTime = false;
        }
    }

    void Finish()
    {
        OnLevelFinish(true);
        
        var n = PlayerPrefs.GetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("CurrentLevel", ++n);
    }
}
