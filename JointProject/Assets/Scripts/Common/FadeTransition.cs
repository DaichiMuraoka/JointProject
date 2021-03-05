//kanoko
//フェイドアウトして次のシーンに移行する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    [SerializeField] private bool FadeIn = true;
    [SerializeField] private string nextSceneName = null;
    [SerializeField] private float speed = 1.0F;
    
    private Animation anim;
    private bool nextSceneFlag = false;
    
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        image = gameObject.GetComponent<Image>();
        //アニメーションのスピードを設定
        anim[anim.clip.name].speed = speed;
        
        if(FadeIn){
            anim.Play(); //フェードイン
        }
        else
        {
            HideImage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(nextSceneFlag && anim.IsPlaying(anim.clip.name) == false){
            SceneManager.LoadScene(nextSceneName);
        }
    }
    
    //フェードアウトをスタート
    public void StartFadeOut()
    {
        anim.Stop();
        //フェードインを逆再生
        anim[anim.clip.name].speed *= -1.0F;
        anim[anim.clip.name].time = anim.clip.length;
        anim.Play();
        
        nextSceneFlag = true;
    }
    
    //imageを切り替え
    public void HideImage()
    {
        if(image.enabled){
            image.enabled = false;
        }else{
            image.enabled = true;
        }
    }
}
