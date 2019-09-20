using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTile : Tile
{
    public override void OnClickTile(Image image)
    {
        base.OnClickTile(image);

        //1.같은 라인의 타일인지 확인
        //2.같으면 넘어가기
        //3.다르면 해당 라인으로 위아래 이동 실행
        //player.currentPos = tileNumber;
        //Vector3 newPos = transform.position;
        //newPos.z = player.transform.position.z;
        //player.OnMove(newPos);
    }
}
