using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPanel : Panel
{
    [SerializeField]
    private GameObject howto = null;

    [SerializeField]
    private GameObject localCount2 = null;

    private void Start()
    {
        Close();
    }

    public void Open(bool isLocalCount2)
    {
        SetActive(true);
        howto.SetActive(!isLocalCount2);
        localCount2.SetActive(isLocalCount2);
    }

    public void Close()
    {
        SetActive(false);
    }
}
