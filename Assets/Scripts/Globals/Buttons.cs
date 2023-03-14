using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Windows.Input;

public class Buttons : MonoBehaviour
{
    public string sceneToCall;

    public void Click(string parameter)
    {
        
        InGameConsole.ManagerConsola.instance.WriteLine(parameter);
        GameObject obj = GameObject.Find("MenuManager");
        MenuManager menuManager = obj.GetComponent<MenuManager>();
        menuManager.LoadScene(parameter);
    }
}
