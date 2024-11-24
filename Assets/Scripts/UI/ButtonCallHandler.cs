using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCallHandler : MonoBehaviour
{
    public void OnPressedStart()
    {
        GameStateManager.StartGame();
    }

    public void OnPressedToMenu()
    {
        GameStateManager.LeaveGame();
    }
}
