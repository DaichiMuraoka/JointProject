//kanoko
//スクリプタブルオブジェクトからレベルを読み出しシーンに配置する。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevel : MonoBehaviour
{
    [SerializeField]
    private MapDeliverer mapDeliverer = null;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(mapDeliverer.Map);
    }
}
