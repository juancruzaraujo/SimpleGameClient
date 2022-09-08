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
        _gridContainer.name = "Grid Container";
        _gridContainer.AddComponent<Grid>();
        _gridContainer.GetComponent<Grid>().SetCellPrefab = cubeDefault;
        _gridContainer.GetComponent<Grid>().SetSoilTexture = soilTexture;
        _gridContainer.GetComponent<Grid>().SetWallTexture = wallTexture;

        //recien ahora intento conectarme
        _tcpSocketClient.ConnectClient(connectionParameters); 
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    private void TCPSocketClient_Event_Socket(EventParameters eventParameters)
    {
        switch (eventParameters.GetEventType)
        {
            case EventParameters.EventType.CLIENT_CONNECTION_OK:
                InGameConsole.ManagerConsola.instance.WriteLine(eventParameters.GetEventType.ToString());
                _tcpConnectionNumber = eventParameters.GetConnectionNumber;
                _tcpSocketClient.Send(_tcpConnectionNumber, ConnectionCommands.SEND_CONNECTION_OK);
                break;

            case EventParameters.EventType.DATA_IN:
                Debug.Log(eventParameters.GetData);
                //InGameConsole.ManagerConsola.instance.WriteLine(eventParameters.GetData);
                _dataIn = eventParameters.GetData;
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

        string[] commandParameters = new string[10];
        
        if (commandFromServer !="")
        {
            commandParameters = commandFromServer.Split('|');
        }


        if (commandFromServer.Contains(ConnectionCommands.DATAIN_CREATE_GRID))
        {
            int size_x = int.Parse(commandParameters[1]);
            int size_y = int.Parse(commandParameters[2]);
            int size_z = int.Parse(commandParameters[3]);
            int coordsValue = int.Parse(commandParameters[4]);

            InGameConsole.ManagerConsola.instance.WriteLine("Creando grilla>");
            _gridContainer.GetComponent<Grid>().CreateGrid(size_x, size_y, size_z,coordsValue);
            _gridContainer.GetComponent<Grid>().CreatePerimeterWall();


            InGameConsole.ManagerConsola.instance.WriteLine("Grilla creada>");
            _tcpSocketClient.Send(_tcpConnectionNumber, ConnectionCommands.SEND_LEVEL_CREATED);


            //result = GameComunication.DATASEND_CREATE_GRID + "|" + ConstantValues.LEVEL_SIZE_X + "|" + ConstantValues.LEVEL_SIZE_Y + "|" + ConstantValues.LEVEL_SIZE_Z;
        }

        /*
        if (commandFromServer.Contains(ConnectionCommands.DATAIN_CREATE_WALL))
        {
            InGameConsole.ManagerConsola.instance.WriteLine("Creado muro>");
            _gridContainer.GetComponent<Grid>().CreatePerimeterWall();
            InGameConsole.ManagerConsola.instance.WriteLine("Muro creado");
            _tcpSocketClient.Send(_tcpConnectionNumber, ConnectionCommands.SEND_WALL_CREATED);
        }
        */
    }
}
