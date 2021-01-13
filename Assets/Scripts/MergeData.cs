using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum dataType {Human, Tree, Water}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateNewMergeData", order = 1)]
public class MergeData : ScriptableObject
{
    public dataType type;
    public string name;
    public int level;

    public GameObject mergeVisuals;

}
