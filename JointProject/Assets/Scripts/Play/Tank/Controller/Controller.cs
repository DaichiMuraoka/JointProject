using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private MOVE_STATE state = MOVE_STATE.FREEZE;

    public MOVE_STATE State
    {
        get { return state; }
        set { state = value; }
    }

    [SerializeField]
    private float moveSpeed = 5f;

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    [SerializeField]
    private float rotationSpeed = 200f;

    public float RotationSpeed
    {
        get { return rotationSpeed; }
    }

    private Rigidbody rb = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Freeze(float second)
    {
        StartCoroutine(FreezeCoroutine(second));
    }

    private IEnumerator FreezeCoroutine(float second)
    {
        state = MOVE_STATE.FREEZE;
        yield return new WaitForSeconds(second);
        state = MOVE_STATE.MOVE;
    }
}

public enum MOVE_STATE
{
    MOVE,
    ATTACK,
    FREEZE
}
