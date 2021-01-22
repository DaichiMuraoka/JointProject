//kanoko

//https://qiita.com/keroxp/items/97d375786617c9eca783
//より借用したスクリプトを改変
//プレイ中にマップパーツをグリッドにスナップするために用いる

using System;
using UnityEngine;

namespace Hexat.Editor
{
    //[ExecuteInEditMode]
    //Rendererが必須
    [RequireComponent(typeof(Renderer))]
    public class SnapToGrid : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private Vector3 center;
        [SerializeField] private Vector2 cellSize = new Vector3(1, 1);
        [SerializeField] private int width = 1;
        [SerializeField] private int height = 1;
        [SerializeField] private Vector2 offset;
        [SerializeField] private bool isOdd = true;

        private enum HorizontalPivot
        {
            Left = -1,
            Middle = 0,
            Right = 1
        }

        private enum VerticalPivot
        {
            Top = 1,
            Midle = 0,
            Bottom = -1
        }

        [SerializeField] private HorizontalPivot _horizontalPivot = HorizontalPivot.Middle;
        [SerializeField] private VerticalPivot _verticalPivot = VerticalPivot.Midle;

        //Rendererコンポーネントを読み込む
        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private Vector3 pivotCenter
        {
            get
            {
                var v = new Vector3(
                    ((int) _horizontalPivot + 1) / 2f,
                    ((int) _verticalPivot + 1) / 2f,
                    0
                );
                return new Vector3(minX, minY, 0) + Vector3.Scale(new Vector3(width, height, 0), v);
            }
        }

        private Bounds bounds
        {
            get { return _renderer.bounds; }
        }

        private float minX
        {
            get
            {
                switch (_horizontalPivot)
                {
                    case HorizontalPivot.Left:
                        return bounds.min.x + offset.x;
                    case HorizontalPivot.Middle:
                        return bounds.center.x - (cellSize.x * width) / 2 + offset.x;
                    case HorizontalPivot.Right:
                        return bounds.max.x - cellSize.x * width + offset.x;
                }
                return 0;
            }
        }

        private float minY
        {
            get
            {

                switch (_verticalPivot)
                {
                    case VerticalPivot.Top:
                        return bounds.max.y - cellSize.y * height + offset.y;
                    case VerticalPivot.Midle:
                        return bounds.center.y - (cellSize.y * height) / 2 + offset.y;
                    case VerticalPivot.Bottom:
                        return bounds.min.y + offset.y;
                }
                return 0;
            }
        }

        //オブジェクトをグリッドにスナップ
        private void DoSnap()
        {
            var x = minX + cellSize.x * width;
            var y = minY + cellSize.y * height;
            var x0 = Mathf.Floor(center.x + cellSize.x * (int) (x / cellSize.x));
            if (isOdd) x0 -= cellSize.x / 2;
            var y0 = Mathf.Floor(center.y + cellSize.y * (int) (y / cellSize.y));
            if (isOdd) y0 -= cellSize.y / 2;
            var dx = x - x0 > 0.5f ? x0 + cellSize.x - x : x0 - x;
            var dy = y - y0 > 0.5f ? y0 + cellSize.y - y : y0 - y;
            transform.position += new Vector3(dx, 0, dy);
        }

        private void Update()
        {
            DoSnap();
        }

    }
}