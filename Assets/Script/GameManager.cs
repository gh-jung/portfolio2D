using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static int SIZE_X = 20;
    public static int SIZE_Y = 20;
    public static string PLAYER = "player";
    public static string BULLET_PATH = "Prefabs/bullet";

    public ObjectInfo CreateObj(string obj)
    {
        ObjectInfo temp = Resources.Load("Scriptable/Characters/" + obj) as ObjectInfo;
        return temp;
    }
}
