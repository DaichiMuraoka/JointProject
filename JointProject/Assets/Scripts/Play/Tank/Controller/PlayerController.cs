using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private List<string> horizontalKey = new List<string>();
    private string horizontalMainKey = "Horizontal2";
    private string horizontalSubKey = "Horizontal";
    private float GetHorizontalAxis()
    {
        if(horizontalKey.Count == 2)
        {
            if(Mathf.Abs(Input.GetAxis(horizontalKey[0])) > Mathf.Abs(Input.GetAxis(horizontalKey[1])))
            {
                return Input.GetAxis(horizontalKey[0]);
            }
            else
            {
                return Input.GetAxis(horizontalKey[1]);
            }
        }
        else if(horizontalKey.Count == 1)
        {
            return Input.GetAxis(horizontalKey[0]);
        }
        else
        {
            Debug.LogError("horizontalkey is none");
            return 0f;
        }
    }

    private List<string> verticalKey = new List<string>();
    private string verticalMainKey = "Vertical2";
    private string verticalSubKey = "Vertical";
    private float GetVerticalAxis()
    {
        if (verticalKey.Count == 2)
        {
            if (Mathf.Abs(Input.GetAxis(verticalKey[0])) > Mathf.Abs(Input.GetAxis(verticalKey[1])))
            {
                return Input.GetAxis(verticalKey[0]);
            }
            else
            {
                return Input.GetAxis(verticalKey[1]);
            }
        }
        else
        {
            return Input.GetAxis(verticalKey[0]);
        }
    }

    private List<KeyCode> nomalFireKey = new List<KeyCode>();
    private KeyCode nomalFireMainKey = KeyCode.E;
    private KeyCode nomalFireSubKey = KeyCode.Return;

    private List<KeyCode> flyFireKey = new List<KeyCode>();
    private KeyCode flyFireMainKey = KeyCode.F;
    private KeyCode flyFireSubKey = KeyCode.Backspace;

    private int id = 0;

    public int ID
    {
        set
        {
            id = value;
            if(ModeSettingLoader.Instance.ModeSetting.PlayMode == PLAY_MODE.LOCAL)
            {
                if(ModeSettingLoader.Instance.ModeSetting.PlayerCount == 1)
                {
                    horizontalKey.Add(horizontalMainKey);
                    horizontalKey.Add(horizontalSubKey);
                    verticalKey.Add(verticalMainKey);
                    verticalKey.Add(verticalSubKey);
                    nomalFireKey.Add(nomalFireMainKey);
                    nomalFireKey.Add(nomalFireSubKey);
                    flyFireKey.Add(flyFireMainKey);
                    flyFireKey.Add(flyFireSubKey);
                }
                else
                {
                    if (id == 1)
                    {
                        horizontalKey.Add(horizontalMainKey);
                        verticalKey.Add(verticalMainKey);
                        nomalFireKey.Add(nomalFireMainKey);
                        flyFireKey.Add(flyFireMainKey);
                    }
                    else if (id == 2)
                    {
                        horizontalKey.Add(horizontalSubKey);
                        verticalKey.Add(verticalSubKey);
                        nomalFireKey.Add(nomalFireSubKey);
                        flyFireKey.Add(flyFireSubKey);
                    }
                }
            }
            else
            {
                //オンラインでの動作
            }
        }
    }

    public override void Start()
    {
        base.Start();
        PlayerTankAdjuster.Instance.Adjust(this);
    }

    private void Update()
    {
        if (State == MOVE_STATE.FREEZE)
        {
            return;
        }
        //移動
        float x = GetHorizontalAxis() * RotationSpeed * Time.deltaTime;
        float z = GetVerticalAxis() * MoveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * z, Space.World);
        transform.Rotate(new Vector3(0, 1, 0), x);
        //発射
        foreach(KeyCode keyCode in nomalFireKey)
        {
            if (Input.GetKeyDown(keyCode))
            {
                GetComponent<FireManager>().Fire(FIRE_TYPE.NOMAL);
            }
        }
        foreach (KeyCode keyCode in flyFireKey)
        {
            if (Input.GetKeyDown(keyCode))
            {
                GetComponent<FireManager>().Fire(FIRE_TYPE.FLY);
            }
        }
    }
}
