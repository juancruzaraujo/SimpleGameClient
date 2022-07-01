using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionData : GlobalValuesManager<ConnectionData>
{
    string _playerName;
    string _host;
    string _port;

    public string PlayerName
    {
        set
        {
            _playerName = value; 
        }
        get
        {
            return _playerName;
        }

    }

    public string Host
    {
        set
        {
            _host = value;
        }
        get
        {
            return _host;
        }
    }

    public string Port
    {
        set
        {
            _port = value;
        }
        get
        {
            return _port;
        }
    }

    public static ConnectionData ConnectionDataInstance
    {
        get
        {
            return ((ConnectionData)GlobalValuesManagerInstance);
        }
        set
        {
            GlobalValuesManagerInstance = value;
        }
    }

    protected ConnectionData() { }
}
