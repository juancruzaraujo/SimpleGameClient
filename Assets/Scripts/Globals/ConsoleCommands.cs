using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;
using System;
using Sockets;
using UnityEngine.SceneManagement;

public class ConsoleCommands : MonoBehaviour
{
    // Start is called before the first frame update


    //borrar
    Socket _tcpSocketClient;
    string _dataIn;
    int _tcpConnectionNumber;
    //borrar



    void Start()
    {

        
        ManagerConsola.instance.WriteLine(SceneManager.GetActiveScene().name);
        ManagerConsola.instance.WriteLine("hola mundo?");

        CreateCommands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCommands()
    {
        //Commands.commandInstance.AddCommand("/gotolevel", "move to level", ChangeLevel);
        //Commands.commandInstance.AddCommand("/sendTCP", "envía un mensaje al servidor", SendTCPMessageToServer);
        //Commands.commandInstance.AddCommand("/sendUDP", "envía un mensaje al servidor", SendUDPMessageToServer);
        //Commands.commandInstance.AddCommand("/send", "prueba de la lib de sockets", Send);
        //Commands.commandInstance.AddCommand("/startserver", "inicia un mini server", StartServer);
        Commands.commandInstance.AddCommand("/test", "test", Test);
        Commands.commandInstance.AddCommand("/conn", "conn", ConnectToServer);
        Commands.commandInstance.AddCommand("/level", "level 1", GoToLevel);
    }

    private void GoToLevel(string command, string parameters)
    {
        GameObject obj = GameObject.Find("MenuManager");
        MenuManager menuManager = obj.GetComponent<MenuManager>();
        menuManager.LoadScene(parameters);
    }

    private void ConnectToServer(string command, string parameters)
    {
        _tcpSocketClient = new Socket();
        _tcpSocketClient.Event_Socket += TCPSocketClient_Event_Socket;

        ConnectionParameters connectionParameters = new ConnectionParameters();
        connectionParameters.SetPort(1492);
        connectionParameters.SetHost("127.0.0.1");
        connectionParameters.SetProtocol(Protocol.ConnectionProtocol.TCP);

        _tcpSocketClient.ConnectClient(connectionParameters);
    }

    private void Test(string command, string parameters)
    {
        ManagerConsola.instance.WriteLine("test test");
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
                //ReadCommandFromServer(_dataIn);
                ManagerConsola.instance.WriteLine(eventParameters.GetData);
                break;
        }
    }
}
