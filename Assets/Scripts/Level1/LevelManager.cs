using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sockets;


public class LevelManager : MonoBehaviour
{

    public Texture soilTexture;
    public Texture wallTexture;

    public GameObject genericContainer;

    private GameObject _gridContainer;
    //private GameObject _wallContainer;
    //private GameObject _obstacleContainer;

    private GameObject _minionContainer;

    public GameObject cubeDefault; //va a contener lo que es el piso y paredes

    public GameObject leader;
    private GameObject _leader;

    public GameObject minion;
    public int minionsCount;

    public GameObject wayPointPrefab;

    Socket _tcpSocketClient;
    string _dataIn;
    int _tcpConnectionNumber;

    private void Awake()
    {
        
        InGameConsole.ManagerConsola.instance.WriteLine("Iniciando nivel");
        InGameConsole.Commands.commandInstance.AddCommand("/conn", "test", SetConnectionParameters);
        
        _dataIn = "";
        _tcpSocketClient = new Socket();
        _tcpSocketClient.Event_Socket += TCPSocketClient_Event_Socket;

        ConnectionData connData = ConnectionData.ConnectionDataInstance;

        InGameConsole.ManagerConsola.instance.WriteLine(connData.PlayerName);
        InGameConsole.ManagerConsola.instance.WriteLine(connData.Host + ":" + connData.Port);

        ConnectionParameters connectionParameters = new ConnectionParameters();
        connectionParameters.SetPort(int.Parse(connData.Port));
        connectionParameters.SetHost(connData.Host);
        connectionParameters.SetProtocol(Protocol.ConnectionProtocol.TCP);

        _gridContainer = Instantiate(genericContainer);
        _gridContainer.name = "Grid_Container";
        _gridContainer.AddComponent<Grid>();
        _gridContainer.GetComponent<Grid>().SetCellPrefab = cubeDefault;
        _gridContainer.GetComponent<Grid>().SetSoilTexture = soilTexture;
        _gridContainer.GetComponent<Grid>().SetWallTexture = wallTexture;

        //recien ahora intento conectarme
        
        //InGameConsole.ManagerConsola.instance.WriteLine("Conectando");
        ////_tcpSocketClient.ConnectClient(connectionParameters);
        ConnetToServer(connectionParameters);
        
        
    }


    //para pruebas, hay que borrar esta función
    void SetConnectionParameters(string param1, string param2)
    {
        ConnectionData connData = ConnectionData.ConnectionDataInstance;

        InGameConsole.ManagerConsola.instance.WriteLine(connData.PlayerName);
        InGameConsole.ManagerConsola.instance.WriteLine(connData.Host + ":" + connData.Port);

        ConnectionParameters connectionParameters = new ConnectionParameters();
        connectionParameters.SetPort(int.Parse(connData.Port));
        connectionParameters.SetHost(connData.Host);
        connectionParameters.SetProtocol(Protocol.ConnectionProtocol.TCP);

        ConnetToServer(connectionParameters);
    }

    void ConnetToServer(ConnectionParameters connectionParameters)
    {
        _tcpSocketClient.ConnectClient(connectionParameters);
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameConsole.ManagerConsola.instance.WriteLine("start level");
    }

    private void TCPSocketClient_Event_Socket(EventParameters eventParameters)
    {
        
        switch (eventParameters.GetEventType)
        {
            case EventParameters.EventType.CLIENT_CONNECTION_OK:
                //InGameConsole.ManagerConsola.instance.WriteLine(eventParameters.GetEventType.ToString());
                _tcpConnectionNumber = eventParameters.GetConnectionNumber;
                _tcpSocketClient.Send(_tcpConnectionNumber, ConnectionCommands.SEND_CONNECTION_OK);
                break;

            case EventParameters.EventType.DATA_IN:
                Debug.Log(eventParameters.GetData);
                //InGameConsole.ManagerConsola.instance.WriteLine(eventParameters.GetData); //NO VA
                _dataIn = eventParameters.GetData;
                //ReadCommandFromServer(_dataIn); //NO VA
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_dataIn !="")
        {
            InGameConsole.ManagerConsola.instance.WriteLine(_dataIn);
            ReadCommandFromServer(_dataIn);
            _dataIn = "";
        }
        
        
    }

    private void ReadCommandFromServer(string commandFromServer)
    {
        BodyMessage body = new BodyMessage();
        var bodyMessage = JsonConverter.JsonToClass(commandFromServer, typeof(BodyMessage), body);
        body = (BodyMessage)bodyMessage;

        string[] commandParameters = new string[10];


        if (body.messageTag == ConnectionCommands.DATAIN_CREATE_GRID)
        {
            Map map = new Map();
            var mapMsg = JsonConverter.JsonToClass(body.messageBody, typeof(Map), map);
            map = (Map)mapMsg;


            int size_x = map.LevelSizeX;
            int size_y = map.LevelSizeY;
            int size_z = map.LevelSizeZ;
            int coordsValue = map.Coords;
            List<Map.obstacles> lstObstacles = map.lstObstacles;

            InGameConsole.ManagerConsola.instance.WriteLine("Creando grilla>");
            _gridContainer.GetComponent<Grid>().CreateGrid(size_x, size_y, size_z, coordsValue);
            _gridContainer.GetComponent<Grid>().CreatePerimeterWall();
            _gridContainer.GetComponent<Grid>().CreateObstacles(lstObstacles);


            InGameConsole.ManagerConsola.instance.WriteLine("Grilla creada>");
            _tcpSocketClient.Send(_tcpConnectionNumber, ConnectionCommands.SEND_LEVEL_CREATED);

        }

    }
}
