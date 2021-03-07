//kanoko
//チュートリアル用のマップをセット

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorial : MonoBehaviour
{
    [SerializeField] private MapDeliverer mapDeliverer = null;
    
    public void OnClick()
    {
        Destroy(GameObject.Find("BGM"));
        mapDeliverer.Level = 0;
    }
}
