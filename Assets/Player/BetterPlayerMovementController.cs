using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BetterPlayerMovementController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private LayerMask GroundLayerMask;

    [Header("Horizontal")]
    /// <summary>
    /// Hard max speed. Players cannot go past this.
    /// </summary>
    [SerializeField] private float MaxHorizontalSpeed = 50f;
    /// <summary>
    /// Players can go past this, but the game will continuously try to slow them to this speed
    /// </summary>
    [SerializeField] private float TargetHorizontalSpeed = 10f;
    [SerializeField] private float HorizontalAccelerationGround = 200f;
    [SerializeField] private float HorizontalAccelerationAir = 80f;
    [SerializeField] private float HoriontalFrictionGround = 20f;
    [SerializeField] private float HoriontalFrictionAir = 20f;

    [Header("Vertical")]
    [SerializeField] private Vector2 MaxVerticalSpeed = new Vector2(-30f, 10f);
    [SerializeField] private float Gravity = -40f;
    [SerializeField] private float GravityAcceleration = 40f;
    [SerializeField] private float JumpForce = 11f;
    
    [Header("Wall Jumping")]
    [SerializeField] private Vector2 WallJumpForce = new Vector2(3f, 6f);
    [SerializeField] private float WallStickTime = 0.5f;
    /// <summary>
    /// Gravity is reduce by this factor when touching a wall
    /// </summary>
    [SerializeField] private float WallGravityDampener = 0.5f;

    private float _currentGravity;
    private float _currentHorizontalAcceleration => _touchingGround ? HorizontalAccelerationGround * Time.deltaTime : HorizontalAccelerationAir * Time.deltaTime;
    private float _currentHorizontalFriction => _touchingGround ? HoriontalFrictionGround * Time.deltaTime : HoriontalFrictionAir * Time.deltaTime;

    private bool _touchingGround = false;
    private bool _touchingRightWall = false;
    private bool _touchingLeftWall = false;
    private float _lastTimeRightKeyDown = 0;
    private float _lastTimeLeftKeyDown = 0;

    private Vector3 _velocity;
    private bool _canMove = false;

    private void Start()
    {
        _velocity = Vector3.zero;
    }
    public void ResetMovement()
    {
        _velocity = Vector3.zero;
        _touchingGround = false;
        _touchingRightWall = false;
        _touchingLeftWall = false;
        _currentGravity = Gravity;
        _canMove = false;
    }
    void Update()
    {
        if (_canMove) { HandleInput(); }

        _touchingGround = CheckCollision(Vector3.down);
        _canMove = _canMove || _touchingGround;
        if (_touchingGround == false) { ApplyGravity(); }
        _touchingRightWall = CheckCollision(Vector3.right);
        _touchingLeftWall = CheckCollision(Vector3.left);
        CheckCollision(Vector3.up);

        ApplyHorizontalTargetResistance();

        _velocity.x = Mathf.Clamp(_velocity.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
        _velocity.y = Mathf.Clamp(_velocity.y, MaxVerticalSpeed.x, MaxVerticalSpeed.y);
        transform.position += _velocity * Time.deltaTime;


    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndJump();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _lastTimeRightKeyDown = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _lastTimeLeftKeyDown = Time.time;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (_touchingGround || !_touchingLeftWall || Time.time - _lastTimeRightKeyDown >= WallStickTime)
            {
                MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_touchingGround || !_touchingRightWall || Time.time - _lastTimeLeftKeyDown >= WallStickTime)
            {
                MoveLeft();
            }
        }
        else if (_velocity.x > 0)
        {
            _velocity.x -= Mathf.Min(_velocity.x, _currentHorizontalFriction);
        }
        else
        {
            _velocity.x += Mathf.Min(-_velocity.x, _currentHorizontalFriction);
        }
    }

    private void ApplyHorizontalTargetResistance()
    {
        if (Mathf.Abs(_velocity.x) > TargetHorizontalSpeed) 
        {
            if (_velocity.x < 0) { _velocity.x += _currentHorizontalAcceleration; }
            else { _velocity.x -= _currentHorizontalAcceleration; }
        }
    }
    private void MoveRight()
    {
        _velocity.x += _currentHorizontalAcceleration;
    }
    private void MoveLeft()
    {
        _velocity.x -= _currentHorizontalAcceleration;
    }

    private void TryJump()
    {
        if (_touchingGround) { StartJump(new Vector3(0f, JumpForce, 0f)); }
        else if (_touchingRightWall) { StartJump(new Vector3(-WallJumpForce.x, WallJumpForce.y, 0f)); }
        else if (_touchingLeftWall) { StartJump(new Vector3(WallJumpForce.x, WallJumpForce.y, 0f)); }
    }
    private void StartJump(Vector3 force)
    {
        _currentGravity = 0f;
        _velocity.y = 0;
        _velocity += force;
    }
    private void EndJump()
    {
        _currentGravity = Gravity;
    }
    private void ApplyGravity()
    {
        _currentGravity -= Time.deltaTime * GravityAcceleration;
        _currentGravity = Mathf.Max(_currentGravity, Gravity);
        _velocity.y += _touchingLeftWall || _touchingRightWall ? _currentGravity * Time.deltaTime * WallGravityDampener : _currentGravity * Time.deltaTime;
    }

    private bool CheckCollision(Vector3 direction, int count = 3)
    {
        Vector3 maskedVelocity = _velocity.Mul(direction.Abs()).normalized;
        bool shouldHaltVelocity = Mathf.Approximately((maskedVelocity + direction).magnitude, 2f);

        float playerHeight = 0.7f;
        float playerWidth = 0.7f;
        float raycastOffset = 0.5f;
        float raycastLength = playerHeight - raycastOffset;
        float touchingGroundBuffer = 0.02f; //The raycast will go this distance further to see if the player is touching the ground. But if the raycast only touches this extra distance, it won't affect velocity.

        Vector3 perpendicularDirection = new Vector3(direction.y, direction.x, 0f);
        bool colliding = false;
        Vector3 raycastStartPos = transform.position + ((playerHeight / 2f) - raycastOffset) * -direction;
        raycastStartPos -= playerWidth / 2f * perpendicularDirection;
        float stepLength = playerWidth / (float)(count + 1);

        for (int i = 0; i < count; i++)
        {
            raycastStartPos += stepLength * perpendicularDirection;
            if (Physics.Raycast(raycastStartPos, transform.TransformDirection(direction), out RaycastHit hit, raycastLength + touchingGroundBuffer, GroundLayerMask))
            {
                if (hit.distance < raycastLength && shouldHaltVelocity)
                {
                    transform.position += (raycastLength - hit.distance) * -direction;
                    _velocity = _velocity.Mul(new Vector3(direction.y, direction.x, direction.z).Abs());
                }
                Debug.DrawRay(raycastStartPos, transform.TransformDirection(direction) * hit.distance, Color.red);
                colliding = true;
            }
            else
            {
                Debug.DrawRay(raycastStartPos, transform.TransformDirection(direction) * raycastLength, Color.green);
            }
        }
        return colliding;

    }

}
