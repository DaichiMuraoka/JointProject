using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector]
    public EnemyMove enemyMove = null;

    [HideInInspector]
    public FireManager fireManager = null;

    [HideInInspector]
    public Controller controller = null;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        if(enemyMove == null)
        {
            Debug.LogError("EnemyMove is null.");
        }
        fireManager = GetComponent<FireManager>();
        if (fireManager == null)
        {
            Debug.LogError("FireManager is null.");
        }
        controller = GetComponent<Controller>();
        if (controller == null)
        {
            Debug.LogError("Controller is null.");
        }
    }

    private void Update()
    {
        if (GetComponent<Controller>().State == MOVE_STATE.FREEZE)
        {
            return;
        }
        AttackPerFrame();
    }

    public virtual void AttackPerFrame() { }
}
