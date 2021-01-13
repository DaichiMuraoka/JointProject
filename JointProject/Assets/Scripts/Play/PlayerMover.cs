using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
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

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * z, Space.World);
        transform.Rotate(new Vector3(0, 1, 0), x);
        Debug.Log(transform.forward);
    }
}
