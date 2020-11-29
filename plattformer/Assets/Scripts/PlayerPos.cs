using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;

    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPoint;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
        else if (Input.GetKeyDown(KeyCode.K))
            gm.EndGame();
    }


    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
