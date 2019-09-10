using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static int SIZE_X = 20;
    public static int SIZE_Y = 20;
    public static string PLAYER = "Characters/player";
    public static string BULLET_PATH = "Prefabs/bullet";

    public ObjectInfo GetScriptable(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/" + obj) as ObjectInfo;
        return temp;
    }
}
