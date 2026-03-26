using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    InputActions.PlayerActions _player;

    public Vector2 PlayerFacing {  get; private set; }

    private void Awake()
    {
        _player = new InputActions().Player;
    }

    #region Initialise
    private void OnEnable() { EnablePlayer(); }
    private void OnDisable() { DisablePlayer(); }

    public void EnablePlayer() { _player.Enable(); }
    public void DisablePlayer() { _player.Disable(); }
    #endregion

    public Vector2 ReadInput()
    {
        Vector2 dir = _player.Move.ReadValue<Vector2>();
        if (dir != Vector2.zero) { PlayerFacing = dir.normalized; }

        return dir;
    }

    public bool Attack() { return _player.Attack.WasPressedThisFrame(); }

    public bool Parry() { return _player.Parry.WasPressedThisFrame(); }

    public bool Interact() { return _player.Interact.WasPressedThisFrame(); }

    public bool Spell() { return _player.Spell.WasPressedThisFrame(); }
}
