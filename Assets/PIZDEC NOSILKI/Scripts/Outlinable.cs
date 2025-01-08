using UnityEngine;

public class Outlinable : MonoBehaviour
{
    [HideInInspector] public string info;
    [HideInInspector] public bool isChangable;
    [HideInInspector] public bool isOnceUnchangable;
    [HideInInspector] public Outline outline;
    [HideInInspector] public Color color = new();
}