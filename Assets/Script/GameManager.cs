using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
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

    public static ObjectInfo LoadScriptable(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/" + obj) as ObjectInfo;
        return temp;
    }

    public static GameObject LoadBullet(string path) 
    {
        return Instantiate(Resources.Load(GameManager.BULLET_PATH) as GameObject);
    }

    private void Start()
    {
        gamePoint = 0;
        pointUI.text = String.Format("{0:D8}", gamePoint);
        currentTime = 0;
        respawnCount = 0;
        respawnTime = 3;
    }

    private void Update()
    {
        if (EmenysCount != enemys.Count && player.character.Alive)
        {
            currentTime += Time.deltaTime;
            if (currentTime > respawnTime)
            {
                GameObject enemyObj = Instantiate(enemy);
                ObjectController enemyController = enemyObj.GetComponent<ObjectController>(); ;
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

    public static int GetPoint()
    {
        return GameManager.Instance.gamePoint;
    }

    public static void AddPoint(int point)
    {
        GameManager.Instance.gamePoint += point;
        GameManager.Instance.pointUI.text = String.Format("{0:D8}", GameManager.Instance.gamePoint);
    }

    public void AllWait()
    {
        foreach(ObjectController controller in enemys)
        {
            controller.SetIsIdle(true);
        }
    }
}
