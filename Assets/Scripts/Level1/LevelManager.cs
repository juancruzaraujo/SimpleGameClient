using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectionData connData = ConnectionData.ConnectionDataInstance;

        InGameConsole.ManagerConsola.instance.WriteLine(connData.PlayerName);
        InGameConsole.ManagerConsola.instance.WriteLine(connData.Host + ":" + connData.Port);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
