using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알 발사 메서드
public interface IBulletBehavior
{
    IEnumerator Attack();
    ImpuseReturnValue SetTarget();

}
