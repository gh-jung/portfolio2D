using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectController, IBulletBehavior
{
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        InitObj(GameManager.ENEMY_ROCKET_MAN);
        state = ObjectTypes.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ObjectTypes.IDLE)
        {
            currentTime += Time.deltaTime;
            if (currentTime > character.AttackSpeed)
            {
                state = ObjectTypes.ATTACK;
                animator.SetTrigger("Attack");
                currentTime = 0;
            }
        }
    }

    //플레이어와의 충돌 시 같은 라인인지 확인 필요 같은 라인일때 로직 구현
    public ImpuseReturnValue SetTarget()
    {

        ImpuseReturnValue returnValue;
        returnValue.destinationPosX = GameManager.SCREEN_LEFT;
        returnValue.destinationTile = currentPos / 3;

        return returnValue;
    }

    IEnumerator IBulletBehavior.Attack()
    {
        animator.speed = 0;
        yield return new WaitForSeconds(0.3f);
        animator.speed = 1;
        GameObject go = GameManager.LoadBullet(GameManager.BULLET_PATH);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletController>().SetBulletInfo(bullet, SetTarget);
    }
}
