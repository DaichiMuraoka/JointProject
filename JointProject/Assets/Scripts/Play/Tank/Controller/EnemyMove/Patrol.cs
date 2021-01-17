using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyMove
{
    [SerializeField]
    private List<Transform> route = new List<Transform>();

    public override void MovePerFrame()
    {

    }
}
