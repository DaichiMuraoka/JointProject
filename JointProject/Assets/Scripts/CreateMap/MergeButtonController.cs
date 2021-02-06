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
    private bool[] intrusion; //他パーツと重なっているか
    private List<List<int>> connected = new List<List<int>>(); //繋がっているマップパーツのインデックスリスト
    private bool[] appeared; //つながり判定用
    
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
        intrusion = new bool[mapParts.Length];
        appeared = new bool[mapParts.Length];
        
        for(int i=0; i<mapParts.Length; i++){
            //ボックスコライダーを取得
            colliders[i] = mapParts[i].GetComponent<BoxCollider>();
            //初期位置を保持
            previousPositions[i] = mapParts[i].transform.position;
            //最初は全て有効状態
            mapPartsEnable[i] = true;
            //他パーツとは重なっていない
            intrusion[i] = false;
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
                //つながりを初期化
                connected.Clear();
                //侵入判定を更新
                judgeMapPartsIntrusion(i);
                
                //MapPartsの有効判定
                for(int j=0; j<mapParts.Length; j++)
                    judgeMapPartsAdjacent(j);
                
                //前回位置を更新
                previousPositions[i] = mapParts[i].transform.position;
                
                //全てのマップパーツが有効のとき
                int enableSum = 0;
                for(int j=0; j<mapParts.Length; j++)
                    if(mapPartsEnable[j]) enableSum++;
                
                //全てのマップパーツが繋がっているとき
                if(enableSum == mapParts.Length && isConnectedAllMapParts(0))
                    changeButtonInteractable(true);
                else
                    changeButtonInteractable(false);
                
                break;
            }
        }
        
    }
    
    /// <summary>
    /// マップパーツが全て繋がっているか再帰的に判定する。
    /// 最初の呼び出し時の引数は0。
    /// </summary>
    /// <returns></returns>
    private bool isConnectedAllMapParts(int selected)
    {
        //初期化
        if(selected == 0){
            for(int i=0; i<appeared.Length; i++)
                appeared[i] = false;
        }
        
        //探索済なら
        if(appeared[selected] == true) return true;
        
        //selectedを探索済にする
        appeared[selected] = true;
        
        //全部探索済なら打ち切り
        int appearedCount = 0;
        for(int i=0; i<appeared.Length; i++)
                if(appeared[i] == true) appearedCount++;
        if(appearedCount == appeared.Length) return true;
        
        //connectedがなくなったら打ち切り
        if(connected.Count == 0) return false;
        
        //次の行き先をリストに追加
        List<int> contactParts = new List<int>();
        for(int i=0; i<connected.Count; i++){
            if(connected[i][0] == selected){
                if(appeared[connected[i][1]] == false) contactParts.Add(connected[i][1]);
                connected.RemoveAt(i);
            }
            else if(connected[i][1] == selected){
                if(appeared[connected[i][0]] == false) contactParts.Add(connected[i][0]);
                connected.RemoveAt(i);
            }
        }
        
        //行き先がなければ打ち切り
        if(contactParts.Count == 0) return false;
        
        //再帰探索
        for(int i=0; i<contactParts.Count; i++){
            bool dammy = isConnectedAllMapParts(contactParts[i]);
        }
        
        if(selected == 0){
            for(int i=0; i<appeared.Length; i++)
                if(appeared[i] == false)
                    return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// 指定のマップパーツについて、他パーツとの重なりを判定し、intrusionを更新する
    /// </summary>
    private void judgeMapPartsIntrusion(int selected)
    {
        float errorValue = 0.3f; //許容される誤差
        
        Vector3 selectedPos = mapParts[selected].transform.position + colliders[selected].center;
        Vector3 selectedSize = colliders[selected].size/2;
        
        intrusion[selected] = false;
        for(int i=0; i<mapParts.Length; i++)
        {
            if(i != selected){
                Vector3 otherPos = mapParts[i].transform.position + colliders[i].center;
                Vector3 otherSize = colliders[i].size/2;
                Vector3 MaxDistance = selectedSize + otherSize;
                
                float dx = Mathf.Abs(selectedPos.x - otherPos.x);
                float dz = Mathf.Abs(selectedPos.z - otherPos.z);
                
                //侵入判定
                if(dx < MaxDistance.x - errorValue && dz < MaxDistance.z - errorValue)
                {
                    intrusion[selected] = true;
                    break;
                }
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
        float errorValue = 0.3f; //許容される誤差
        
        Vector3 selectedPos = mapParts[selected].transform.position + colliders[selected].center;
        Vector3 selectedSize = colliders[selected].size/2;
        
        int sumAdjacent = 0; //隣接マップパーツ数カウンタ
        
        for(int i=0; i<mapParts.Length; i++)
        {
            if(i != selected){
                Vector3 otherPos = mapParts[i].transform.position + colliders[i].center;
                Vector3 otherSize = colliders[i].size/2;
                Vector3 MaxDistance = selectedSize + otherSize;
                
                float dx = Mathf.Abs(selectedPos.x - otherPos.x);
                float dz = Mathf.Abs(selectedPos.z - otherPos.z);
                
                //隣接判定 
                if((Mathf.Abs(dx - MaxDistance.x) < errorValue && dz < MaxDistance.z - contactBlockNum + errorValue)
                || (Mathf.Abs(dz - MaxDistance.z) < errorValue && dx < MaxDistance.x - contactBlockNum + errorValue) )
                {
                    connected.Add(new List<int>{selected, i}); //隣接している組み合わせを保存
                    sumAdjacent++;
                }
                
            }
        }
        
        //他パーツに侵入しておらず、隣接パーツがあるとき有効にする
        if(intrusion[selected] == false && sumAdjacent != 0){
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
