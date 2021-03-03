//kanoko
//BGMオブジェクトが既にあれば自分を削除

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject bgm = GameObject.Find("BGM");
        if(bgm != gameObject){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }
}
