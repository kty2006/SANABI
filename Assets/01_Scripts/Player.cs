using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Base
    BoxCollider2D col;
    Rigidbody2D rigid;
    Animator anim;

    #region Keycode
    public static KeyCode rightKey = KeyCode.D;
    public static KeyCode leftKey = KeyCode.A;
    public static KeyCode jumpKey = KeyCode.Space;
    #endregion

    public Rigidbody2D Rigid() { return rigid; }

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    #endregion

    void Update()
    {
        MoveControl();
        JumpControl();
    }

    void FixedUpdate()
    {
        CheckPlatform();

        Move();
        JumpMovement();
    }

    #region Move
    [SerializeField] float moveSpeed;
    Vector2 moveDirection;

    void MoveControl() //이동 조작 제어
    {
        if (isDie) return;

        moveDirection = Vector2.zero;

        if (Input.GetKey(rightKey))
        {
            moveDirection.x = 1f;
            transform.eulerAngles = Vector3.zero; //오른쪽을 봄
        }
        else if (Input.GetKey(leftKey))
        {
            moveDirection.x = -1f;
            transform.eulerAngles = new Vector3(0f, 180f, 0f); //왼쪽을 봄
        }

        anim.SetBool("Is Walking", moveDirection.x != 0f);
    }

    void Move() //이동
    {
        if (isDie) return;
        rigid.position += moveSpeed * Time.fixedDeltaTime * moveDirection;
    }
    #endregion
    #region Jump
    [SerializeField] float jumpForce = 10f;
    bool jumpKeyPressed;

    const float jumpBuffer = 0.1f;
    float currentJumpbuffer = 0f;
    const float coyoteTime = 0.1f;
    float currentCoyoteTime = 0f;

    void JumpControl() //점프 조작 제어
    {
        if (isDie) return;

        if (Input.GetKeyDown(jumpKey)) currentJumpbuffer = jumpBuffer; //점프 버퍼 활성화

        jumpKeyPressed = Input.GetKey(jumpKey);

        anim.SetBool("Is Jumping", !isOnPlatform);
        if (!isOnPlatform) anim.SetFloat("Jump Velocity", rigid.linearVelocity.y);
    }

    void JumpMovement()
    {
        if (isDie) return;

        if (isOnPlatform) currentCoyoteTime = coyoteTime; //코요테 타임 초기화

        if (jumpKeyPressed && currentJumpbuffer > 0f && currentCoyoteTime > 0f)
        {
            Jump();
        }

        currentJumpbuffer -= Time.fixedDeltaTime;
        currentCoyoteTime -= Time.fixedDeltaTime;
    }

    public void Jump() //점프
    {
        if (isDie) return;

        currentJumpbuffer = 0f;
        currentCoyoteTime = 0f;

        rigid.gravityScale = 3f;

        rigid.linearVelocity *= Vector2.right;
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    #endregion

    const float castLength = 0.1f;
    public bool isOnPlatform { get; private set; }
    bool isDie = false;

    void CheckPlatform()
    {
        Vector2 checkerCenter = (Vector2)transform.position + col.offset + Vector2.down * (col.size.y / 2f + castLength / 2f);
        Vector2 checkerSize = new Vector2(col.size.x, castLength);
        isOnPlatform = Physics2D.OverlapBox(checkerCenter, checkerSize, 0f, LayerMask.GetMask("Platform"));
    }

    public void OnDie()
    {
        if (isDie) return;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        rigid.gravityScale = 0f;
        rigid.linearVelocity = Vector2.zero;

        isDie = true;
        yield return null;

        isDie = false;
    }
}
