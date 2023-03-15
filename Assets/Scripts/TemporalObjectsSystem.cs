using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalObjectsSystem : MonoBehaviour
{
    public static TemporalObjectsSystem Instance;

    [SerializeField] public Transform proyectilesContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
