//kanoko
//スクリプタブルオブジェクトからレベルを読み出しシーンに配置する。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevel : MonoBehaviour
{
    [SerializeField]
    private MapDeliverer mapDeliverer = null;
    
    //Startより前に実行される
    void Awake()
    {
        Instantiate(mapDeliverer.Map);
    }
}
