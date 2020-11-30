using UnityEngine;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    public int enemiesKilled = 0;

    void Update()
    {
        if(enemiesKilled >= 3)
        {
            SceneManager.LoadScene("Winner");
            print("Ganaste chavo");
        }
    }
}
