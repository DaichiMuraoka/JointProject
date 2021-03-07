using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumber : MonoBehaviour
{
    [SerializeField] private MapDeliverer mapDeliverer = null;
    
    void Awake()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "レベル" + (mapDeliverer.Level).ToString();
    }
}
