using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevelTwo : MonoBehaviour
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
            SceneManager.LoadScene("Level-2");
            gm.lastCheckPoint = GameObject.FindWithTag("StartPos").transform.position;
        }

    }
}
