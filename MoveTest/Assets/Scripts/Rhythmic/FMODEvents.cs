using System.Runtime.InteropServices;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Song")]
    [field: SerializeField] public EventReference song { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference noteFailed { get; private set; }
    [field: Header("PowerUps SFX")]
    [field: SerializeField] public EventReference Invulnerable { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("Hay mas de 1 FMODEvents en escena");
        }
        instance = this;
    }
}
