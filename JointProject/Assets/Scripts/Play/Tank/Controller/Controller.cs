﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private MOVE_STATE state = MOVE_STATE.FREEZE;

    public MOVE_STATE State
    {
        get { return state; }
        set
        {
            StartCoroutine(SetAnimationParam(value));
            state = value;
        }
    }

    private IEnumerator SetAnimationParam(MOVE_STATE moveState)
    {
        if(moveState == MOVE_STATE.MOVE)
        {
            animator.SetBool("moving", true);
        }
        else if(moveState == MOVE_STATE.NOMAL_ATTACK)
        {
            animator.SetBool("fire_fwd", true);
            yield return null;
            animator.SetBool("fire_fwd", false);
        }
        else if(moveState == MOVE_STATE.FLY_ATTACK)
        {
            animator.SetBool("fire_uwd", true);
            yield return null;
            animator.SetBool("fire_uwd", false);
        }
        else if(moveState == MOVE_STATE.FREEZE)
        {
            animator.SetBool("moving", false);
        }
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

    private Animator animator = null;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("animator is none");
        }
        else
        {
            Debug.Log("set anomator");
        }
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
    FREEZE,
    NOMAL_ATTACK,
    FLY_ATTACK
}
