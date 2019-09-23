using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public const int SIZE_X = 20;
    public const int SIZE_Y = 20;
    public const string PLAYER = "Characters/player";
    public const string ENEMY_ROCKET_MAN = "Characters/Enemy_Roket_Man";
    public const string BULLET_PATH = "Prefabs/bullet";
    public const int SCREEN_LEFT = -10;
    public const int SCREEN_RIGHT = 10;

    public List<ObjectController> enemys = new List<ObjectController>();

    public static ObjectInfo LoadScriptable(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/" + obj) as ObjectInfo;
        return temp;
    }

    public static GameObject LoadBullet(string path) 
    {
        return Instantiate(Resources.Load(GameManager.BULLET_PATH) as GameObject);
    }

    //플레이어와 같은 라인 찾고 해당라인에서 가장 가까운 적 찾기
    public int IsExistEnemy(int lineNum)
    {
        int count = enemys.Count;
        int returnValue = int.MaxValue;
        for (int i = 0; i < count; i++)
        {
            if((enemys[i].currentPos / 3) == lineNum)
            {
                if(returnValue > enemys[i].currentPos)
                {
                    returnValue = enemys[i].currentPos;
                }
            }
        }

        return returnValue;
    }

    public void OnDamage(BulletController bulletPos)
    {
        int count = enemys.Count;
        for (int i = 0; i < count; i++)
        {
            if (enemys[i].currentPos == bulletPos.DestinationTile)
            {
                enemys[i].OnDemage(bulletPos.info);
            }
        }
    }
}
