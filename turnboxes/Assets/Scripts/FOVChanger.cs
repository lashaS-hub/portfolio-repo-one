using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVChanger : MonoBehaviour
{


    private Camera cam;

    public float hor;


    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = calcVertivalFOV(hor, cam.aspect);

    }

    private float calcVertivalFOV(float hFOVInDeg, float aspectRatio)
    {
        float hFOVInRads = hFOVInDeg * Mathf.Deg2Rad;
        float vFOVInRads = 2 * Mathf.Atan(Mathf.Tan(hFOVInRads / 2) / aspectRatio);
        float vFOV = vFOVInRads * Mathf.Rad2Deg;
        return vFOV;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        cam.orthographicSize = calcVertivalFOV(hor, cam.aspect);
        
    }
}
