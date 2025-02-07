using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats MoveStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody2D _rb;

    public Vector2 MoveVelocity {get; set;}
    private bool _isFacingRight;

    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedHead;


    //jump variables
    public float VerticalVelocity {get; set;}
    private bool _isJumping;
    private bool _isFastFalling;
    private bool _isFalling;
    private float _fastFallTime;
    private float _fastFallReleaseSpeed;
    private int _numberOfJumpsUsed;

    //Apex variables
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    //Jump buffer variables
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;

    //coyote time variables
    private float _coyoteTimer;

    //Ladder checks
    private LadderMovement lm;

    private void Update(){
        CountTimers();
        JumpChecks();
    }
    private void Awake(){
        _isFacingRight = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        CollisionChecks();
        
        Jump();

        if(_isGrounded){
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeacceleration, InputManager.Movement);
        }else{
            Move(MoveStats.AirAcceleration, MoveStats.AirDeacceleration, InputManager.Movement);
        }
    }

    #region Movement
    

    private void Move(float acceleration, float deacceleration, UnityEngine.Vector2 moveInput){
        if(moveInput != UnityEngine.Vector2.zero){

            TurnCheck(moveInput);

            UnityEngine.Vector2 targetVelocity = UnityEngine.Vector2.zero;
            if(InputManager.RunIsHeld){
                targetVelocity = new UnityEngine.Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
            }
            else{
                targetVelocity = new UnityEngine.Vector2(moveInput.x,0f) * MoveStats.MaxWalkSpeed;
            }

            MoveVelocity = UnityEngine.Vector2.Lerp(MoveVelocity,targetVelocity, acceleration * Time.fixedDeltaTime);
            _rb.velocity = new UnityEngine.Vector2(MoveVelocity.x, _rb.velocity.y);
        }
        else if(moveInput == UnityEngine.Vector2.zero){
            MoveVelocity = UnityEngine.Vector2.Lerp(MoveVelocity, UnityEngine.Vector2.zero, deacceleration * Time.fixedDeltaTime);
            _rb.velocity = new UnityEngine.Vector2(MoveVelocity.x, _rb.velocity.y);
        }
    }

    private void TurnCheck(UnityEngine.Vector2 moveInput){
        if(_isFacingRight && moveInput.x < 0){
            Turn(false);
        }
        else if(!_isFacingRight && moveInput.x > 0){
            Turn(true);
        }
    }

    private void Turn(bool turnRight){
        if(turnRight){
            _isFacingRight = true;
            transform.Rotate(0f,180f,0f);   
        }
        else{
            _isFacingRight = false;
            transform.Rotate(0f,-180f,0f);
        }
    }
    #endregion
    #region Jump 

    //buffer stuff, collision checks and coyote timer
    private void JumpChecks(){
        if(InputManager.JumpWasPressed){
            _jumpBufferTimer = MoveStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }

        if(InputManager.JumpWasReleased){
            if(_jumpBufferTimer > 0f){
                _jumpReleasedDuringBuffer = true;   
            }
            if(_isJumping && VerticalVelocity > 0f){
                if(_isPastApexThreshold){
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    _fastFallTime = MoveStats.TimeForUpwardsCancel; 
                    VerticalVelocity = 0f;
                }else{
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticalVelocity;
                }

            }
        }
        if(_jumpBufferTimer > 0f && !_isJumping && (_isGrounded || _coyoteTimer > 0f)){
            InitiateJump(1);

            if(_jumpReleasedDuringBuffer){
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticalVelocity;
            }
        }
        else if(_jumpBufferTimer > 0f && _isJumping && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed){
            _isFastFalling = false;
            InitiateJump(1);
        }
        else if(_jumpBufferTimer > 0f && _isFalling && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed - 1){
            InitiateJump(2);
            _isFastFalling = false;
        }

        if((_isFalling || _isJumping) && _isGrounded && VerticalVelocity <= 0f){
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;
            _fastFallTime = 0f;
            _isPastApexThreshold = false;
            _numberOfJumpsUsed = 0;

            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    private void Jump(){
        if(_isJumping){
            if(_bumpedHead){
                _isFastFalling = true;
            }
            if(VerticalVelocity >= 0f){

                _apexPoint = Mathf.InverseLerp(MoveStats.InitialJumpVelocity, 0f, VerticalVelocity);

                if(_apexPoint > MoveStats.ApexThreshold){

                    if(!_isPastApexThreshold){
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    if(_isPastApexThreshold){
                        _timePastApexThreshold += Time.fixedDeltaTime;
                        if(_timePastApexThreshold < MoveStats.ApexHangTime){
                            VerticalVelocity = 0f;
                        }else{
                            VerticalVelocity = -0.01f;
                        }
                        
                    }
                }else{
                    VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
                    if(_isPastApexThreshold){
                        _isPastApexThreshold = false;
                    }
                    
                }
            }
            else if(!_isFastFalling){
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if(VerticalVelocity < 0f){
                if(!_isFalling){
                    _isFalling = true;
                }
            }

        }
    
        //jump cut
        if(_isFastFalling){
            if(_fastFallTime >= MoveStats.TimeForUpwardsCancel){
                VerticalVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }
            else if(_fastFallTime < MoveStats.TimeForUpwardsCancel){
                VerticalVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (_fastFallTime / MoveStats.TimeForUpwardsCancel));

            }
            _fastFallTime += Time.fixedDeltaTime;
        }

        //gravity stuff
        if(!_isGrounded && !_isJumping){
            if(!_isFalling){
                _isFalling = true;
            }

            VerticalVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }

        //clamp fall speeeeeed

        VerticalVelocity = Mathf.Clamp(VerticalVelocity, -MoveStats.MaxFallSpeed, 50f);
        _rb.velocity = new Vector2(_rb.velocity.x, VerticalVelocity);
    }

    private void InitiateJump(int numberOfJumpsUsed){
        if(!_isJumping){
            _isJumping = true;
        }
        _jumpBufferTimer = 0f;
        _numberOfJumpsUsed += numberOfJumpsUsed;
        VerticalVelocity = MoveStats.InitialJumpVelocity;
        
    }
    #endregion
    #region Collision Checks

    private void IsGrounded(){
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayer);
        if(_groundHit.collider != null){
            _isGrounded = true;

        }else{
            _isGrounded = false;
        }
    }
    private void BumpedHead(){
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * MoveStats.HeadWidth, MoveStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRayLength, MoveStats.GroundLayer);
        if(_headHit.collider != null){
            _bumpedHead = true;
        }else{
            _bumpedHead = false;
        }
    }
    private void CollisionChecks(){
        IsGrounded();
        BumpedHead();
    }

    #endregion
    #region Timers
    private void CountTimers(){
        _jumpBufferTimer -= Time.deltaTime;

        if(!_isGrounded){
            _coyoteTimer  -= Time.deltaTime;
        }else{
            _coyoteTimer = MoveStats.JumpCoyoteTime;
        }

    }
    #endregion
}
