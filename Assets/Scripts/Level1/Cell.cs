using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private string _name;
    private int _py;
    private int _px;
    private int _coordsValue;
    private Texture _cellTexture;
    private Renderer _renderer;
    Vector3 _scale;
    private bool _obstacle;
    private Coords[] _coords;
    private Rigidbody _rb;

    public bool IsObstacle
    {
        set
        {
            _obstacle = value;
        }
        get
        {
            return _obstacle;
        }
    }

    public void setName(string name)
    {
        _name = name;
        gameObject.name = _name;
    }

    public int GetPx
    {
        get
        {
            return _px;
        }
    }

    public int GetPy
    {
        get
        {
            return _py;
        }
    }

    public Vector3 GetPosition
    {
        get
        {
            return transform.position;
        }
    }

    public void SetCoordinatePoints(int py, int px)
    {
        _py = py;
        _px = px;
    }

    public Texture CellTexture
    {
        get
        {
            return _cellTexture;
        }
    }

    public void SetTexture(Texture cellTexture)
    {
        _cellTexture = cellTexture;
        _renderer.material.mainTexture = _cellTexture;
    }

    public void SetCoords(int coordsValue)
    {
        _coordsValue = coordsValue;
    }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _scale = transform.localScale;
        _coords = new Coords[_coordsValue]; 
        _rb = gameObject.AddComponent<Rigidbody>();

        _rb.useGravity = false;
        _rb.isKinematic = true;
        _rb.constraints = RigidbodyConstraints.FreezePosition;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    public void SetNeighbour(int cardinalPoint, int neigbourPy, int neigbourPx, int neigbourPz)
    {

    }

    //por temas de tiempo hice esto acá
    //pero lo ideal sería usar un patrón eventmaneger que tome las interacciones de los usuarios
    //y haga lo necesario, pero bueno
    void OnMouseOver()
    {

        ////hacer que ponga bien la textura, hay que pasarla desde grid
        //if (Input.GetMouseButtonUp(1))
        //{
        //    float y = transform.position.y;
        //    float x = transform.position.x;
        //    float z = transform.position.z;

        //    if (!_obstacle)
        //    {

        //        _obstacle = true;
        //        y = y + Defaults.C_CELL_UP_MOVE_UNIT;
        //    }
        //    else
        //    {
        //        _obstacle = false;
        //        y = y - Defaults.C_CELL_UP_MOVE_UNIT;
        //    }

        //    transform.position = new Vector3(x, y, z);
        //    GlobalValues.recalculatePath = true;
        //}

    }
}
