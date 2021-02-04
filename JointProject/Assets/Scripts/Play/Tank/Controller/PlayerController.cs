using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField]
    private KeyCode nomalFireKey = KeyCode.E;

    [SerializeField]
    private KeyCode flyFireKey = KeyCode.F;

    private void Start()
    {
        State = MOVE_STATE.MOVE;
    }

    private void Update()
    {
        if (State == MOVE_STATE.FREEZE)
        {
            return;
        }
        //移動
        float x = Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * z, Space.World);
        transform.Rotate(new Vector3(0, 1, 0), x);
        //発射
        if (Input.GetKeyDown(nomalFireKey))
        {
            GetComponent<FireManager>().Fire(FIRE_TYPE.NOMAL);
        }
        if (Input.GetKeyDown(flyFireKey))
        {
            GetComponent<FireManager>().Fire(FIRE_TYPE.FLY);
        }
    }
}
