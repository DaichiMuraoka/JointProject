using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTutorialMode : MonoBehaviour
{
    [SerializeField] private GameObject modeText = null;
    [SerializeField] private ModeSetting modeSetting = null;
    [SerializeField] private float waitTime = 3.0f;
    
    void Awake()
    {
        if(modeSetting.PlayMode == PLAY_MODE.TUTORIAL){
            modeText.GetComponent<TextMeshProUGUI>().text = "チュートリアル";
            gameObject.GetComponent<MessageManager>().StartMessage(waitTime);
        }else{
            gameObject.SetActive(false);
        }
        
    }
}
