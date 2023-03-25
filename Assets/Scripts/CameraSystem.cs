using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    #region"Singleton"
    static CameraSystem instance;
    public static CameraSystem Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform activeTarget;

    public bool active = true;

    [SerializeField] Camera cam;
    [SerializeField] float zOffset = -10f;

    private void LateUpdate()
    {
        Vector3 offset = new Vector3(0, 0, zOffset);
        cam.transform.position = activeTarget.position + offset;
    }
}
