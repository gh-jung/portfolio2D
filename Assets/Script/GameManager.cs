using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static GameManager gameManager;

    public const int SIZE_X = 20;
    public const int SIZE_Y = 20;
    public const string PLAYER = "Characters/player";
    public const string ENEMY_ROCKET_MAN = "Characters/Enemy_Roket_Man";
    public const string BULLET_PATH = "Prefabs/bullet";
    public const int SCREEN_LEFT = -10;
    public const int SCREEN_RIGHT = 10;

    public TextMeshProUGUI pointUI;
    private int gamePoint;

    public List<ObjectController> enemys = new List<ObjectController>();
    public ObjectController player;

    public GameObject enemy;
    public float respawnTime = 3;
    private float currentTime;
    private int respawnCount = 0;

    private int EmenysCount
    {
        get
        {
            int count = 0;
            foreach(ObjectController controller in enemys)
            {
                if(controller)
                {
                    ++count;
                }
            }

            return count;
        }
    }

    private int killCount;
    private float playTime;

    public Image[] windows;
    public TextMeshProUGUI windowTitle;
    public TextMeshProUGUI[] menuNames;
    public TextMeshProUGUI[] menuValue;

    public Button[] buttons;

    public AudioSource backgroundSoundSource;
    public AudioClip overSound;

    public static ObjectInfo LoadScriptable(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/" + obj) as ObjectInfo;
        return temp;
    }

    public static GameObject LoadBullet(string path) 
    {
        return Instantiate(Resources.Load(GameManager.BULLET_PATH) as GameObject);
    }

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
    }

    private void Start()
    {

        IsShowOverview(false);
        killCount = 0;
        playTime = 0;
        gamePoint = 0;
        pointUI.text = String.Format("{0:D8}", gamePoint);
        currentTime = 0;
        respawnCount = 0;
        respawnTime = 3;
    }

    private void IsShowOverview(bool isShow)
    {
        windowTitle.enabled = isShow;
        foreach (Image image in windows)
        {
            image.enabled = isShow;
        }

        foreach (TextMeshProUGUI text in menuNames)
        {
            text.enabled = isShow;
        }

        foreach (TextMeshProUGUI text in menuValue)
        {
            text.enabled = isShow;
        }
        
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(isShow);
        }
    }

    private void Update()
    {
        if (EmenysCount != enemys.Count && player.character.Alive)
        {
            currentTime += Time.deltaTime;
            playTime += Time.deltaTime;
            if (currentTime > respawnTime)
            {
                GameObject enemyObj = Instantiate(enemy);
                ObjectController enemyController = enemyObj.GetComponent<ObjectController>();
                int tileNumber = GetEmptyPos();

                enemyObj.transform.position = TileManager.Instance.enemyMoveAbleTiles[tileNumber].transform.position;
                enemys[tileNumber] = enemyController;
                enemys[tileNumber].currentPos = tileNumber;

                currentTime = 0;
                respawnCount++;
                if (respawnCount % 10 == 0)
                {
                    respawnTime *= 0.9f;
                }
            }
        }
    }

    //플레이어와 같은 라인 찾고 해당라인에서 가장 가까운 적 찾기
    public int IsExistEnemy(int lineNum)
    {
        int count = enemys.Count;
        int returnValue = int.MaxValue;
        for (int i = 0; i < count; i++)
        {
            if(enemys[i] != null && (enemys[i].currentPos / TileManager.ENEMY_COW) == lineNum)
            {
                if(returnValue > enemys[i].currentPos)
                {
                    returnValue = enemys[i].currentPos;
                }
            }
        }

        return returnValue;
    }

    public void RemoveEnemy(int posIndex)
    {
        enemys[posIndex] = null;
    }

    public int GetEmptyPos()
    {
        
        int value = UnityEngine.Random.Range(0, enemys.Count);
        while(enemys[value] != null)
        {
            value = UnityEngine.Random.Range(0, enemys.Count);
        }

        return value;
    }

    public int GetPoint()
    {
        return gameManager.gamePoint;
    }

    public void AddPoint(int point)
    {
        gameManager.gamePoint += point;
        gameManager.pointUI.text = String.Format("{0:D8}", gameManager.gamePoint);
    }

    public void IncreseKillPoint()
    {
        ++gameManager.killCount;
    }

    public void AllWait()
    {
        foreach(ObjectController controller in enemys)
        {
            if (controller != null)
            {
                controller.SetIsIdle(true);
            }
        }
    }

    public void OnSceneChange(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator ShowPlayScoreView()
    {
        StartCoroutine(FadeOut(backgroundSoundSource));
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeIn(backgroundSoundSource, 0.5f, overSound));
        windows[0].enabled = true;
        Color color = windows[0].color;
        while (windows[0].color.a < 100.0f/255.0f)
        {
            color.a += Time.deltaTime;
            windows[0].color = color;
            yield return null;
        }
        color.a = 100.0f / 255.0f;
        windows[0].color = color;
        yield return new WaitForSeconds(1);
        foreach(Image image in windows)
        {
            image.enabled = true;
        }
        windowTitle.enabled = true;

        int count = menuNames.Length;
        for (int i = 0; i < count; ++i)
        {
            yield return new WaitForSeconds(1);
            menuNames[i].enabled = true;
            switch (i)
            {
                case 0:
                    var span = TimeSpan.FromSeconds(playTime);
                    menuValue[i].text = String.Format("{0:00} : {1:00} : {2:00}", span.TotalHours, span.Minutes, span.Seconds);

                    break;
                case 1:
                    menuValue[i].text = String.Format("{0:D8}", killCount);
                    break;
                case 2:
                    menuValue[i].text = String.Format("{0:D8}", gamePoint);
                    break;
                case 3:
                    menuValue[i].enabled = true;
                    int pointValue = gamePoint + killCount * 100 + (int)playTime * 100;
                    float showPoint = 0;
                    while(showPoint < pointValue)
                    {
                        menuValue[i].text = String.Format("{0:D8}", (int)showPoint);
                        showPoint += Time.deltaTime * (pointValue / 2);
                        yield return null;
                    }
                    menuValue[i].text = String.Format("{0:D8}", pointValue);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1);
            menuValue[i].enabled = true;
        }
        yield return new WaitForSeconds(1);
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public IEnumerator FadeIn(AudioSource source, float TargetValue, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
        while(backgroundSoundSource.volume < TargetValue)
        {
            source.volume += Time.deltaTime * (0.5f / 3);
            yield return null;
        }
        source.volume = TargetValue;
    }

    public IEnumerator FadeOut(AudioSource source)
    {
        while (backgroundSoundSource.volume > 0)
        {
            source.volume -= Time.deltaTime * (0.5f / 3);
            yield return null;
        }
        source.volume = 0;
    }
}
