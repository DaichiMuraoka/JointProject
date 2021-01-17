using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : Controller
{
    [SerializeField]
    private ENEMY_MOVE move = ENEMY_MOVE.PATROL;

    private void Start()
    {

    }

    private void SetEnemyMove()
    {
        EnemyMove em = GetComponent<EnemyMove>();
        if (move == ENEMY_MOVE.PATROL)
        {
            if(em is Patrol)
            {
                
            }
            else
            {
                if(em != null)
                {
                    EditorApplication.delayCall += () => DestroyImmediate(em);
                }
                gameObject.AddComponent<Patrol>();
            }
        }
        else if(move == ENEMY_MOVE.CHASE)
        {
            if (em is Chase)
            {
                
            }
            else
            {
                if (em != null)
                {
                    EditorApplication.delayCall += () => DestroyImmediate(em);
                }
                gameObject.AddComponent<Chase>();
            }
        }
        else
        {
            if(em is Wait)
            {

            }
            else
            {
                if (em != null)
                {
                    EditorApplication.delayCall += () => DestroyImmediate(em);
                }
                gameObject.AddComponent<Wait>();
            }
        }
    }

    private void OnValidate()
    {
        SetEnemyMove();
    }
}

public enum ENEMY_MOVE
{
	PATROL,
    CHASE,
	WAIT
}
