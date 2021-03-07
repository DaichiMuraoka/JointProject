using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRigidbody : MonoBehaviour
{
    private GameObject[] players;
    private GameObject[] enemys;
    
    // Start is called before the first frame update
    void Start()
    {
        //全てのプレイヤー・敵を取得
        players = GameObject.FindGameObjectsWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        
        SwitchRigidbodyEnabled(false);
    }

    public void OnClick()
    {
        SwitchRigidbodyEnabled(true);
    }
    
    //RigidbodyのEnabledを切り替え
    private void SwitchRigidbodyEnabled(bool enable)
    {
        foreach (GameObject obj in players)
        {
            obj.GetComponent<CapsuleCollider>().enabled = enable;
            obj.GetComponent<Rigidbody>().useGravity = enable;
        }
        foreach (GameObject obj in enemys)
        {
            obj.GetComponent<CapsuleCollider>().enabled = enable;
            obj.GetComponent<Rigidbody>().useGravity = enable;
        }
    }
}
