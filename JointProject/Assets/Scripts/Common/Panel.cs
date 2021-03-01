using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private GameObject root = null;

    public void SetActive(bool active)
    {
        root.SetActive(active);
    }

    public bool activeSelf
    {
        get { return root.activeSelf; }
    }
}
