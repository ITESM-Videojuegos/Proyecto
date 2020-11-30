using UnityEngine;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    [HideInInspector] public int enemiesKilled = 0;
    [SerializeField] private int enemiesToKill;

    void Update()
    {
        if(enemiesKilled >= enemiesToKill)
        {
            SceneManager.LoadScene("Winner");
            print("Ganaste chavo");
        }
    }
}
