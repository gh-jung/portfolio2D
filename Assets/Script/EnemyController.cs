using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ObjectController, IBulletShooting
{
    // Start is called before the first frame update
    void Start()
    {
        InitObj(GameManager.ENEMY_ROCKET_MAN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shooting()
    {
        GameObject go = GameManager.Instance.LoadBullet(GameManager.BULLET_PATH);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletController>().SetBulletInfo(bullet, 0);
    }
}
