using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 40f;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _facingLeft = true;

    [Range(0, .3f)] [SerializeField] private float movementDampening = .05f;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Animator animator;

    private void Update() {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        Move(_horizontalMovement * moveSpeed * Time.fixedDeltaTime, false);
        Move(_verticalMovement * moveSpeed * Time.fixedDeltaTime, true);

        if (animator != null) {
            if (_horizontalMovement is -1 or 1) {
                animator.SetTrigger("Side");

                switch (_horizontalMovement) {
                    case > 0 when !_facingLeft:
                    case < 0 when _facingLeft:
                        Flip();
                        break;
                }
            }

            else if (_verticalMovement is 1) {
                animator.SetTrigger("Back");
            }
            else if (_verticalMovement is -1) {
                animator.SetTrigger("Front");
            }
            else if (_verticalMovement is 0 || _horizontalMovement is 0) {
                animator.SetTrigger("Idle");
            }
        }
    }

    private void Move(float movement, bool isVertical) {
        Vector2 playerVelocity = playerRigidbody2D.velocity;

        Vector2 targetVelocity = isVertical switch {
            true => new Vector2(playerVelocity.x, movement * 10f),
            false => new Vector2(movement * 10f, playerVelocity.y)
        };

        playerRigidbody2D.velocity =
            Vector3.SmoothDamp(playerVelocity, targetVelocity, ref _velocity, movementDampening);
    }

    private void Flip() {
        _facingLeft = !_facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}