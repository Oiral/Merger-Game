using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeDataManager : MonoBehaviour
{
    #region SINGLTON
    public static MergeDataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.Log("Found multiple Data Managers");
            Destroy(this);
        }
    }
    #endregion


    public List<MergeData> humanData;
    public List<MergeData> treeData;
    public List<MergeData> waterData;



    public List<MergeData> GetDataType(dataType type)
    {
        switch (type)
        {
            case dataType.Human:
                return humanData;
            case dataType.Tree:
                return treeData;
            case dataType.Water:
                return waterData;
            default:
                return humanData;
        }
    }

    public MergeData GetNextMerge(MergeData data)
    {
        List<MergeData> merges = GetDataType(data.type);

        if (merges.Contains(data))
        {
            int index = merges.IndexOf(data);
            if (index >= merges.Count - 1)
            {
                //We have reached the end of the merging
                Debug.LogError("We have reached the end of the merge tree");
                return null;
            }
            else
            {
                return merges[index + 1];
            }
        }
        else
            return null;
    }

    public int GetDataID(MergeData data)
    {
        List<MergeData> merges = GetDataType(data.type);

        if (merges.Contains(data))
        {
            return merges.IndexOf(data);
        }
        else
            return 0;
    }
}
