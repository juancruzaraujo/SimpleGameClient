using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coords
{
    int _py;
    int _px;
    int _pz;
    public Coords(int py, int px, int pz)
    {
        _py = py;
        _px = px;
        _pz = pz;
    }

    public int GetPy
    {
        get
        {
            return _py;
        }
    }

    public int GetPx
    {
        get
        {
            return _px;
        }
    }

    public int GetPz
    {
        get
        {
            return _pz;
        }
    }
}
