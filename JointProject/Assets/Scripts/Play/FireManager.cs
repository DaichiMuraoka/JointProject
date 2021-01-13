using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    
    [SerializeField]
    private Bullet nomalBullet = null;

    [SerializeField]
    private Bullet flyBullet = null;

    [SerializeField]
    private KeyCode nomalFire = KeyCode.E;

    [SerializeField]
    private KeyCode flyFire = KeyCode.F;

    private FIRE_TYPE fireType = FIRE_TYPE.DEFAULT;

    private void Update()
    {
        if (Input.GetKeyDown(nomalFire))
        {

        }
        if (Input.GetKeyDown(flyFire))
        {

        }
    }

    public virtual void Fire()
    {

    }
}
public enum FIRE_TYPE
{
    DEFAULT,
    NOMAL,
    FLY
}
