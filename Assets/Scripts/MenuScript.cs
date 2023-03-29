using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public GameObject WallMenu;
    public GameObject GameMarcer;
    public GameObject GameOwer;
    void Start(){
        GameMarcer.SetActive(false);
        GameOwer.SetActive(false);
    }
    public void Play()
    {
      WallMenu.SetActive(false);
      GameMarcer.SetActive(true);
      GameOwer.SetActive(false);
    }
    public void Menu()
    {
       WallMenu.SetActive(true);
       GameMarcer.SetActive(false);
       GameOwer.SetActive(false);
    }
    public void PersonDed()
    {
        WallMenu.SetActive(false);
        GameMarcer.SetActive(false);
        GameOwer.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
