using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevelThree : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("demo");
            gm.lastCheckPoint = GameObject.FindWithTag("StartPos").transform.position;
            print(gm);
        }
            
    }
}
