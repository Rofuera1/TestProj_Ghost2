using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
            GameStateManager.EndGame(true);
        else if (other.tag == "Bottom")
            GameStateManager.EndGame(false);
    }
}
