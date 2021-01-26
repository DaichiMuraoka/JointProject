using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMap : MonoBehaviour
{
    enum Direction
    {
        Top = 0,
        Bottom = 1,
        Left = 2,
        Right = 3
    }
    
    public void OnClick()
    {
        //不要な壁を削除してマップを繋げる
        
        //マップパーツのコライダーをオフにする
        GameObject[] parts = GameObject.FindGameObjectsWithTag ("MapParts");
        foreach (GameObject p in parts)
        {
            BoxCollider coll = p.GetComponent<BoxCollider>(); 
            coll.enabled = false;
        }
        
        //方向と座標変移
        Vector3[] coordinate = new Vector3[4];
        coordinate[(int)Direction.Top] = new Vector3(0, 5, 1);
        coordinate[(int)Direction.Bottom] = new Vector3(0, 5, -1);
        coordinate[(int)Direction.Left] = new Vector3(-1, 5, 0);
        coordinate[(int)Direction.Right] = new Vector3(1, 5, 0);
        
        //全ての壁ブロックを取得
        GameObject[] walls = GameObject.FindGameObjectsWithTag ("Map_wall");
        //削除するブロックのリスト
        List<GameObject> destroyBlocks = new List<GameObject>();
        
        //全ての壁ブロックについて
        foreach (GameObject wall in walls)
        {
            //注目ブロックの周囲のブロックリスト
            GameObject[] surroundingBlocks = new GameObject[4];
            int blockSum = 0;
            
            //上下左右について
            foreach (Direction d in Enum.GetValues(typeof(Direction)))
            {
                //壁ブロックがあれば保持する
                RaycastHit hit;
                if(Physics.Raycast(coordinate[(int)d]+wall.transform.position, 
                    new Vector3(0, -5, 0), out hit, Mathf.Infinity))
                {
                    GameObject block = hit.collider.gameObject.transform.parent.gameObject;
                    if (block.tag == "Map_wall")
                    {
                        surroundingBlocks[(int)d] = block; 
                        blockSum++;
                    }
                }
            }
            
            //接面ブロックが3以上のとき
            if(blockSum > 2)
            {
                foreach (Direction d in Enum.GetValues(typeof(Direction)))
                {
                    //ブロックがない方向の逆方向のブロックを削除リストに追加
                    if(surroundingBlocks[(int)d] == null)
                    {
                        //逆方向
                        Direction opposite = d;
                        switch (d)
                        {
                            case Direction.Top:
                                opposite = Direction.Bottom;
                                break;
                            case Direction.Bottom:
                                opposite = Direction.Top;
                                break;
                            case Direction.Right:
                                opposite = Direction.Left;
                                break;
                            case Direction.Left:
                                opposite = Direction.Right;
                                break;
                            default:
                                break;
                        }
                        
                        //逆方向の一個先にまだ壁ブロックがある場合は削除しない
                        RaycastHit hit;
                        if(Physics.Raycast(coordinate[(int)opposite]+surroundingBlocks[(int)opposite].transform.position, 
                            new Vector3(0, -5, 0), out hit, Mathf.Infinity))
                        {
                            GameObject block = hit.collider.gameObject.transform.parent.gameObject;
                            if (block.tag != "Map_wall")
                            {
                                destroyBlocks.Add(surroundingBlocks[(int)opposite]);
                            }
                        }else{
                            destroyBlocks.Add(surroundingBlocks[(int)opposite]);
                        }
                        break;
                    }
                }
            }
        }
        
        IEnumerable<GameObject> destroyBlocks_notDuplicate = destroyBlocks.Distinct();
        
        //削除リストのブロックを削除
        foreach(GameObject wall in destroyBlocks_notDuplicate)
        {
            Destroy(wall);
        }
    }
}
