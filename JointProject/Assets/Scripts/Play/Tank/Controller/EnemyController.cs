﻿using System.Collections;
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
            else
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
        }
    }

    [SerializeField]
    private ENEMY_TARGET target = ENEMY_TARGET.NEAREST;

    public ENEMY_TARGET Target
    {
        get { return target; }

    }

    private void OnValidate()
    {
        Move = move;
    }

    public void CopyOtherEnemyController(EnemyController ec)
    {
        Move = ec.Move;
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

public enum ENEMY_TARGET
{
    NEAREST,
    RANDOM
}