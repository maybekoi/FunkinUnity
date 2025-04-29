using UnityEngine;

public class SpriteSpinner : MonoBehaviour
{
    public float bpm = 120f;
    private float secondsPerBeat;
    private float rotationSpeed;

    private void Start()
    {
        secondsPerBeat = 60f / bpm;
        rotationSpeed = 360f / secondsPerBeat;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
