using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyMove
{
    [SerializeField]
    private List<Transform> route = new List<Transform>();

    public List<Transform> Route
    {
        get { return route; }
    }

    private int currentDestination = 0;

    public override void MovePerFrame()
    {
        Vector3 dPos = route[currentDestination].position;
        Vector3 destinationPos = new Vector3(dPos.x, transform.position.y, dPos.z);
        if (Rotate(destinationPos))
        {
            if (GoStraight(destinationPos))
            {
                currentDestination++;
                if(currentDestination == route.Count)
                {
                    currentDestination = 0;
                }
                Debug.Log(currentDestination);
            }
        }
    }
}
