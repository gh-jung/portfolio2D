using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : Singleton<TileManager>
{
    public const string PLAYER_SELLECT_TILE = "FF6400";
    public const string ENEMY_SELLECT_TILE = "00C800";
    public const string PLAYER_NORMAL_TILE = "9C3200";
    public const string ENEMY_NORMAL_TILE = "006000";
    public const int PLAYER_COW = 4;
    public const int ENEMY_COW = 3;
    public const float TILE_ALPHA = 0.5f;

    private const float MAX_COLOR_VALUE = 255;
    private const int NOT_INIT = -1;

    public PlayerTile[] playerMoveAbleTiles;
    public EnemyTile[] enemyMoveAbleTiles;
    public Image[] playerTiles;
    public Image[] enemyTiles;

    private int playerPos = 6;
    private int selectEnemyPos = -1;

    public int PlayerPos
    {
        get
        {
            return playerPos;
        }
    }

    public int SelectEnemyPos
    {
        get
        {
            return selectEnemyPos;
        }
    }

    //적이 여러마리일때의 타일 배치 구현

    private void OnTileColorChange(int number, Color color, ref int targetTileNumber, ref Image[] targetTileArray)
    {
        if (targetTileNumber <= NOT_INIT || targetTileNumber == number)
        {
            targetTileNumber = number;
            return;
        }
        targetTileArray[targetTileNumber].color = color;
        targetTileNumber = number;
    }

    public void SetTileColor(int newNumber,Color oldColor, TileTypes type)
    {
        if (type == TileTypes.PLAYER_TILE)
        {
            OnTileColorChange(newNumber, oldColor, ref playerPos, ref playerTiles);
        }
        else
        {
            OnTileColorChange(newNumber, oldColor, ref selectEnemyPos, ref enemyTiles);
        }
    }

    public void UnSlectTile()
    {
        if (selectEnemyPos == -1 || selectEnemyPos == int.MaxValue)
            return;
        Color color = Color.black;
        color.a = TILE_ALPHA;
        SetColorToHexRGB(ENEMY_NORMAL_TILE, ref color);
        enemyTiles[selectEnemyPos].color = color;
        selectEnemyPos = -1;
    }

    public static void SetColorToHexRGB(string hexColorString, ref Color color)
    {
        color.r = Convert.ToInt32(hexColorString.Substring(0, 2), 16) / MAX_COLOR_VALUE;
        color.g = Convert.ToInt32(hexColorString.Substring(2, 2), 16) / MAX_COLOR_VALUE;
        color.b = Convert.ToInt32(hexColorString.Substring(4, 2), 16) / MAX_COLOR_VALUE;
    }

    public bool IsSameLine()
    {
        return (playerPos / 4) == (selectEnemyPos / 3);
    }

    public void SetMovePlay()
    {
        int difference = playerPos + ((selectEnemyPos / 3) - (playerPos / 4)) * 4;
        playerMoveAbleTiles[difference].OnClickTile(playerTiles[difference]);
    }

    public void SetPlayerPos(GameObject player)
    {
        int index = playerPos;
        player.GetComponent<ObjectController>().currentPos = index;
        player.transform.position = playerMoveAbleTiles[index].transform.position;

        Color newColor = Color.black;
        newColor.a = TILE_ALPHA;
        SetColorToHexRGB(PLAYER_SELLECT_TILE, ref newColor);
        playerTiles[index].color = newColor;
    }

    public void SetEnemyPos(GameObject enemy)
    {
        int index = selectEnemyPos;
        enemy.GetComponent<ObjectController>().currentPos = index;

        enemy.transform.position = enemyMoveAbleTiles[index].transform.position;

        Color newColor = Color.black;
        newColor.a = TILE_ALPHA;
        SetColorToHexRGB(ENEMY_SELLECT_TILE, ref newColor);
        enemyTiles[index].color = newColor;
    }
}
