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
    //총알에 이미지가 여러장일경우 애니메이션 동작하도록 수정
    public void SetBulletInfo(BulletInfo info, int destinationPos)
    {
        this.info = Instantiate(info);
        this.destinationPos = destinationPos;

        mRenderer = GetComponent<SpriteRenderer>();
        mCollider = GetComponent<BoxCollider2D>();

        if(info.bulletImages.Length > 1)
        {
            StartCoroutine(ChangeSprite());
        }
        else
        {
            mRenderer.sprite = info.bulletImages[0];
        }
        mCollider.size = new Vector2(info.bulletImages[0].rect.width / GameManager.SIZE_X, info.bulletImages[0].rect.height / GameManager.SIZE_Y);
    }

    protected ObjectInfo OnDemage(ObjectInfo obj)
    {
        obj.health = Mathf.Max(obj.health - info.demage, 0);
        return obj;
    }

    IEnumerator ChangeSprite()
    {
        int i = 0;
        int length = info.bulletImages.Length;
        while (gameObject)
        {
            mRenderer.sprite = info.bulletImages[i];
            i = (i + 1) % length;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
