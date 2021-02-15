//kanoko
//壊せるブロックにアタッチする

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //衝突したのが弾だったら壊れる
        if(collision.gameObject.GetComponent<Bullet>() != null){
            Destroy(gameObject);
        }
    }
}
