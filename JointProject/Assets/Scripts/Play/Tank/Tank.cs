using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private Transform nomalMuzzle = null;

    [SerializeField]
    private Transform flyMuzzle = null;

    public Transform GetMuzzle(FIRE_TYPE fireType)
    {
        if(fireType == FIRE_TYPE.NOMAL)
        {
            return nomalMuzzle;
        }
        else
        {
            return flyMuzzle;
        }
    }

    private SIDE side = SIDE.NONE;

    public SIDE Side
    {
        get { return side; }
        set
        {
            side = value;
            name = value.ToString();
            if (GetComponent<Controller>())
            {
                Destroy(GetComponent<Controller>());
            }
            if(value == SIDE.NONE)
            {
                Debug.LogError("side is none.");
            }
            else if(value == SIDE.PLAYER)
            {
                gameObject.AddComponent<PlayerController>();
            }
            else
            {
                //gameObject.AddComponent<EnemyController>();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if(bullet == null)
        {
            return;
        }
        else
        {
            if (Side == SIDE.ENEMY && bullet.Side == SIDE.PLAYER)
            {
                Explosion(collision);
            }
            else if (Side == SIDE.PLAYER && bullet.Side == SIDE.ENEMY)
            {
                Explosion(collision);
            }
        }
    }

    private void Explosion(Collision collision)
    {
        collision.gameObject.GetComponent<Bullet>().Explosion();
        Destroy(gameObject);
    }
}
