//kanoko
//インスペクターで指定されたレベル分のボタンを生成する

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class LevelBottunGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] levels = null;
    [SerializeField] private GameObject content = null;
    [SerializeField] private GameObject levelBottun = null;
    [SerializeField] private MapDeliverer mapDeliverer = null;
    
    void Start(){
        
        for(int i = 0; i < levels.Length; i++) {
            //プレハブからボタンを生成
            GameObject listButton = Instantiate(levelBottun) as GameObject;
            
            //content の子にする
            listButton.transform.SetParent(content.transform, false);
            //ボタンの表示文字を設定
            listButton.transform.GetChild(0).GetComponent<Text>().text = "レベル" + (i+1).ToString();

            int n = i;
            //クリック時の関数を設定
            listButton.GetComponent<Button>().onClick.AddListener(() => OnClickLevelBottun(n));
        }
        
    }

    void OnClickLevelBottun(int index){
        //デリバラーにレベルをセット
        mapDeliverer.Map = levels[index];
        //シーン遷移
    }
}
