using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

   public void PlayGame() //function to load next level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  //loads next scene in queue
    }
        
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit(); //closes down program
    }
}
