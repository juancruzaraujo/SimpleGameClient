using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //private Cell _matrixCells;
    private CellMatrix[,,] _cellMatrix;
    private CellMatrix[,,] _cellMatrixWall;
    private GameObject _cellPrefab;
    private Texture _soilTexture;
    private Texture _wallTexture;

    private int _width;
    private int _high;
    private int _depth;

    public GameObject SetCellPrefab
    {
        set
        {
            _cellPrefab = value;
        }
    }

    public Texture SetSoilTexture
    {
        set
        {
            _soilTexture = value;
        }
    }

    public Texture SetWallTexture
    {
        set
        {
            _wallTexture = value;
        }
    }

    public int GetWidth
    {
        get
        {
            return _width;
        }
    }

    public int GetHigh
    {
        get
        {
            return _high;
        }
    }

    public int GetDepth
    {
        get
        {
            return _depth;
        }
    }

    public Vector3 GetCellPosition(Coords celCoords)
    {
        return _cellMatrix[celCoords.GetPy, celCoords.GetPx, celCoords.GetPz].GetCellPosition();
    }

    public void CreateGrid(int width, int high, int depth, int coordsValue)
    {

        Vector3 hInc = new Vector3(1, 0, 0);
        Vector3 vInc = new Vector3(0, 0, -1);
        Vector3 dInc = new Vector3(0, 1, 0);

        
        _width = width;
        _high = high;
        _depth = depth;

        transform.position = new Vector3(-_width * 0.5f, _depth * 0.5f, _high * 0.5f);
        
        _cellMatrix = new CellMatrix[_high, _width, _depth];

        for (int py = 0; py < _high; py++)
        {
            for (int px = 0; px < _width; px++)
            {
                for (int pz = 0; pz < _depth; pz++)
                {

                    CellMatrix auxCell = new CellMatrix();
                    auxCell.setPy(py);
                    auxCell.setPx(px);
                    
                    auxCell.SetPrefab(_cellPrefab);
                    auxCell.setName("soil[" + py.ToString() + "," + px.ToString() + "]");
                    auxCell.setParent(transform);
                    auxCell.SetScale(transform.localScale);
                    auxCell.CreateCell(hInc * px + vInc * py + dInc * pz,coordsValue,_soilTexture,false);

                    _cellMatrix[py, px, pz] = auxCell;

                }
            }
        }

    }

    public void CreatePerimeterWall()
    {

        #region OLD
        /*
        for (int pz = 0; pz < _depth; pz++)
        {
            for (int py = 0; py < _high; py++)
            {
                //_cellMatrix[py, 0, pz].CreateCellWall(null);
                //_cellMatrix[py, _width - 1, pz].CreateCellWall(null);
            }

            for (int px = 1; px < _width - 1; px++)
            {
                //_cellMatrix[0, px, pz].CreateCellWall(null);
                //_cellMatrix[_high - 1, px, pz].CreateCellWall(null);
                
            }
        }*/
        #endregion

        
        _cellMatrixWall = new CellMatrix[_high, _width, _depth];
        float posX;
        float posY;
        float posZ;

        for (int pz = 0; pz < _depth; pz++)
        {
            for (int py = 0; py < _high; py++)
            {
                posX = _cellMatrix[py, 0, pz].GetPx;
                posY = _cellMatrix[py, 0, pz].GetPy +1;
                posZ = _cellMatrix[py, 0, pz].GetPz;
                _cellMatrixWall[py, 0, pz] = CreateAuxCell(posX, posY, posZ);

                posX = _cellMatrix[py, _width - 1, pz].GetPx;
                posY = _cellMatrix[py, _width - 1, pz].GetPy + 1;
                posZ = _cellMatrix[py, _width - 1, pz].GetPz;

                _cellMatrix[py, _width - 1, pz] = CreateAuxCell(posX, posY, posZ);

            }

            for (int px = 1; px < _width - 1; px++)
            {
                
                posX = _cellMatrix[1, px, pz].GetPx;
                posY = _cellMatrix[1, px, pz].GetPy + 1;
                posZ = _cellMatrix[1, px, pz].GetPz+1;
                _cellMatrixWall[1, px, pz] = CreateAuxCell(posX, posY, posZ);

                posX = _cellMatrix[_high -1, px, pz].GetPx;
                posY = _cellMatrix[_high - 1, px, pz].GetPy + 1;
                posZ = _cellMatrix[_high - 1, px, pz].GetPz;
                
                _cellMatrix[_high-1, px, pz] = CreateAuxCell(posX, posY, posZ);

            }
        }
    }


    private CellMatrix CreateAuxCell(float posX,float posY, float posZ)
    {
        CellMatrix auxCell = new CellMatrix();

        auxCell.SetPrefab(_cellPrefab);
        auxCell.setName("wall[" + posY.ToString() + "," + posX.ToString() + "]");
        
        Vector3 vec = new Vector3(posX, posY, posZ);

        auxCell.CreateCell(vec, 8, _wallTexture, false);

        return auxCell;
    }

    public void Createobstacles(int radioNoObstacles = 0)
    {
        /*
        for (int pz = 0; pz < _depth; pz++)
        {
            for (int py = 1; py < _high - 1; py++)
            {
                for (int px = 1; px < _width - 1; px++)
                {
                    if ((py > radioNoObstacles) | (px > radioNoObstacles))
                    {
                        if (Random.Range(0, 100) < Defaults.C_VAL_GEN_OBSTACLE)
                        {
                            if (_cellMatrix[py, px, pz].CreateCellObstacle(py, px, pz))
                            {
                                _cellMatrix[py, px, pz].CreateCellWall(null);
                            }
                        }
                    }
                }
            }
        }*/
    }

    public void DestroyObtacles()
    {
        for (int pz = 0; pz < _depth; pz++)
        {
            for (int py = 1; py < _high - 1; py++)
            {
                for (int px = 1; px < _width - 1; px++)
                {

                    if (_cellMatrix[py, px, pz].IsCellObstacle())
                    {
                        _cellMatrix[py, px, pz].DestroyCellWall(_soilTexture);
                    }

                }
            }
        }
    }

    public bool IsCellObstacle(int y, int x, int z)
    {
        return _cellMatrix[y, x, z].IsCellObstacle();
    }

    public Cell GetCell(int y, int x, int z)
    {
        return _cellMatrix[y, x, z].GetCell();
    }

    public void UpdateGrid()
    {

    }
}
