using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    
    [SerializeField]
    private Bullet nomalBullet = null;

    [SerializeField]
    private KeyCode nomalFireKey = KeyCode.E;

    [SerializeField]
    private float nomalFireSpeed = 500;

    [SerializeField]
    private Bullet flyBullet = null;

    [SerializeField]
    private KeyCode flyFireKey = KeyCode.F;

    [SerializeField]
    private float flyFireSpeed = 100;

    [SerializeField]
    private float flyUpPower = 3f;

    [SerializeField]
    private Transform muzzle = null;

    private void Update()
    {
        if (Input.GetKeyDown(nomalFireKey))
        {
            Fire(FIRE_TYPE.NOMAL);
        }
        if (Input.GetKeyDown(flyFireKey))
        {
            Fire(FIRE_TYPE.FLY);
        }
    }

    public virtual void Fire(FIRE_TYPE fireType)
    {
        if(fireType == FIRE_TYPE.NOMAL)
        {
            Bullet bullet = Instantiate(nomalBullet, muzzle.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * nomalFireSpeed);
        }
        else
        {
            Bullet bullet = Instantiate(flyBullet, muzzle.position, Quaternion.identity);
            Vector3 force = new Vector3(transform.forward.x / flyUpPower, flyUpPower, transform.forward.z / flyUpPower) * flyFireSpeed;
            bullet.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
public enum FIRE_TYPE
{
    NOMAL,
    FLY
}
