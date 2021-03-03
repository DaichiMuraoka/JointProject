using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionNextScene : MonoBehaviour
{
    [SerializeField] private AudioClip se = null;
    [SerializeField] private GameObject fadeCurtain = null;
    
    private AudioSource audioSource;
    private FadeTransition fadeTransition;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        fadeTransition = fadeCurtain.GetComponent<FadeTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            fadeTransition.StartFadeOut();
            audioSource.Stop();
            audioSource.PlayOneShot(se);
        }
    }
}
