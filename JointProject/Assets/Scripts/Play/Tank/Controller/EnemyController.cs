using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : Controller
{
    [SerializeField]
    private ENEMY_MOVE move = ENEMY_MOVE.PATROL;
    
    public ENEMY_MOVE Move
    {
        get { return move; }
        set
        {
            move = value;
            EnemyMove em = GetComponent<EnemyMove>();
            if (value == ENEMY_MOVE.PATROL)
            {
                if (!(em is Patrol))
                {
                    if (em != null)
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
            else if (value == ENEMY_MOVE.CHASE)
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
            else if(value == ENEMY_MOVE.WAIT)
            {
                if (!(em is Wait))
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
            else
            {
                if (em != null)
                {
#if UNITY_EDITOR
                    EditorApplication.delayCall += () => DestroyImmediate(em, true);
#else
                    Destroy(em);
#endif
                }
            }
        }
    }

    [SerializeField]
    private ENEMY_TARGET target = ENEMY_TARGET.NEAREST;

    public ENEMY_TARGET Target
    {
        get { return target; }
        set
        {
            target = value;
            EnemyAttack ea = GetComponent<EnemyAttack>();
            if (value == ENEMY_TARGET.NEAREST)
            {
                if (!(ea is AttackNearest))
                {
                    if (ea != null)
                    {
#if UNITY_EDITOR
                        EditorApplication.delayCall += () => DestroyImmediate(ea);
#else
                        Destroy(ea);
#endif
                    }
                    gameObject.AddComponent<AttackNearest>();
                }
            }
            else if (value == ENEMY_TARGET.RANDOM)
            {
                if (!(ea is AttackRandom))
                {
                    if (ea != null)
                    {
#if UNITY_EDITOR
                        EditorApplication.delayCall += () => DestroyImmediate(ea);
#else
                        Destroy(ea);
#endif
                    }
                    gameObject.AddComponent<AttackRandom>();
                }
            }
            else
            {
                if (ea != null)
                {
#if UNITY_EDITOR
                    EditorApplication.delayCall += () => {
                        DestroyImmediate(ea, true);
                        EditorApplication.delayCall += () =>
                        {
                            EnemyAttack ea2 = GetComponent<EnemyAttack>();
                            if (ea2 != null)
                            {
                                Target = ENEMY_TARGET.NONE;
                            }
                        };
                    };
#else
                    Destroy(ea);
#endif
                }
            }
        }
    }
    
    private void OnValidate()
    {
        Move = move;
        Target = target;
    }
}

public enum ENEMY_MOVE
{
    NONE,
	PATROL,
    CHASE,
	WAIT
}

public enum ENEMY_TARGET
{
    NONE,
    NEAREST,
    RANDOM
}
