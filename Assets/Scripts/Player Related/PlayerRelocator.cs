using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRelocator : MonoBehaviour
{
    public Transform StartPosition;
    public Rigidbody Player;

    private void Start()
    {
        GameStateManager.OnLeaved.AddListener(onGameStarted);
    }
    private void onGameStarted()
    {
        Player.velocity = Vector3.zero;
        Player.position = StartPosition.position;
        Player.rotation = StartPosition.rotation;
    }
}
