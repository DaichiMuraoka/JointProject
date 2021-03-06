//kanoko
//チュートリアル用のマップをセット

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialLevel = null;
    [SerializeField] private MapDeliverer mapDeliverer = null;
    private GameObject bgm;
    
    public void OnClick()
    {
        Destroy(GameObject.Find("BGM"));
        mapDeliverer.Map = tutorialLevel;
        mapDeliverer.Level = 0;
    }
}
