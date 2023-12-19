using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 40f;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    private Vector3 _velocity = Vector3.zero;

    [Range(0, .3f)] [SerializeField] private float movementDampening = .05f;
    [SerializeField] private Rigidbody2D playerRigidbody2D;

    private void Update() {
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
        _verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void FixedUpdate() {
        Move(_horizontalMovement * Time.fixedDeltaTime, false);
        Move(_verticalMovement * Time.fixedDeltaTime, true);
    }

    private void Move(float movement, bool isVertical) {
        Vector2 playerVelocity = playerRigidbody2D.velocity;

        Vector3 targetVelocity = isVertical switch {
            true => new Vector2(playerVelocity.x, movement * 10f),
            false => new Vector2(movement * 10f, playerVelocity.y)
        };

        playerRigidbody2D.velocity =
            Vector3.SmoothDamp(playerVelocity, targetVelocity, ref _velocity, movementDampening);
    }
}