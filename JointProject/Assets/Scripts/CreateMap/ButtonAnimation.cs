//kanoko
//ボタンが有効な時だけアニメーションを再生する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    private Button button;
    private Animation anim;
    
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(button.interactable && anim.isPlaying == false){
            anim.Play();
        }else if(button.interactable == false && anim.isPlaying){
            anim.Stop();
        }
    }
}
