using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerActionViewer : ActionViewer
{
    protected PlayerController playerController { get; private set; }

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
}