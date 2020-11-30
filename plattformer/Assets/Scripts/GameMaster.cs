using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    private static GameMaster instance;
    public Vector2 lastCheckPoint;
    public Vector2 startCheckPoint;
    public int playerLifes;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        startCheckPoint = new Vector2(-7.5f, -1f);
    }



    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

}
