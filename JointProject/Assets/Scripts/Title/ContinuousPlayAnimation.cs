//kanoko
//インスペクターで指定したアニメーションを連続再生する。
//AnimationコンポーネントのAnimationsにも指定してないと動かない。

using UnityEngine;
using System.Collections;

public class ContinuousPlayAnimation : MonoBehaviour {

    [SerializeField]
    private AnimationClip[] clips = null;

    private Animation anim;
    

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animation>();
        anim.playAutomatically = false;
        
        foreach (AnimationClip clip in clips) {
            anim.PlayQueued(clip.name, QueueMode.CompleteOthers);
        }
        
    }
}