using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlockDataBase : ScriptableObject
{
    public List<BlockData> blocksData;
}

[Serializable]
public class BlockData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public bool needTop { get; private set; }
    [field: SerializeField]
    public bool needBottom { get; private set; }
    [field: SerializeField]
    public Vector3Int price { get; private set; }
}