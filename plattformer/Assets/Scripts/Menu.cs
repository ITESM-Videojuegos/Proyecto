using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private GameMaster gm;

    public void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level-1");
        gm.lastCheckPoint = gm.startCheckPoint;
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Que chin... a su madre el America");
    }
}
