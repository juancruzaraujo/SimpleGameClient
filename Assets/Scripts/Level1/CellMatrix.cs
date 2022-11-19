using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMatrix
{

    private int _py;
    private int _px;

    private string _name;
    //private Texture _wallTexture;
    //private Texture _soilTexture;
    private Texture _texture;

    private GameObject _cellPrefab;
    private Transform _tr;
    private Vector3 _scale;
    Vector3 _localPos;

    private GameObject _cell;

    public CellMatrix SetTexture(Texture texture)
    {
        //wallTexture = wallTexture;
        _texture = texture;
        return this; ;
    }
        
    public CellMatrix SetPrefab(GameObject cellprefab)
    {
        _cellPrefab = cellprefab;
        return this;
    }

    public CellMatrix setPy(int py)
    {
        _py = py;
        return this;
    }


    public float GetPx
    {
        get
        {
            return _cell.GetComponent<Cell>().transform.position.x;
        }

    }

    public float GetPy
    {
        get
        {
            return _cell.GetComponent<Cell>().transform.position.y;
        }
    }

    public float GetPz
    {
        get
        {
            return _cell.GetComponent<Cell>().transform.position.z;
        }
    }

    public CellMatrix setPx(int px)
    {
        
        _px = px;
        return this;
    }

    public CellMatrix setName(string name)
    {
        _name = name;
        return this;
    }

    public CellMatrix setParent(Transform pos)
    {
        _tr = pos;
        return this;
    }

    public Transform GetParent
    {
        get
        {
            return _tr;
        }
    }

    public CellMatrix setLocalPoss(Vector3 localPosition)
    {
        _localPos = localPosition;
        return this;
    }

    public Vector3 GetLocalPoss
    {
        get
        {
            return _localPos;
        }
    }

    public CellMatrix SetScale(Vector3 scale)
    {
        _scale = scale;
        return this;
    }

    public void CreateCell(Vector3 localPos,int coordsValue,Texture texture,bool isObstacle)
    {

        _texture = texture;
        _cell = GameObject.Instantiate(_cellPrefab);
        _cell.AddComponent<Cell>();
        _cell.GetComponent<Cell>().setName(_name);
        _cell.GetComponent<Cell>().SetCoordinatePoints(_py, _px); 
        _cell.GetComponent<Cell>().SetCoords(coordsValue);
        _cell.GetComponent<Cell>().SetTexture(texture);
        _cell.GetComponent<Cell>().IsObstacle = isObstacle;
        _cell.transform.parent = _tr;
        _cell.transform.localPosition = localPos;
        setLocalPoss(localPos);
    }

    public Vector3 GetCellPosition()
    {
        return _cell.GetComponent<Cell>().GetPosition;
    }

    public bool IsCellObstacle()
    {
        return _cell.GetComponent<Cell>().IsObstacle;
    }

    public Cell GetCell()
    {
        return _cell.GetComponent<Cell>();
    }
}
