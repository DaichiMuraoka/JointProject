//kanoko
//作成完了エフェクトとSEの再生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteEffect : MonoBehaviour
{
    [SerializeField] private float waitTime = 3.5f;
    [SerializeField] private AudioClip se = null;
    [SerializeField] private GameObject completeText = null;
    [SerializeField] private GameObject fadeCurtain = null;
    
    public void OnClick()
    {
        ChildAnimController cac = completeText.GetComponent<ChildAnimController>();
        cac.PlayAllAnimation();
        
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(se);
        
        StartCoroutine(NextScene());
    }
    
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(waitTime);
        FadeTransition fadeTransition = fadeCurtain.GetComponent<FadeTransition>();
        fadeTransition.StartFadeOut();
    }
}
