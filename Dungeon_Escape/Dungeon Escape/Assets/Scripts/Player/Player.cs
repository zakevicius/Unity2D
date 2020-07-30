using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _speed = 3f;

    private Rigidbody2D _rigid;
    private bool _shouldResetJump = false;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private bool _grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    private void Movement()
    {
        // Horizontal Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        Flip(moveX);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        _rigid.velocity = new Vector2(moveX * _speed, _rigid.velocity.y);
        _playerAnimation.Move(moveX);
    }

    private void Jump()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(ResetJumpRoutine());
        _playerAnimation.Jump(true);
    }

    private void Flip(float moveX)
    {
        Vector3 newPos = _swordArcSprite.transform.localPosition;

        if (moveX < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;
            newPos.x = -1.01f;

        }
        else if (moveX > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;
            newPos.x = 1.01f;
        }

        _swordArcSprite.transform.localPosition = newPos;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, _layer);
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.green);

        if (hit.collider != null && _shouldResetJump == false)
        {
            _playerAnimation.Jump(false);
            return true;
        }
        
        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _shouldResetJump = true;
        yield return new WaitForSeconds(0.1f);
        _shouldResetJump = false;
    }
}
