//kanoko
//子に登録されたメッセージの表示をクリックで切り替えていく

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private bool startAutomatically = true;
    [SerializeField] private float waitTime = 0.0f;
    [SerializeField] private GameObject[] invailidObjects = null;
    //メッセージ表示中無効にしたいオブジェクト
    
    private GameObject[] messages;
    private int index = 0;
    private bool input = false; //入力を受け付けるか
    private AudioSource audioSource;
    
    void Start()
    {
        //オブジェクトを取得
        messages = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            messages[i] = gameObject.transform.GetChild(i).gameObject;
        }
        
        audioSource = gameObject.GetComponent<AudioSource>();
        
        //全てのメッセージを非表示にする
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i].SetActive(false);
        }
        
        if(startAutomatically)
        {
            StartMessage(waitTime);
        }
        
    }
    
    
    public void StartMessage(float time)
    {
        StartCoroutine(InitSetting(time));
    }
    
    //初期設定
    public IEnumerator InitSetting(float time)
    {
        yield return new WaitForSeconds(time);
        
        foreach(GameObject obj in invailidObjects)
        {
            obj.SetActive(false);
        }
        messages[0].SetActive(true);
        input = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(input && Input.GetMouseButtonDown(0))
        {
            //次のメッセージを表示する
            audioSource.PlayOneShot(audioSource.clip);
            messages[index].SetActive(false);
            index++;
            //全てのメッセージを表示し終わったら
            if(index >= messages.Length)
            {
                input = false;
                foreach(GameObject obj in invailidObjects)
                {
                    obj.SetActive(true);
                }
                gameObject.SetActive(false);
            }else{
                messages[index].SetActive(true);
            }
        }
    }
    
    
}
