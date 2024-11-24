using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform rbToMove;
    public GameObject objectVisual;
    private Moves moves;

    private void Start()
    {
        GameStateManager.OnStarted.AddListener(onGameStarted);
    }

    private void onGameStarted()
    {
        objectVisual.SetActive(GameStateManager.Lap == 1);

        if (GameStateManager.Lap == 0)
            return;

        moves = FileLoader.ReadFromFile<Moves>("save.moves");
        if (moves == null) return;

        SerializablePosition startPos = moves.StartPosition;
        rbToMove.position = new Vector3(startPos.X, startPos.Y, startPos.Z);
        rbToMove.rotation = new Quaternion(startPos.XRot, startPos.YRot, startPos.ZRot, startPos.WRot);

        StartCoroutine(executeMoves());
    }

    private IEnumerator executeMoves()
    {
        for(int i = 0; i < moves.timecodes.Length; i++)
        {
            //yield return new WaitForSeconds();
            SerializablePosition oldVec = moves.velocities[i];
            Vector3 newVec = new Vector3(oldVec.X, oldVec.Y, oldVec.Z);
            Quaternion newRot = new Quaternion(oldVec.XRot, oldVec.YRot, oldVec.ZRot, oldVec.WRot);

            float time = (float)moves.timecodes[i];
            float t = 0f;

            Vector3 from = rbToMove.position;
            Quaternion fromRot = rbToMove.rotation;

            while(t < time)
            {
                t += Time.deltaTime;

                rbToMove.position = Vector3.Lerp(from, newVec, t / time);
                rbToMove.rotation = Quaternion.Lerp(fromRot, newRot, t / time);

                yield return null;
            }
            //rbToMove.rotation = newRot;
            //rbToMove.position = newVec;
        }
    }
}
