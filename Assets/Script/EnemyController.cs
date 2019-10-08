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
        if (state == ObjectTypes.IDLE && !isIdle)
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

    public TargetReturnValue SetTarget()
    {
        TargetReturnValue returnValue;
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
        OnPlaySound(bullet.bulletSound[UnityEngine.Random.Range(0, bullet.bulletSound.Length)]);
    }
}
