using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController, IBulletShooting
{
    // Start is called before the first frame update
    void Start()
    {
        InitObj(GameManager.PLAYER);
        state = ObjectTypes.SHOOTING;
        animator.SetTrigger("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //수정점 목표 지점 설정 및 총알 생성이 적 몬스터도 같은 메서드를 탈 수 있도록 이벤트화
    //공격할 타일을 받아 해당 지점에 도착 시 적의 존재 유무 확인 및 있을때 데미지를 넣고 제거 없으면 바로 제거
    //공격하는 타일은 공격중임을 나타내는 이미지가 나오도록 수정
    IEnumerator IBulletShooting.Shooting()
    {
        yield return null;
        GameObject go = GameManager.LoadBullet(GameManager.BULLET_PATH);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletController>().SetBulletInfo(bullet, 0);
    }

    public void Idle()
    {
        state = ObjectTypes.IDLE;
        animator.SetTrigger("Idle");
    }
}
