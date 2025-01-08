using UnityEngine;
using Valve.VR.InteractionSystem;
public class CancelTeleportHintScript : MonoBehaviour 
{  
    void Update () 
    { 
        Teleport.instance.CancelTeleportHint(); 
    } 
} 