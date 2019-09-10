using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator animator;

    public BulletInfo bullet;
    public ObjectInfo character;
    public Transform bulletInitPos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ObjectInfo tempInfo = GameManager.Instance.CreateObj(GameManager.PLAYER);
        if (tempInfo != null)
        {
            character = Instantiate(tempInfo);
            bullet = character.bullet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //수정점 목표 지점 설정 및 총알 생성이 적 몬스터도 같은 메서드를 탈 수 있도록 이벤트화
    public void Shooting()
    {
        var tempGo = Resources.Load(GameManager.BULLET_PATH) as GameObject;
        GameObject go = Instantiate(tempGo);
        go.transform.position = bulletInitPos.position;
        go.GetComponent<BulletObj>().SetBulletObj(bullet, 0);
    }
}
