using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToBank()
    {
        SceneManager.LoadScene("BankScene");
    }
    public void GoToSlotMachine()
    {
        SceneManager.LoadScene("SlotMachineScene");
    }
    public void GoToSlotMinigame()
    {
        SceneManager.LoadScene("MinigameScene");
    }
    public void GoToBerryMinigame()
    {
        SceneManager.LoadScene("StrawberryGame");
    }
}
