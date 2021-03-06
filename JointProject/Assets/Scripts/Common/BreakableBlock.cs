//kanoko
//壊せるブロックにアタッチする

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private GameObject effect = null;
    
    void OnCollisionEnter(Collision collision)
    {
        //衝突したのが弾だったら壊れる
        if(collision.gameObject.GetComponent<Bullet>() != null){
            //破壊エフェクトを生成
            Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
