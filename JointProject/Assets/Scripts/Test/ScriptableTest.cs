using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "test", menuName = "test")]
public class ScriptableTest : ScriptableObject
{
    [SerializeField]
    private int num = 0;
}
