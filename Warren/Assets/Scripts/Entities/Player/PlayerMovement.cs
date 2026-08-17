using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour, IVariableValues
{
    [SerializeField] float baseSpeed;
    float finalSpeed;
    Vector2 _direction;
    Vector2 _facing;
    bool canMove = true;

    Rigidbody2D rb;
    PlayerAnimator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimator>();

    }

    private void OnEnable()
    {
        PlayerInput.OnMovePressed += (t) => { _direction = t.ReadValue<Vector2>().normalized; _facing = _direction; };
        PlayerInput.OnMoveCanceled += (t) => _direction = Vector2.zero;

        PlayerSpell.UpdateValues += UpdateValues;
    } 

    private void OnDisable()
    {
        PlayerInput.OnMovePressed -= (t) => { _direction = t.ReadValue<Vector2>().normalized; _facing = _direction; };
        PlayerInput.OnMoveCanceled -= (t) => _direction = Vector2.zero;

        PlayerSpell.UpdateValues -= UpdateValues;
    }

    private void FixedUpdate()
    {
        if (!canMove) { rb.linearVelocity = Vector2.zero; return; }

        rb.linearVelocity = _direction * finalSpeed;
    }

    private void Update()
    {
        animator.SetAnimPos(_facing.x, _facing.y);

        if (rb.linearVelocity != Vector2.zero) animator.SetAnimBool("IsRunning", true);
        else animator.SetAnimBool("IsRunning", false);

        if (_direction != Vector2.zero) transform.localScale = new Vector2(_direction.x >= 0f ? -1 : 1, 1);
    }

    public void StopMovement() { canMove = false; }
    public void StartMovement() { canMove = true; }

    public void UpdateValues()
    {
        finalSpeed = PlayerSpell.AdjustValue<IAdjustSpeed>(baseSpeed, x => x.AdjustSpeed(), ModType.Player);
    }
}
