using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSE : MonoBehaviour
{   
    [SerializeField] private float waitTime = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySE());
    }
    
    private IEnumerator PlaySE()
    {
        yield return new WaitForSeconds(waitTime);
        
        //bgmが既にあるならストップ
        GameObject bgm = GameObject.Find("BGM");
        if(bgm != null){
            bgm.GetComponent<AudioSource>().Stop();
        }
        
        //seを再生
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

}
