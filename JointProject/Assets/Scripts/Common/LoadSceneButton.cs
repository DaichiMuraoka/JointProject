//kanoko
//指定のシーンを読み込むだけのボタン

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string SceneName = null;
    [SerializeField] private float waitTime = 0.2f;
    
    public void OnClick()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        
        StartCoroutine(NextScene());
    }
    
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneName);
    }
}
