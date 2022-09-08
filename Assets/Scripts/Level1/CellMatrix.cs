using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMatrix
{

    private int _py;
    private int _px;

    private string _name;
    private Texture _wallTexture;
    private Texture _soilTexture;

    private GameObject _cellPrefab;
    private Transform _tr;
    private Vector3 _scale;

    private GameObject _cell;

    public CellMatrix SetWallTexture(Texture wallTexture)
    {
        _wallTexture = wallTexture;
        return this;
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

    public CellMatrix SetScale(Vector3 scale)
    {
        _scale = scale;
        return this;
    }

    public void CreateCell(Vector3 localPos,int coordsValue,Texture texture,bool isObstacle)
    {

        _cell = GameObject.Instantiate(_cellPrefab);
        _cell.AddComponent<Cell>();
        _cell.GetComponent<Cell>().setName(_name);
        _cell.GetComponent<Cell>().SetCoordinatePoints(_py, _px); //setPointY(_py);
        _cell.GetComponent<Cell>().SetCoords(coordsValue);
        _cell.GetComponent<Cell>().SetTexture(texture);
        _cell.GetComponent<Cell>().IsObstacle = isObstacle;
        _cell.transform.parent = _tr;
        _cell.transform.localPosition = localPos;
    }

    public void CreateCellWall(Texture wallTexture)
    {
        
        float posX = _cell.GetComponent<Cell>().transform.position.x;
        float posY = _cell.GetComponent<Cell>().transform.position.y + 1; // Defaults.C_CELL_UP_MOVE_UNIT;
        float posZ = _cell.GetComponent<Cell>().transform.position.z;

        _cell.GetComponent<Cell>().transform.position = new Vector3(posX, posY, posZ);
        _cell.GetComponent<Cell>().IsObstacle = true;
        _cell.GetComponent<Cell>().setName("wall");
        if (wallTexture != null)
        {
            _cell.GetComponent<Cell>().SetTexture(wallTexture);
        }
        else
        {
            _cell.GetComponent<Cell>().SetTexture(_wallTexture);
        }
    }

    public void DestroyCellWall(Texture cellTexture)
    {
        //float posX = _cell.GetComponent<Cell>().transform.position.x;
        //float posY = _cell.GetComponent<Cell>().transform.position.y - Defaults.C_CELL_UP_MOVE_UNIT;
        //float posZ = _cell.GetComponent<Cell>().transform.position.z;

        //_cell.GetComponent<Cell>().transform.position = new Vector3(posX, posY, posZ);
        //_cell.GetComponent<Cell>().IsObstacle = false;
        //if (cellTexture != null)
        //{
        //    _cell.GetComponent<Cell>().SetTexture(cellTexture);
        //}
        //else
        //{
        //    _cell.GetComponent<Cell>().SetTexture(_soilTexture);
        //}
    }

    public bool CreateCellObstacle(int py, int px, int pz)
    {
        //hacer que revise lo vecinos por si tiene que crear o no la celda
        return true;
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
