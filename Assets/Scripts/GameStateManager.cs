using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    NotStarted,
    Playing,
    Ended
}
public class GameStateManager : MonoBehaviour
{
    protected static GameStateManager Instance;
    public static GameState State { get; private set; }
    public static UnityEvent OnStarted = new UnityEvent();
    public static UnityEvent OnEnded = new UnityEvent();
    public static UnityEvent OnLeaved = new UnityEvent();
    public static int Lap { get; private set; }
    public static bool DidWin { get; private set; }

    private void Awake()
    {
        Instance = this;
        setTimescaleToZero();
    }

    private void Start()
    {
        State = GameState.NotStarted;
        Lap = PlayerPrefs.GetInt("LAP", 0) % 2;
    }

    public static void StartGame()
    {
        Lap = (PlayerPrefs.GetInt("LAP", 0) + 1) % 2;
        PlayerPrefs.SetInt("LAP", Lap);

        State = GameState.Playing;
        Instance.setTimescaleToNormal();

        OnStarted?.Invoke();
    }

    public static void EndGame(bool didWin)
    {
        State = GameState.Ended;
        DidWin = didWin;

        Instance.slowlyTimescaleToZero();

        OnEnded?.Invoke();
    }

    public static void LeaveGame()
    {
        State = GameState.NotStarted;

        Instance.setTimescaleToZero();

        OnLeaved?.Invoke();
    }

    private void setTimescaleToZero()
    {
        Time.timeScale = 0f;
    }

    private void setTimescaleToNormal()
    {
        Time.timeScale = 1f;
    }

    private void slowlyTimescaleToZero()
    {
        StartCoroutine(timescaleLerper(1f, 0f, 0.1f));
    }

    private IEnumerator timescaleLerper(float from, float to, float time)
    {
        float t = 0f;

        while(t < time)
        {
            t += Time.unscaledDeltaTime;

            Time.timeScale = EasingFunction.EaseInOutCirc(from, to, t / time);

            yield return null;
        }

        Time.timeScale = to;
    }
}
