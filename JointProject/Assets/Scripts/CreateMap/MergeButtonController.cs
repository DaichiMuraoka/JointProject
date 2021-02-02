//kanoko
//マップ合成が可能な状態かどうかを判定し，ボタンの有効/無効を切り替える
//MapParts同士の距離とサイズから接地判定をし，色を変える

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject mergeButton;
    
    private Button mergeButtonComponent;
    private GameObject[] mapParts;
    private BoxCollider[] colliders;
    private Vector3[] previousPositions; //前回位置
    private bool[] mapPartsEnable; //合成可能な状態か
    
    private Color disableColor = Color.gray; //無効時の色
    private Color enableColor = Color.white; //有効時の色
    
    private bool buttonInteractable = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //全てのMapPartsを取得
        mapParts = GameObject.FindGameObjectsWithTag ("MapParts");
        
        colliders = new BoxCollider[mapParts.Length];
        previousPositions = new Vector3[mapParts.Length];
        mapPartsEnable = new bool[mapParts.Length];
        
        for(int i=0; i<mapParts.Length; i++){
            //ボックスコライダーを取得
            colliders[i] = mapParts[i].GetComponent<BoxCollider>();
            //初期位置を保持
            previousPositions[i] = mapParts[i].transform.position;
            //最初は全て有効状態
            mapPartsEnable[i] = true;
            //MapPartsを無効に変更
            changeMapPartsEnablement(i, false);
        }
        
        mergeButtonComponent = mergeButton.GetComponent<Button>();
        //ボタンを無効に設定
        changeButtonInteractable(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<mapParts.Length; i++){
            //マップパーツが移動したとき
            if(mapParts[i].transform.position != previousPositions[i]){
                //MapPartsの有効判定
                for(int j=0; j<mapParts.Length; j++)
                    judgeMapPartsAdjacent(j);
                    
                //前回位置を更新
                previousPositions[i] = mapParts[i].transform.position;
                
                //全てのマップパーツが有効のときボタンを有効にする
                int enableSum = 0;
                for(int j=0; j<mapParts.Length; j++)
                    if(mapPartsEnable[j]) enableSum++;
                
                if(enableSum == mapParts.Length)
                    changeButtonInteractable(true);
                else
                    changeButtonInteractable(false);
                
                break;
            }
        }
        
    }
    
    /// <summary>
    /// 指定のMapPartsが他のMapPartsと隣接していたら，隣接する2つのMapPartsを有効にする．
    /// 隣接しなければ無効にする．
    /// </summary>
    /// <param name="selected">MapPartsのインデックス</param>
    private void judgeMapPartsAdjacent(int selected)
    {
        int contactBlockNum = 3; //最低隣接しなければならないブロック数
        float errorValue = 0.5f; //許容される誤差
        
        Vector3 selectedTop = mapParts[selected].transform.position + colliders[selected].center
            + colliders[selected].size/2;
        Vector3 selectedBottom = mapParts[selected].transform.position + colliders[selected].center
            - colliders[selected].size/2;
        Vector3 selectedSize = colliders[selected].size;
        
        int sumAdjacent = 0; //隣接マップパーツ数カウンタ
        
        for(int i=0; i<mapParts.Length; i++)
        {
            if(i == selected){
                i++;
            }else{
                Vector3 otherTop = mapParts[i].transform.position + colliders[i].center
                    + colliders[i].size/2;
                Vector3 otherBottom = mapParts[i].transform.position + colliders[i].center
                    - colliders[i].size/2;
                Vector3 otherSize = colliders[i].size;
                Vector3 MaxDistance = selectedSize + colliders[i].size;
                MaxDistance += new Vector3(errorValue - contactBlockNum, errorValue - contactBlockNum, errorValue - contactBlockNum);
                
                //隣接判定 
                if( ((Mathf.Abs(selectedTop.z - otherBottom.z) < errorValue
                    || Mathf.Abs(selectedBottom.z - otherTop.z) < errorValue) &&
                        ((0 <= selectedTop.x - otherBottom.x && selectedTop.x - otherBottom.x < MaxDistance.x)
                        && (0 <= otherTop.x - selectedBottom.x && otherTop.x - selectedBottom.x < MaxDistance.x)))
                || 
                    ((Mathf.Abs(selectedTop.x - otherBottom.x) < errorValue
                    || Mathf.Abs(selectedBottom.x - otherTop.x) < errorValue) &&
                        ((0 <= selectedTop.z - otherBottom.z && selectedTop.z - otherBottom.z < MaxDistance.z)
                        && (0 <= otherTop.z - selectedBottom.z && otherTop.z - selectedBottom.z < MaxDistance.z))) )
                {
                        changeMapPartsEnablement(i, true); //Bottom側の判定が不安定なので念のため
                        sumAdjacent++;
                }
            }
        }
        
        if(sumAdjacent != 0){
            changeMapPartsEnablement(selected, true);
        }else{
            changeMapPartsEnablement(selected, false);
        }
    }
    
    /// <summary>
    /// マージボタンの有効/無効を切り替える．
    /// </summary>
    /// <param name="enable">有効:true 無効:false</param>
    private void changeButtonInteractable(bool enable)
    {
        if(buttonInteractable != enable)
        {
            mergeButtonComponent.interactable = enable;
            buttonInteractable = enable;
        }
    }
    
    /// <summary>
    /// マップパーツを有効/無効に切り替える．
    /// </summary>
    /// <param name="index">MapPartsのインデックス</param>
    /// <param name="enable">有効:true 無効:false</param>
    private void changeMapPartsEnablement(int index, bool enable)
    {
        if(mapPartsEnable[index] != enable){
            if(enable){
                changeAllMaterialsColor(mapParts[index], enableColor);
            }else{
                changeAllMaterialsColor(mapParts[index], disableColor);
            }
            mapPartsEnable[index] = enable;
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
