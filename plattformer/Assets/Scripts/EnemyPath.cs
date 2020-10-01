using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // Start is called before the first frame update
    public Node[] path;
    private int current;

    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(path[current].transform);
        transform.Translate(transform.forward * Time.deltaTime, Space.Self);
        if (Mathf.Abs(transform.position.x - path[current].transform.position.x) < 0.5)
        {
            current += 1;
            current %= 2;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
