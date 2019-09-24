using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//충돌 판정 델리게이트
public delegate ImpuseReturnValue Impuse();

public class BulletController : MonoBehaviour
{
    public BulletInfo info;
    private SpriteRenderer mRenderer;
    private event Impuse impuseEvent;
    private float destinationX;
    public int DestinationTile
    {
        get; set;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (int)info.type * info.speed * Time.deltaTime);

        if (info.type == BulletTypes.PLAYER && transform.position.x > destinationX ||
            info.type == BulletTypes.EMENY && transform.position.x < destinationX)
        {
            //데미지를 받아서 인식 하는 부분도 이벤트로 처리
            //GameManager.Instance.OnDamage(this);
            Destroy(this.gameObject);
        }
    }

    //총알 생성 시 반드시 호출 필요
    //플레이어 적 모두 적용가능하도록 수정 필요
    public void SetBulletInfo(BulletInfo mInfo, Impuse bulletImpuse)
    {
        info = Instantiate(mInfo);
        impuseEvent = bulletImpuse;
        ImpuseReturnValue tempValue = impuseEvent();

        DestinationTile = tempValue.destinationTile;
        destinationX = tempValue.destinationPosX;

        mRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D mCollider = GetComponent<BoxCollider2D>();

        if (info.bulletImages.Length > 1)
        {
            StartCoroutine(ChangeSprite());
        }
        else
        {
            mRenderer.sprite = info.bulletImages[0];
        }
        mCollider.size = new Vector3(
            info.bulletImages[0].rect.width / GameManager.SIZE_X, 
            info.bulletImages[0].rect.height / GameManager.SIZE_Y,
            0);
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
