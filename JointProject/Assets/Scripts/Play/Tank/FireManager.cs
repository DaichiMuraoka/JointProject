using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    //通常弾設定
    [SerializeField]
    private Bullet nomalBullet = null;

    [SerializeField]
    private float nomalFireSpeed = 500;

    //高角弾設定
    [SerializeField]
    private Bullet flyBullet = null;

    [SerializeField]
    private float flyFireSpeed = 150;

    [SerializeField]
    private float flyUpPower = 2f;

    //クールタイム
    [SerializeField]
    private float coolTime = 2f;

    //発射
    public void Fire(FIRE_TYPE fireType)
    {
        Debug.Log(gameObject.name + " fire!");
        Tank tank = GetComponent<Tank>();
        Vector3 muzzlePos = tank.GetMuzzle(fireType).position;  //発射位置取得
        Bullet bullet = Instantiate(GetBullet(fireType), muzzlePos, Quaternion.identity);   //砲弾生成
        bullet.GetComponent<Rigidbody>().AddForce(GetFireForce(fireType));  //弾を飛ばす
        bullet.Side = tank.Side;    //PLAYER or ENEMY
        GetComponent<Controller>().Freeze(coolTime);    //coolTime秒フリーズ
    }

    private Bullet GetBullet(FIRE_TYPE fireType)
    {
        if (fireType == FIRE_TYPE.NOMAL)
        {
            return nomalBullet;
        }
        else
        {
            return flyBullet;
        }
    }

    //発射の角度と位置取得
    private Vector3 GetFireForce(FIRE_TYPE fireType)
    {
        if (fireType == FIRE_TYPE.NOMAL)
        {
            return transform.forward * nomalFireSpeed;
        }
        else
        {
            return new Vector3(transform.forward.x / flyUpPower, flyUpPower, transform.forward.z / flyUpPower) * flyFireSpeed;
        }
    }
}

public enum FIRE_TYPE
{
    NOMAL,
    FLY
}
