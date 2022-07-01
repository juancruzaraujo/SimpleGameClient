using InGameConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{

    string _playerName;
    string _host;
    string _port;
    // Start is called before the first frame update
    void Start()
    {
        //ejemplo
        //GameObject obj = GameObject.Find("MenuManager");
        //MenuManager menuManager = obj.GetComponent<MenuManager>();
        GetMenuValues();

    }

    public void ConnectToServerAndGoToScene(string sceneName)
    {
        GetMenuValues();

        ConnectionData connData = ConnectionData.ConnectionDataInstance;
        connData.PlayerName = _playerName;
        connData.Host = _host;
        connData.Port = _port;

        GameObject obj = GameObject.Find("MenuManager");
        MenuManager menuManager = obj.GetComponent<MenuManager>();
        menuManager.LoadScene(sceneName);
    }

    private void GetMenuValues()
    {

        GameObject obj = GameObject.Find("TxtInputName");
        InputField txtName = obj.GetComponent<InputField>();
        _playerName = txtName.text;

        obj = GameObject.Find("TxtInputHost");
        InputField txtHost = obj.GetComponent<InputField>();
        string host = txtHost.text;

        string[] connData = host.Split(':');
        _host = connData[0];
        _port = connData[1];
    }
}
