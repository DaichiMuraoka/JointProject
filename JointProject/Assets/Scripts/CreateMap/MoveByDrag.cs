//kanoko

//指定タグのオブジェクトを
//マウスドラッグで移動させる．
//XZ平面のみで移動可能．

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByDrag : MonoBehaviour
{
    //ドラッグで移動させたいオブジェクトのタグ
    [SerializeField] private string objectTag = "MapParts";
    //グリッドにスナップさせるか
    [SerializeField] private bool snapGrid = false;
    //仮想グリッドのセルサイズ
    //[SerializeField] private int cellSize = 1;
    
    //オブジェクトをつかんでいるか
    private bool isGrabbing;
    //ヒットしたオブジェクトのトランスフォーム
    private Transform obj;
    //ヒットしたオブジェクトの中央座標とマウス座標の差
    private Vector3 hitpos;

    // Update is called once per frame
    void Update()
    {
        //左クリック
        if(Input.GetMouseButtonDown(0))
        {
            //レイを飛ばし，ヒットしたオブジェクトのタグが
            //objectTagならオブジェクトを掴む
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == objectTag)
                {
                    isGrabbing = true;
                    obj = hit.transform;
                    hitpos.x = obj.position.x-hit.point.x;
                    hitpos.y = 0f;
                    hitpos.z = obj.position.z-hit.point.z;
                }
            }
        }
 
 
        if (isGrabbing)
        {
            Vector3 mousepos = Input.mousePosition;
            mousepos.z = 20f;
            //マウス位置をワールド座標に変換
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousepos);
            //yはもとの値のまま
            worldPosition.y = obj.position.y;
            //xとzは掴んだ位置との差を加える
            worldPosition += hitpos;
            
            //snapGridがオンのとき座標を丸め込む
            if(snapGrid){
                worldPosition.x = (float)Mathf.RoundToInt(worldPosition.x);
                worldPosition.z = (float)Mathf.RoundToInt(worldPosition.z);
            }
            
            //オブジェクトの位置を変更
            obj.position = worldPosition;
            
            if (Input.GetMouseButtonUp(0))
            {
                isGrabbing = false;
            }
        }
    }
}
