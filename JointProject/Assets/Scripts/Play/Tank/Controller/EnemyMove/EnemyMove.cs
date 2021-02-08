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

    public virtual void CopyOtherEnemyMove(EnemyMove em) { }

    public bool Rotate(Vector3 destinationPos)
    {
        Vector3 dir = (destinationPos - transform.position).normalized;
        Vector3 axis = Vector3.Cross(transform.forward, dir);
        Vector3 dif = dir - transform.forward;
        if (Mathf.Abs(dif.x) <= 0.1f && Mathf.Abs(dif.z) <= 0.1f)
        {
            return true;
        }
        float x = GetComponent<Controller>().RotationSpeed * Time.deltaTime * (axis.y < 0 ? -1 : 1);
        transform.Rotate(new Vector3(0, 1, 0), x);
        return false;
    }

    public bool GoStraight(Vector3 destinationPos)
    {
        if (Vector3.Distance(transform.position, destinationPos) < 0.1f)
        {
            return true;
        }
        transform.LookAt(new Vector3(destinationPos.x, transform.position.y, destinationPos.z));
        float z = GetComponent<Controller>().MoveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * z, Space.World);
        return false;
    }
}
