using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : Controller
{
    [SerializeField]
    private ENEMY_MOVE move = ENEMY_MOVE.PATROL;

    public ENEMY_MOVE GetEnemyMove()
    {
        return move;
    }

    public void SetEnemyMove(ENEMY_MOVE _move)
    {
        move = _move;
        EnemyMove em = GetComponent<EnemyMove>();
        if (_move == ENEMY_MOVE.PATROL)
        {
            if(!(em is Patrol))
            {
                if(em != null)
                {
#if UNITY_EDITOR
                    EditorApplication.delayCall += () => DestroyImmediate(em);
#else
                    Destroy(em);
#endif
                }
                gameObject.AddComponent<Patrol>();
            }
        }
        else if(_move == ENEMY_MOVE.CHASE)
        {
            if (!(em is Chase))
            {
                if (em != null)
                {
#if UNITY_EDITOR
                    EditorApplication.delayCall += () => DestroyImmediate(em);
#else
                    Destroy(em);
#endif
                }
                gameObject.AddComponent<Chase>();
            }
        }
        else
        {
            if(!(em is Wait))
            {
                if (em != null)
                {
#if UNITY_EDITOR
                    EditorApplication.delayCall += () => DestroyImmediate(em);
#else
                    Destroy(em);
#endif
                }
                gameObject.AddComponent<Wait>();
            }
        }
    }

    private void OnValidate()
    {
        SetEnemyMove(move);
    }

    public void CopyOtherEnemyController(EnemyController ec)
    {
        SetEnemyMove(ec.GetEnemyMove());
        EnemyMove em = ec.gameObject.GetComponent<EnemyMove>();
#if UNITY_EDITOR
        EditorApplication.delayCall += () => GetComponent<EnemyMove>().CopyOtherEnemyMove(em);
#else
        GetComponent<EnemyMove>().CopyOtherEnemyMove(em);
#endif
    }
}

public enum ENEMY_MOVE
{
	PATROL,
    CHASE,
	WAIT
}
