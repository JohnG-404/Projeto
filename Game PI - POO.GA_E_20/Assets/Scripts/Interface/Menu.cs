using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    
    void Start(){
        instance = this;
    }
    public void Game(string FaseName)
    {
        SceneManager.LoadScene(FaseName);
    }
    public void Sair(){
        Application.Quit();
    }
}
