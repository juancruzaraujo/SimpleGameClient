using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private CellMatrix[,,] _cellMatrix;
    private CellMatrix[,,] _cellMatrixWall;
    private CellMatrix[,,] _cellMatrixObstacles;
    private GameObject _cellPrefab;
    private Texture _soilTexture;
    private Texture _wallTexture;

    private int _levelSizeX;
    private int _levelSizeY;
    private int _leveSizeZ;
    private int _levelHeightZ;

    private int _coords;

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

    public int GetLevelSizeX
    {
        get
        {
            return _levelSizeX;
        }
    }

    public int GetLevelSizeY
    {
        get
        {
            return _levelSizeY;
        }
    }

    public int GetDepth
    {
        get
        {
            return _leveSizeZ;
        }
    }

    public Vector3 GetCellPosition(Coords celCoords)
    {
        return _cellMatrix[celCoords.GetPy, celCoords.GetPx, celCoords.GetPz].GetCellPosition();
    }

    public void CreateGrid(int x, int y, int z, int coordsValue)
    {

        Vector3 hInc = new Vector3(1, 0, 0);
        Vector3 vInc = new Vector3(0, 0, -1);
        Vector3 dInc = new Vector3(0, 1, 0);


        _levelSizeX = x;
        _levelSizeY = y;
        _leveSizeZ = z;
        _levelHeightZ = _leveSizeZ - 1;

        transform.position = new Vector3(-_levelSizeX * 0.5f, _leveSizeZ * 0.5f, _levelSizeY * 0.5f);
        
        _cellMatrix = new CellMatrix[_levelSizeY, _levelSizeX, _leveSizeZ];

        for (int px = 0; px < _levelSizeX; px++)
        {
            for (int py = 0; py < _levelSizeY; py++)
            {
                for (int pz = 0; pz <= _levelHeightZ; pz++)
                {

                    CellObjectParameters cellObjectParameters = new CellObjectParameters();
                    cellObjectParameters.ArrayPosY = py;
                    cellObjectParameters.ArrayPosX = px;
                    cellObjectParameters.ArrayPosZ = pz;
                    cellObjectParameters.CellPrefab = _cellPrefab;
                    cellObjectParameters.Name = "soil[" + py.ToString() + "," + px.ToString() + "]";
                    cellObjectParameters.Parent = transform;
                    cellObjectParameters.Escale = transform.localScale;
                    cellObjectParameters.Texture = _soilTexture;
                    cellObjectParameters.LocalPoss = hInc * px + vInc * py + dInc * pz;
                    cellObjectParameters.Coords = coordsValue;
                    cellObjectParameters.IsObstacle = false;
                    _cellMatrix[py, px, pz] = CreateAuxCell(cellObjectParameters);



                }
            }
        }

    }

    //private CellMatrix CreateAuxCell(float posX, float posY, float posZ)
    private CellMatrix CreateAuxCell(CellObjectParameters cellObjectParameters)
    {
        CellMatrix auxCell = new CellMatrix();
        auxCell.SetPrefab(cellObjectParameters.CellPrefab);
        auxCell.setName(cellObjectParameters.Name);
        auxCell.CreateCell(cellObjectParameters.LocalPoss, cellObjectParameters.Coords, cellObjectParameters.Texture, cellObjectParameters.IsObstacle);

        return auxCell;
    }

    public void CreatePerimeterWall()
    {
       
        _cellMatrixWall = new CellMatrix[_levelSizeY, _levelSizeX, _leveSizeZ];

        CellObjectParameters cellObjectParameters = new CellObjectParameters();
        cellObjectParameters.Coords = 8; //arreglar este hardcodeo por favor
        cellObjectParameters.CellPrefab = _cellPrefab;
        cellObjectParameters.Texture = _wallTexture;

        for (int pz = 0; pz <= _levelHeightZ; pz++)
        {
            for (int py = 0; py < _levelSizeY; py++)
            {                
                cellObjectParameters.PosX = _cellMatrix[py, _levelSizeX - 1, pz].GetPx;
                cellObjectParameters.PosY = _cellMatrix[py, _levelSizeX - 1, pz].GetPy +1;
                cellObjectParameters.PosZ = _cellMatrix[py, _levelSizeX - 1, pz].GetPz;
                cellObjectParameters.Name = "wall[" + cellObjectParameters.PosY + "," + cellObjectParameters.PosX + "," + cellObjectParameters.PosZ + "]";
                _cellMatrixWall[py, 0, pz] = CreateAuxCell(cellObjectParameters);

                cellObjectParameters.PosX = _cellMatrix[py, 0, pz].GetPx;
                cellObjectParameters.PosY = _cellMatrix[py, 0, pz].GetPy + 1;
                cellObjectParameters.PosZ = _cellMatrix[py, 0, pz].GetPz;
                cellObjectParameters.Name = "wall[" + cellObjectParameters.PosY + "," + cellObjectParameters.PosX + "," + cellObjectParameters.PosZ + "]";
                _cellMatrix[py, _levelSizeX - 1, pz] = CreateAuxCell(cellObjectParameters);
            }

            for (int px = 1; px < _levelSizeX - 1; px++)
            {
                cellObjectParameters.PosX = _cellMatrix[1, px, pz].GetPx;
                cellObjectParameters.PosY = _cellMatrix[1, px, pz].GetPy + 1;
                cellObjectParameters.PosZ = _cellMatrix[1, px, pz].GetPz +1;
                cellObjectParameters.Name = "wall[" + cellObjectParameters.PosY + "," + cellObjectParameters.PosX + "," + cellObjectParameters.PosZ + "]";
                _cellMatrixWall[1, px, pz] = CreateAuxCell(cellObjectParameters);

                cellObjectParameters.PosX = _cellMatrix[_levelSizeY - 1, px, pz].GetPx;
                cellObjectParameters.PosY = _cellMatrix[_levelSizeY - 1, px, pz].GetPy + 1;
                cellObjectParameters.PosZ = _cellMatrix[_levelSizeY - 1, px, pz].GetPz;
                cellObjectParameters.Name = "wall[" + cellObjectParameters.PosY + "," + cellObjectParameters.PosX + "," + cellObjectParameters.PosZ + "]";
                _cellMatrix[_levelSizeY - 1, px, pz] = CreateAuxCell(cellObjectParameters);

            }
        }
    }

    public void CreateObstacles(List<Map.obstacles> lstObstacles)
    {
        if (lstObstacles == null)
        {
            return;
        }

        _cellMatrixObstacles = new CellMatrix[_levelSizeY, _levelSizeX, _leveSizeZ];
        CellObjectParameters cellObjectParameters = new CellObjectParameters();

        for (int i = 0; i<lstObstacles.Count;i++)
        {
            int x = lstObstacles[i].x;
            int y = lstObstacles[i].y;
            int z = lstObstacles[i].z;

            
            cellObjectParameters.PosX = _cellMatrix[y, x, z].GetPx;
            cellObjectParameters.PosY = _cellMatrix[y, x, z].GetPy + 1;
            cellObjectParameters.PosZ = _cellMatrix[y, x, z].GetPz;
            cellObjectParameters.Name = "obstacle[" + cellObjectParameters.PosY + "," + cellObjectParameters.PosX + "," + cellObjectParameters.PosZ + "]";
            cellObjectParameters.Texture = _wallTexture;
            cellObjectParameters.CellPrefab = _cellPrefab;
            _cellMatrixObstacles[y, x, z] = CreateAuxCell(cellObjectParameters);

        }
    }

    public void DestroyObtacles()
    {
        for (int pz = 0; pz < _levelHeightZ; pz++)
        {
            for (int py = 1; py < _levelSizeY - 1; py++)
            {
                for (int px = 1; px < _levelSizeX - 1; px++)
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
