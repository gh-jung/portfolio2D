using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletInfo info;
    private SpriteRenderer mRenderer;
    private BoxCollider2D mCollider;
    private int destinationPos;// 공격할 타일

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (int)info.type * info.speed * Time.deltaTime);
    }

    //총알 생성 시 반드시 호출 필요
    public void SetBulletInfo(BulletInfo info, int destinationPos)
    {
        this.info = Instantiate(info);
        this.destinationPos = destinationPos;

        mRenderer = GetComponent<SpriteRenderer>();
        mCollider = GetComponent<BoxCollider2D>();
        mRenderer.sprite = info.bulletImage;
        mCollider.size = new Vector2(info.bulletImage.rect.width / GameManager.SIZE_X, info.bulletImage.rect.height / GameManager.SIZE_Y);
    }

    public ObjectInfo OnDemage(ObjectInfo obj)
    {
        obj.health = Mathf.Max(obj.health - info.demage, 0);
        return obj;
    }
}
