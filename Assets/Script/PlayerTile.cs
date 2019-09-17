using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTile : MonoBehaviour
{
    private PlayerController player;
    public int tileNumber;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        tileNumber = int.Parse(gameObject.name);
    }

    //플레이어 이동 구현 필요(애니메이션 점프)
    //공격 중일때만 가능하도록 수정
    public void OnClickTile(Image image)
    {
        player.currentPos = tileNumber;
        Vector3 newPos = transform.position;
        newPos.z = player.transform.position.z;
        player.transform.position = newPos;

        Color newColor = Color.black;
        Color oldColor = Color.black;

        oldColor.a = newColor.a = TileManager.TILE_ALPHA;

        string unSelectColor = TileManager.PLAYER_NORMAL_TILE;
        string selectColor = TileManager.PLAYER_SELLECT_TILE;

        newColor.r = Convert.ToInt32(selectColor.Substring(0, 2), 16) / 255.0f;
        newColor.g = Convert.ToInt32(selectColor.Substring(2, 2), 16) / 255.0f;
        newColor.b = Convert.ToInt32(selectColor.Substring(4, 2), 16) / 255.0f;

        oldColor.r = Convert.ToInt32(unSelectColor.Substring(0, 2), 16) / 255.0f;
        oldColor.g = Convert.ToInt32(unSelectColor.Substring(2, 2), 16) / 255.0f;
        oldColor.b = Convert.ToInt32(unSelectColor.Substring(4, 2), 16) / 255.0f;

        image.color = newColor;
        TileManager.Instance.UnSelectTile(tileNumber);
    }
}
