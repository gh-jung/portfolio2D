using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController, IBulletBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        InitObj(GameManager.PLAYER);
        TileManager.Instance.SetPlayerPos(this.gameObject);
        OnIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPos(Vector3 newPos)
    {
        StartCoroutine(Move(newPos));
    }

    //클릭한 적 타일이 있을경우 && 해당 라인일 경우 -> 해당 위치 공격
    //그 외 -> 해당 라인에서 가장 가까운 적 공격
    public TargetReturnValue SetTarget()
    {
        TargetReturnValue returnValue;
        int playerCurrentLine = currentPos / 4;
        int selectEnemyPos = TileManager.Instance.SelectEnemyPos;
        int destinationTile;

        if (selectEnemyPos != -1 && playerCurrentLine == (selectEnemyPos / 3))
        {
            destinationTile = selectEnemyPos;
        }
        else
        {
            destinationTile = GameManager.Instance.IsExistEnemy(playerCurrentLine);
        }

        returnValue.destinationPosX = TileManager.Instance.enemyMoveAbleTiles[destinationTile].transform.position.x;
        returnValue.destinationTile = destinationTile;
        return returnValue;
    }

    public void SetAttackState()
    {
        if (GameManager.Instance.IsExistEnemy(currentPos / 4) == int.MaxValue)
        {
            OnIdle();
        }
        else
        {
            OnAttack();
        }
    }

    //공격할 타일을 받아 해당 지점에 도착 시 적의 존재 유무 확인 및 있을때 데미지를 넣고 제거 없으면 바로 제거
    IEnumerator IBulletBehavior.Attack()
    {
        yield return null;
        GameObject go = GameManager.LoadBullet(GameManager.BULLET_PATH);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletController>().SetBulletInfo(bullet, SetTarget);
    }

    IEnumerator Move(Vector3 newPos)
    {
        float distance = Vector3.Distance(transform.position, newPos);
        if (transform.position.x > newPos.x)
        {
            Quaternion temp = Quaternion.identity;
            temp.y = 180;
            transform.rotation = temp;
        }
        float moveSpeed = Time.deltaTime;
        while (distance > 0.1f)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed);
            moveSpeed += Time.deltaTime;
            distance = Vector3.Distance(transform.position, newPos);
        }

        transform.position = newPos;
        transform.rotation = Quaternion.identity;

        SetAttackState();
    }
}
