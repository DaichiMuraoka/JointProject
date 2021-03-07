//kanoko
//mapDelivererからレベルを読み出しシーンに配置する。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevel : MonoBehaviour
{
    [SerializeField] private MapDeliverer mapDeliverer = null;
    [SerializeField] private GameObject levelNumber = null;
    
    //Startより前に実行される
    void Awake()
    {
        Instantiate(mapDeliverer.Map);
        levelNumber.GetComponent<TextMeshProUGUI>().text = "レベル" + (mapDeliverer.Level).ToString();
    }
}
