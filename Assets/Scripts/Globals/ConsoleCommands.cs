using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameConsole;
using System;

public class ConsoleCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void Test(string command, string parameters)
    {
        ManagerConsola.instance.WriteLine("test test");
    }

    
}
