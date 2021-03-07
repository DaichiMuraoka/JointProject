//kanoko
//mapDelivererにあるレベル分のボタンを生成する level0はチュートリアル

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject content = null;
    [SerializeField] private GameObject levelButton = null;
    [SerializeField] private GameObject clearedSign = null;
    [SerializeField] private MapDeliverer mapDeliverer = null;
    [SerializeField] private GameObject fadeCurtain = null;
    
    private AudioSource audioSource;
    private FadeTransition fadeTransition;
    
    void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
        fadeTransition = fadeCurtain.GetComponent<FadeTransition>();
        //セーブデータ読み出し
        Progress savedata = SaveDataManager.Instance.LoadProgress();
        
        for(int i = 1; i < mapDeliverer.LevelMax; i++) {
            //プレハブからボタンを生成
            GameObject listButton = Instantiate(levelButton) as GameObject;
            //content の子にする
            listButton.transform.SetParent(content.transform, false);
            //ボタンの表示文字を設定
            listButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "レベル" + (i).ToString();
            
            //クリア済なら印をつける
            foreach (ProgressData data in savedata.list)
            {
                if(data.level == i && data.clear)
                {
                    GameObject sign = Instantiate(clearedSign) as GameObject;
                    //listButton の子にする
                    sign.transform.SetParent(listButton.transform, false);
                    break;
                }
            }

            int n = i;
            //クリック時の関数を設定
            listButton.GetComponent<Button>().onClick.AddListener(() => OnClickLevelButton(n));
        }
        
    }

    void OnClickLevelButton(int index){
        //デリバラーにレベルをセット
        mapDeliverer.Level = index;
        //フェード開始
        fadeTransition.StartFadeOut();
        //BGMの削除
        GameObject bgm = GameObject.Find("BGM");
        Destroy(bgm);
        
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
