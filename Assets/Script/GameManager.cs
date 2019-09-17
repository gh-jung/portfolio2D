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


    public static ObjectInfo LoadScriptable(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/" + obj) as ObjectInfo;
        return temp;
    }

    public static GameObject LoadBullet(string path) 
    {
        return Instantiate(Resources.Load(GameManager.BULLET_PATH) as GameObject);
    }
}
