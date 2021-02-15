//kanoko
//ボタンの状態による色変化を子要素にも反映させる

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TintChildObjectsColor : MonoBehaviour
{
    Button button;
    List<Graphic> targets;

    void Start()
    {
        button = GetComponent<Button>();
        targets = GetComponentsInChildren<Graphic>().Where(c => c.gameObject != gameObject).ToList();
    }

    void Update()
    {
        targets.ForEach(t => t.color = button.targetGraphic.canvasRenderer.GetColor());
    }
}