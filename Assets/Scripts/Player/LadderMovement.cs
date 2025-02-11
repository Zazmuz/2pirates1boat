using UnityEngine;

public class LadderMovement : MonoBehaviour {
    private bool isLadder;
    private bool isClimbing;
    private PlayerMovement _pm;
    InputManager inputManager;

    [Header("References")]
    [SerializeField] private PlayerMovementStats PlayerStats;
    [SerializeField] private Rigidbody2D _rb;
    

    public void Awake() {
        inputManager = GetComponentInChildren<InputManager>();
        _pm = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (isLadder && inputManager.Movement.y > 0f) {
            isClimbing = true;
            _pm.enabled = false;
        } 
        else if (isClimbing && inputManager.Movement.y == 0) {
            isClimbing = false;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, -PlayerStats.MaxFallSpeed * 0.5f, 0.1f));
        }
    }   

    private void FixedUpdate() {
        if (isClimbing) {
            _rb.velocity = new Vector2(inputManager.Movement.x * PlayerStats.HorizontalClimbSpeed, inputManager.Movement.y * PlayerStats.VerticalClimbSpeed);
        } 
        else if (isLadder) {
            _rb.velocity = new Vector2(inputManager.Movement.x * PlayerStats.HorizontalClimbSpeed, -PlayerStats.VerticalClimbSpeed * 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            isLadder = false;
            isClimbing = false;
            _pm.VerticalVelocity = _rb.velocity.y;
            _pm.MoveVelocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
            _pm.enabled = true; 
        }
    }
}
