using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StartPosition : MonoBehaviour
{
    [SerializeField]
    private SIDE side = SIDE.NONE;

    private void Start()
    {
        TankGenerator.Instance.Generate(side, this);
    }

    private void OnValidate()
    {
        if(side == SIDE.ENEMY)
        {
            if(GetComponent<EnemyController>() == null)
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
            if(enemyMove != null)
            {
#if UNITY_EDITOR
                EditorApplication.delayCall += () => DestroyImmediate(enemyMove);
#else
                Destroy(enemyMove);
#endif
            }
        }
    }

}
