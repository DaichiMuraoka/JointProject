using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MergeMap : MonoBehaviour
{
    public void OnClick()
    {
        //不要な壁を削除してマップを繋げる
        
        //方向と座標変移 8方向
        Vector3[] coordinate = new Vector3[8];
        coordinate[0] = new Vector3(0, 5, 1);
        coordinate[1] = new Vector3(1, 5, 1);
        coordinate[2] = new Vector3(1, 5, 0);
        coordinate[3] = new Vector3(1, 5, -1);
        coordinate[4] = new Vector3(0, 5, -1);
        coordinate[5] = new Vector3(-1, 5, -1);
        coordinate[6] = new Vector3(-1, 5, 0);
        coordinate[7] = new Vector3(-1, 5, 1);
        
        //全ての壁ブロックを取得
        GameObject[] walls = GameObject.FindGameObjectsWithTag ("Map_wall");
        
        //全ての壁ブロックについて
        foreach (GameObject wall in walls)
        {   
            int blockSum = 0;
            //上下左右について
            foreach (Vector3 coor in coordinate)
            {
                //MapParts内にヒットするか
                RaycastHit hit;
                if(Physics.Raycast(coor+wall.transform.position, 
                    new Vector3(0, -5, 0), out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "MapParts")
                    { 
                        blockSum++;
                    }
                }
            }
            
            //接面が8未満＝端のブロック
            //端のブロックでないなら消す
            if(blockSum == 8){
                Destroy(wall);
            }
        }
        
        //マップパーツのコライダーを外す
        GameObject[] mapParts = GameObject.FindGameObjectsWithTag ("MapParts");
        foreach (GameObject part in mapParts)
        {
            Destroy(part.GetComponent<BoxCollider>());
        }


        //マップを残す
        DontDestroyOnLoad(mapParts[0].transform.parent.gameObject);

        //シーン移動
        SceneManager.LoadScene("Play");
    }
}
