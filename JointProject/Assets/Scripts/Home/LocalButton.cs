using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalButton : MonoBehaviour
{
    public void OnClick()
    {
        //シーン移動
        SceneManager.LoadScene("Local");
    }
}
