using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;

    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;

    bool facingRight = true;

    bool isGrounded = false;
    [SerializeField]
    Transform groundCheckPosition;
    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    LayerMask groundCheckLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }
        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
    }

    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }
    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));
    }
    void OnGroundCheck()
    {
        isGrounded= Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);
    }
}
