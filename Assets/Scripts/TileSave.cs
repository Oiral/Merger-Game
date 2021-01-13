using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileSave
{
    public Vector3 pos
    {
        get { return new Vector3(xPos, 0, zPos); }
        set {
            xPos = value.x;
            zPos = value.z;
        }
    }
    float yRotation;

    float xPos;
    float zPos;

    public Quaternion rotation
    {
        get { return Quaternion.Euler(0, yRotation, 0); }
        set { yRotation = value.eulerAngles.y; }
    }


    
    public MergeData data
    {
        get { return MergeDataManager.instance.GetDataType(type)[dataID]; }
        set { type = value.type;
            dataID = MergeDataManager.instance.GetDataID(value);
        }
    }
    dataType type;
    int dataID;


    public TileSave(Vector3 _pos, Quaternion _rotation, MergeData _data)
    {
        pos = _pos;
        rotation = _rotation;
        data = _data;
    }
}
