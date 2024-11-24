using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject StartMenuWindow;
    public GameObject InGameWindow;
    public GameObject EndGameWindow;
    private GameObject currentWindow;
    [Space]
    public GameObject EndGame_Win;
    public GameObject EndGame_Lost;
    public Text LapText;

    private void Start()
    {
        GameStateManager.OnStarted.AddListener(ActivateInGameOverlay);
        GameStateManager.OnEnded.AddListener(ActivateEndGameWindow);
        GameStateManager.OnLeaved.AddListener(ActivateMainMenu);

        ActivateMainMenu();
    }

    private void deactivateCurrentWindow()
    {
        if (!currentWindow) return;
        currentWindow.SetActive(false);
    }

    private void setNewWindowActive(GameObject whichWindow)
    {
        currentWindow = whichWindow;
        currentWindow.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        EndGame_Win.SetActive(false);
        EndGame_Lost.SetActive(false);

        deactivateCurrentWindow();
        setNewWindowActive(StartMenuWindow);
    }

    public void ActivateInGameOverlay()
    {
        deactivateCurrentWindow();

        LapText.text = "Lap " + GameStateManager.Lap.ToString() + "/2";

        setNewWindowActive(InGameWindow);
    }

    public void ActivateEndGameWindow()
    {
        deactivateCurrentWindow();
        setNewWindowActive(EndGameWindow);

        if (GameStateManager.DidWin)
            StartCoroutine(Coroutines.LerpTransformScaleRealTime(EndGame_Win.transform, Vector3.zero, Vector3.one, 0.2f, EasingFunction.Linear));
        else
            StartCoroutine(Coroutines.LerpTransformScaleRealTime(EndGame_Lost.transform, Vector3.zero, Vector3.one, 0.2f, EasingFunction.Linear));
    }
}
