using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private void Update()
    {
        if(GetComponent<Controller>().State == MOVE_STATE.FREEZE)
        {
            return;
        }
        MovePerFrame();
    }

    public virtual void MovePerFrame() { }
}
