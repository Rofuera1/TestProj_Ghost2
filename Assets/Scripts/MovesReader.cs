using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesReader : MonoBehaviour
{
    public Rigidbody ReadingRB;

    private List<double> timecode_saves;
    private List<SerializablePosition> velocity_saves;

    private System.DateTime previousKey;
    private System.DateTime startKey;
    private Vector3 previousVelocity;
    private Quaternion previousRotation;

    private SerializablePosition startPosition;

    private void Start()
    {
        timecode_saves = new List<double>();
        velocity_saves = new List<SerializablePosition>();
        startKey = System.DateTime.Now;
        previousKey = System.DateTime.Now;
        previousVelocity = ReadingRB.velocity;

        startPosition = new SerializablePosition(ReadingRB.position, ReadingRB.rotation);

        saveCurrentVelocity();
    }

    private void FixedUpdate()
    {
        if (ReadingRB.velocity != previousVelocity || ReadingRB.rotation != previousRotation)
            saveCurrentVelocity();
    }

    private void saveCurrentVelocity()
    {
        Vector3 currentVelocity = ReadingRB.position;
        Quaternion currentRotation = ReadingRB.rotation;
        double seconds = (System.DateTime.Now - previousKey).TotalSeconds;

        timecode_saves.Add(seconds);
        SerializablePosition newVec = new SerializablePosition(currentVelocity, currentRotation);
        velocity_saves.Add(newVec);

        previousVelocity = currentVelocity;
        previousRotation = currentRotation;
        previousKey = System.DateTime.Now;
    }

    private void OnApplicationQuit()
    {
        Moves moves = new Moves(timecode_saves.ToArray(), velocity_saves.ToArray(), startPosition);
        Debug.Log(moves.timecodes.Length + " for " + (System.DateTime.Now - startKey).TotalSeconds + " seconds");
        FileLoader.SaveFile(moves, "save.moves");
    }
}

[System.Serializable]
public class Moves
{
    [SerializeField]
    public SerializablePosition StartPosition;
    [SerializeField]
    public double[] timecodes;
    [SerializeField]
    public SerializablePosition[] velocities;

    public Moves(double[] timecodes, SerializablePosition[] velocities, SerializablePosition startPos)
    {
        this.timecodes = timecodes;
        this.velocities = velocities;
        StartPosition = startPos;
    }
}

[System.Serializable]
public class SerializablePosition
{
    [SerializeField]
    public float X;
    [SerializeField]
    public float Y;
    [SerializeField]
    public float Z;
    [SerializeField]
    public float XRot;
    [SerializeField]
    public float YRot;
    [SerializeField]
    public float ZRot;
    [SerializeField]
    public float WRot;

    public SerializablePosition(float x, float y, float z, float xr, float yr, float zr, float wr)
    {
        X = x;
        Y = y;
        Z = z;
        XRot = xr;
        YRot = yr;
        ZRot = zr;
        WRot = wr;
    }

    public SerializablePosition(Vector3 newVec, Quaternion rotation)
    {
        X = newVec.x;
        Y = newVec.y;
        Z = newVec.z;

        XRot = rotation.x;
        YRot = rotation.y;
        ZRot = rotation.z;
        WRot = rotation.w;
    }
}
