using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellObjectParameters
{
    //? hace que sea nulleable
    //int? contador;

    Vector3 _localPos;
    float _posX;
    float _posY;
    float _posZ;

    public CellObjectParameters()
    {
        _localPos = new Vector3();
    }

    public float PosX 
    { 
        get
        {
            return _posX;
        }
        set
        {
            _posX = value;
            _localPos.x = _posX;
        }
    }
    public float PosY 
    { 
        get
        {
            return _posY;
        }
        set
        {
            _posY = value;
            _localPos.y = _posY;
        }
    }
    public float PosZ 
    { 
        get
        {
            return _posZ;
        }
        set
        {
            _posZ = value;
            _localPos.z = _posZ;
        }
    }

    public int ArrayPosX { get; set; }
    public int ArrayPosY { get; set; }
    public int ArrayPosZ { get; set; }
    public string Name { get; set; }
    public Transform Parent { get; set; }
    public Vector3 LocalPoss 
    { 
        get
        {
            return _localPos;
        }
        set
        {
            _localPos = value;
            _posX = _localPos.x;
            _posY = _localPos.y;
            _posZ = _localPos.z;
        }
    }
    public int Coords { get; set; }

    public Texture Texture { get; set; }
    public bool IsObstacle { get; set; }

    public GameObject CellPrefab { get; set; }
    public Vector3 Escale { get; set; }




}
