//kanoko
//子オブジェクトのアニメーションをn秒ずらしで再生する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChildAnimation : MonoBehaviour
{
    [SerializeField] private bool playAutomatically = false;
    [SerializeField] private float animationSpeed = 1.0f;
    [SerializeField] private float animationSpacing = 0.2f;
    
    private GameObject[] childlen;
    
    // Start is called before the first frame update
    void Start()
    {
        childlen = new GameObject[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childlen[i] = gameObject.transform.GetChild(i).gameObject;
        }
        
        //textやimageの表示をオフにする
        foreach (GameObject child in childlen)
        {
            ChengeEnabledImageTMP(child, false);
        }
        
        if(playAutomatically){
            foreach (GameObject child in childlen)
            {
                StartCoroutine(PlayChildAnimation(child));
            }
        }
    }
    
    public void PlayAllAnimation(){
        foreach (GameObject child in childlen)
        {
            StartCoroutine(PlayChildAnimation(child));
        }
    }
    
    private void ChengeEnabledImageTMP(GameObject obj, bool enable)
    {
        Image image = obj.GetComponent<Image>();
        if(image != null){
            image.enabled = enable;
        }
        TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();
        if(text != null){
            text.enabled = enable;
        }
    }

    private IEnumerator PlayChildAnimation(GameObject child)
    {
        Animation anim = child.GetComponent<Animation>();
        if(anim != null){
            ChengeEnabledImageTMP(child, true);
            anim[anim.clip.name].speed = animationSpeed;
            anim.Play();
            yield return new WaitForSeconds(animationSpacing);
        }

    }
}
