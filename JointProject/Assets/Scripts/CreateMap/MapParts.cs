using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParts : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other) {
		//他のマップパーツと重なったら
        //全体を赤く表示する
        if(other.gameObject.tag == "MapParts"){
            changeAllMaterialsColor(this.gameObject, Color.red);
        }
	}
    
	void OnTriggerExit(Collider other) {
		//他のマップパーツと離れたら
        //元通り表示する(白を想定)
        if(other.gameObject.tag == "MapParts"){
            changeAllMaterialsColor(this.gameObject, Color.white);
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

