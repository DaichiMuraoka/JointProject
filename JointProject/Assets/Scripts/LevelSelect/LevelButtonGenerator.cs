//kanoko
//インスペクターで指定されたレベル分のボタンを生成する

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
    [SerializeField] private GameObject[] levels = null;
    [SerializeField] private GameObject content = null;
    [SerializeField] private GameObject levelButton = null;
    [SerializeField] private MapDeliverer mapDeliverer = null;
    [SerializeField] private GameObject fadeCurtain = null;
    
    private AudioSource audioSource;
    private FadeTransition fadeTransition;
    
    void Start(){
        audioSource = gameObject.GetComponent<AudioSource>();
        fadeTransition = fadeCurtain.GetComponent<FadeTransition>();
        
        for(int i = 0; i < levels.Length; i++) {
            //プレハブからボタンを生成
            GameObject listButton = Instantiate(levelButton) as GameObject;
            
            //content の子にする
            listButton.transform.SetParent(content.transform, false);
            //ボタンの表示文字を設定
            listButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "レベル" + (i+1).ToString();

            int n = i;
            //クリック時の関数を設定
            listButton.GetComponent<Button>().onClick.AddListener(() => OnClickLevelButton(n));
        }
        
    }

    void OnClickLevelButton(int index){
        //デリバラーにレベルをセット
        mapDeliverer.Map = levels[index];
        //フェード開始
        fadeTransition.StartFadeOut();
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
