//kanoko
//指定のシーンを読み込むだけのボタン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string SceneName = null;
    
    public void OnClick()
    {
        //シーン移動
        SceneManager.LoadScene(SceneName);
    }
}
