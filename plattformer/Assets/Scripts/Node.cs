using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbors;


    //Let's do GIZMOS!
    //Gizmos only runs on editor, not on gameplay
    //2 points where we can draw gizmos
    //- non-selected object
    private void OnDrawGizmos()
    {
        //draw line between neighbors
        Gizmos.color = Color.red;

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (neighbors[i] == null)
            {
                continue;
            }
            Gizmos.DrawLine(
                transform.position,
                neighbors[i].transform.position
                );
        }
    }

    //- selected object
    private void OnDrawGizmosSelected()
    {
        //set color first
        Gizmos.color = Color.cyan;

        //anything we draw will have the same color until we change it again
        Gizmos.DrawWireSphere(transform.position, 1);

    }
}
