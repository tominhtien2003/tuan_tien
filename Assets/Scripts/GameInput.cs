using UnityEngine;

public class GameInput : MonoBehaviour
{
    public PlayerInputAction playerInputAction { get; private set; }
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
    }
    private void Start()
    {
        playerInputAction.Player.Enable();
    }
    public Vector2 GetMovementInputVector()
    {
        return playerInputAction.Player.Move.ReadValue<Vector2>().normalized;
    }
}
