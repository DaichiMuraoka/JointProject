using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

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

    [SerializeField]
    private SIDE side = SIDE.NONE;

    public SIDE Side
    {
        get { return side; }
    }

    private Camera playerCamera = null;

    public Camera Camera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }

    private void Start()
    {
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
                StartCoroutine(Explosion(collision));
            }
            else if (Side == SIDE.PLAYER && bullet.Side == SIDE.ENEMY)
            {
                StartCoroutine(Explosion(collision));
            }
        }
    }

    private IEnumerator Explosion(Collision collision)
    {
        Debug.Log(name + " is explosion");
        Controller controller = GetComponent<Controller>();
        controller.State = MOVE_STATE.FREEZE;
        collision.gameObject.GetComponent<Bullet>().Explosion();
        BattleManager.Instance.DeleteTankList(this);
        yield return null;
        Animator animator = controller.Animator;
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void SetController()
    {
        if (side == SIDE.PLAYER)
        {
            if (GetComponent<PlayerController>() == null)
            {
                gameObject.AddComponent<PlayerController>();
            }
        }
        else
        {
            PlayerController playerController = GetComponent<PlayerController>();
            if (playerController != null)
            {
#if UNITY_EDITOR
                EditorApplication.delayCall += () => DestroyImmediate(playerController);
#else
                Destroy(enemyController);
#endif
            }
        }

        if (side == SIDE.ENEMY)
        {
            if (GetComponent<EnemyController>() == null)
            {
                gameObject.AddComponent<EnemyController>();
            }
        }
        else
        {
            EnemyController enemyController = GetComponent<EnemyController>();
            if (enemyController != null)
            {
#if UNITY_EDITOR
                EditorApplication.delayCall += () => DestroyImmediate(enemyController);
#else
                Destroy(enemyController);
#endif
            }
            EnemyMove enemyMove = GetComponent<EnemyMove>();
            if (enemyMove != null)
            {
#if UNITY_EDITOR
                EditorApplication.delayCall += () => DestroyImmediate(enemyMove);
#else
                Destroy(enemyMove);
#endif
            }
        }
    }

    private void OnValidate()
    {
        SetController();
    }

    public void AddTankList()
    {
        BattleManager.Instance.AddTankList(this);
    }
}

public enum SIDE
{
    NONE,
    PLAYER,
    ENEMY
}
