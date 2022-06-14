using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string sceneToCall;

    public void Click(string parameter)
    {
        GameObject obj = GameObject.Find("MenuManager");
        MenuManager menuManager = obj.GetComponent<MenuManager>();
        menuManager.LoadScene(parameter);
    }
}
