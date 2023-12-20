using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 40f;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _walkingLeft, _walkingFront;

    [Range(0, .3f)] [SerializeField] private float movementDampening = .05f;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Animator characterAnimator, clothesAnimator, hairAnimator;

    private void Update() {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        Move(_horizontalMovement * moveSpeed * Time.fixedDeltaTime, false);
        Move(_verticalMovement * moveSpeed * Time.fixedDeltaTime, true);

        if (characterAnimator != null) {
            if (_horizontalMovement is 1) {
                SetRightAnimation();
                _walkingLeft = false;
            }
            else if (_horizontalMovement is -1) {
                SetLeftAnimation();
                _walkingLeft = true;
            }

            else if (_verticalMovement is 1) {
                SetBackAnimation();
                _walkingFront = false;
            }
            else if (_verticalMovement is -1) {
                SetFrontAnimation();
                _walkingFront = true;
            }
            else if (_verticalMovement is 0 || _horizontalMovement is 0) {
                SetIdleAnimation();
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

    private void SetIdleAnimation() {
        if (_walkingLeft == true) {
            characterAnimator.SetTrigger("IdleLeft");
            clothesAnimator.SetTrigger("IdleLeft");
            hairAnimator.SetTrigger("IdleLeft");
        }
        else if (_walkingLeft == false) {
            characterAnimator.SetTrigger("IdleRight");
            clothesAnimator.SetTrigger("IdleRight");
            hairAnimator.SetTrigger("IdleRight");
        }

        if (_walkingFront == true) {
            characterAnimator.SetTrigger("IdleFront");
            clothesAnimator.SetTrigger("IdleFront");
            hairAnimator.SetTrigger("IdleFront");
        }
        else {
            characterAnimator.SetTrigger("IdleBack");
            clothesAnimator.SetTrigger("IdleBack");
            hairAnimator.SetTrigger("IdleBack");
        }
    }

    private void SetFrontAnimation() {
        characterAnimator.SetTrigger("Front");
        clothesAnimator.SetTrigger("Front");
        hairAnimator.SetTrigger("Front");
    }

    private void SetBackAnimation() {
        characterAnimator.SetTrigger("Back");
        clothesAnimator.SetTrigger("Back");
        hairAnimator.SetTrigger("Back");
    }

    private void SetRightAnimation() {
        characterAnimator.SetTrigger("Right");
        clothesAnimator.SetTrigger("Right");
        hairAnimator.SetTrigger("Right");
    }

    private void SetLeftAnimation() {
        characterAnimator.SetTrigger("Left");
        clothesAnimator.SetTrigger("Left");
        hairAnimator.SetTrigger("Left");
    }
}