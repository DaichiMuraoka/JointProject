using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void Update()
    {
        if (GetComponent<Controller>().State == MOVE_STATE.FREEZE)
        {
            return;
        }
        AttackPerFrame();
    }

    public virtual void AttackPerFrame() { }

    public virtual void CopyOtherEnemyAttack(EnemyAttack ea) { }
}
