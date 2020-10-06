using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator3 : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(5f, 0f, 0f);
    [SerializeField] float period = 3f;

    float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        // todo protect against period is zero
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
