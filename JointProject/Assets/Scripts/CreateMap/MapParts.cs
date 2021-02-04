using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マップパートのサイズに合ったBoxColliderが必須
[RequireComponent(typeof(BoxCollider))]
public class MapParts : MonoBehaviour
{   
    private BoxCollider boxCollider;
    private float width; //マップパーツの横幅(x)
    private float height; //マップパーツの縦幅(z)
    private bool isOverlapped = false; //マップが重なっているかどうか
    private Vector3 previousPosition; //前フレーム時のポジション
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        width = boxCollider.size.x;
        height = boxCollider.size.z;
        previousPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision other) {
		//他のマップパーツと隣接しているとき
        //全体を白く表示する
        if(other.gameObject.tag == "MapParts"){
            changeAllMaterialsColor(this.gameObject, Color.white);
        }
	}
    
	void OnCollisionStay(Collision other) {
        //移動時のみ判定
        if(previousPosition != this.gameObject.transform.position)
        {
            //他のマップパーツと重なっているとき
            //全体を赤く表示する
            if(other.gameObject.tag == "MapParts"){
                if(isOverlapped == false){
                    float separation = 0;
                    foreach (ContactPoint point in other.contacts)
                    {
                        separation = point.separation;
                    }
                    if(separation < 0)
                    {
                        changeAllMaterialsColor(this.gameObject, Color.red);
                        changeAllMaterialsColor(other.gameObject, Color.red);
                        isOverlapped = true;
                    }
                }else{
                    float separation = -1;
                    foreach (ContactPoint point in other.contacts)
                    {
                        separation = point.separation;
                    }
                    if(separation == 0)
                    {
                        changeAllMaterialsColor(this.gameObject, Color.white);
                        changeAllMaterialsColor(other.gameObject, Color.white);
                        isOverlapped = false;
                    }
                }
            }
        }
        //ポジションを更新
        previousPosition = this.gameObject.transform.position;
	}
    
	void OnCollisionExit(Collision other) {
		//他のマップパーツと離れたら
        //全体をグレーに表示する
        if(other.gameObject.tag == "MapParts"){
            changeAllMaterialsColor(this.gameObject, Color.gray);
            isOverlapped = false;
        }
	}
    
    /// <summary>
    /// targetGameObject以下の子オブジェクト群のマテリアルカラーを変更
    /// </summary>
    /// <param name="targetGameObject">対象GameObject。子要素も変更される。</param>
    /// <param name="color">変更後の色</param>
    public void changeAllMaterialsColor(GameObject targetGameObject, Color color)
    {
        foreach (Transform t in targetGameObject.GetComponentsInChildren<Transform>(true)) //include inactive gameobjects
        {
            if (t.GetComponent<Renderer>() != null)
            {
                var materials = t.GetComponent<Renderer>().materials;
                for (int i = 0; i< materials.Length; i++)
                {
                    Material material = materials[i];
                    material.SetColor("_Color", color);
                }
            }
        }
    }
}

