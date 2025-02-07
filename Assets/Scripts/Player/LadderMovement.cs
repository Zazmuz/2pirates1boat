using UnityEngine;

public class LadderMovement : MonoBehaviour {
    private float verticalSpeed = 8f;
    private float horizontalSpeed = 8f;
    private bool isLadder;
    public bool isClimbing;
    private PlayerMovement _pm;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerMovementStats PlayerStats;

    public void Awake() {
        _pm = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (isLadder && InputManager.Movement.y > 0f) {
            isClimbing = true;
            _pm.enabled = false;
        } 
        else if (isClimbing && InputManager.Movement.y == 0) {
            isClimbing = false;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Lerp(_rb.velocity.y, -PlayerStats.MaxFallSpeed * 0.5f, 0.1f));
        }
    }   

    private void FixedUpdate() {
        if (isClimbing) {
            _rb.velocity = new Vector2(InputManager.Movement.x * horizontalSpeed, InputManager.Movement.y * verticalSpeed);
        } 
        else if (isLadder) {
            _rb.velocity = new Vector2(InputManager.Movement.x * horizontalSpeed, -verticalSpeed * 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            isLadder = true;
            Debug.Log("Entered ladder area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            isLadder = false;
            isClimbing = false;
            _pm.VerticalVelocity = _rb.velocity.y;
            _pm.MoveVelocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
            _pm.enabled = true; 
            Debug.Log("Exited ladder");
        }
    }
}
