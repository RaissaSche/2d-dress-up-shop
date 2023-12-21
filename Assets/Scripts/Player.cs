using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 40f;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _walkingLeft, _walkingFront;
    private Dictionary<string, bool> _itemsEquipped;

    [Range(0, .3f)] [SerializeField] private float movementDampening = .05f;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Animator characterAnimator, clothesAnimator, headAnimator;

    [SerializeField] private AnimatorController goldOutfitAnimator,
        blueOutfitAnimator,
        silverOutfitAnimator,
        silverHairAnimator,
        brownHatAnimator,
        witchHatAnimator;

    public void Start() {
        _itemsEquipped = new Dictionary<string, bool> {
            {"GoldOutfit", true},
            {"BlueOutfit", false},
            {"SilverOutfit", false},
            {"SilverHair", false},
            {"BrownHat", false},
            {"WitchHat", false}
        };
    }

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
            headAnimator.SetTrigger("IdleLeft");
        }
        else if (_walkingLeft == false) {
            characterAnimator.SetTrigger("IdleRight");
            clothesAnimator.SetTrigger("IdleRight");
            headAnimator.SetTrigger("IdleRight");
        }

        if (_walkingFront == true) {
            characterAnimator.SetTrigger("IdleFront");
            clothesAnimator.SetTrigger("IdleFront");
            headAnimator.SetTrigger("IdleFront");
        }
        else {
            characterAnimator.SetTrigger("IdleBack");
            clothesAnimator.SetTrigger("IdleBack");
            headAnimator.SetTrigger("IdleBack");
        }
    }

    private void SetFrontAnimation() {
        characterAnimator.SetTrigger("Front");
        clothesAnimator.SetTrigger("Front");
        headAnimator.SetTrigger("Front");
    }

    private void SetBackAnimation() {
        characterAnimator.SetTrigger("Back");
        clothesAnimator.SetTrigger("Back");
        headAnimator.SetTrigger("Back");
    }

    private void SetRightAnimation() {
        characterAnimator.SetTrigger("Right");
        clothesAnimator.SetTrigger("Right");
        headAnimator.SetTrigger("Right");
    }

    private void SetLeftAnimation() {
        characterAnimator.SetTrigger("Left");
        clothesAnimator.SetTrigger("Left");
        headAnimator.SetTrigger("Left");
    }

    public void Equip(int item) {
        Item enumItem = (Item) item;

        switch (enumItem) {
            case Item.GoldOutfit:
                _itemsEquipped["GoldOutfit"] = !_itemsEquipped["GoldOutfit"];
                if (_itemsEquipped["GoldOutfit"] == true) {
                    clothesAnimator.gameObject.SetActive(true);
                    clothesAnimator.runtimeAnimatorController = goldOutfitAnimator;
                    clothesAnimator.transform.localPosition = new Vector3((float) 0.252000004, (float) -0.101000004, 2);
                }
                else {
                    clothesAnimator.gameObject.SetActive(false);
                }

                break;
            case Item.BlueOutfit:
                _itemsEquipped["BlueOutfit"] = !_itemsEquipped["BlueOutfit"];
                if (_itemsEquipped["BlueOutfit"] == true) {
                    clothesAnimator.gameObject.SetActive(true);
                    clothesAnimator.runtimeAnimatorController = blueOutfitAnimator;
                    clothesAnimator.transform.localPosition = new Vector3((float) 0.252000004, (float) -0.128000006, 2);
                }
                else {
                    clothesAnimator.gameObject.SetActive(false);
                }

                break;
            case Item.SilverOutfit:
                _itemsEquipped["SilverOutfit"] = !_itemsEquipped["SilverOutfit"];
                if (_itemsEquipped["SilverOutfit"] == true) {
                    clothesAnimator.gameObject.SetActive(true);
                    clothesAnimator.runtimeAnimatorController = silverOutfitAnimator;
                    clothesAnimator.transform.localPosition = new Vector3((float) 0.252000004, (float) -0.101000004, 2);
                }
                else {
                    clothesAnimator.gameObject.SetActive(false);
                }

                break;
            case Item.SilverHair:
                _itemsEquipped["SilverHair"] = !_itemsEquipped["SilverHair"];
                if (_itemsEquipped["SilverHair"] == true) {
                    headAnimator.gameObject.SetActive(true);
                    headAnimator.runtimeAnimatorController = silverHairAnimator;
                    headAnimator.transform.localPosition = new Vector3((float) 0.250999987, (float) 0.0439999998, 2);
                }
                else {
                    headAnimator.gameObject.SetActive(false);
                }

                break;
            case Item.BrownHat:
                _itemsEquipped["BrownHat"] = !_itemsEquipped["BrownHat"];
                if (_itemsEquipped["BrownHat"] == true) {
                    headAnimator.gameObject.SetActive(true);
                    headAnimator.runtimeAnimatorController = brownHatAnimator;
                    headAnimator.transform.localPosition = new Vector3((float) 0.250999987, (float) 0.00600000005, 2);
                }
                else {
                    headAnimator.gameObject.SetActive(false);
                }

                break;
            case Item.WitchHat:
                _itemsEquipped["WitchHat"] = !_itemsEquipped["WitchHat"];
                if (_itemsEquipped["WitchHat"] == true) {
                    headAnimator.gameObject.SetActive(true);
                    headAnimator.runtimeAnimatorController = witchHatAnimator;
                    headAnimator.transform.localPosition = new Vector3((float) 0.250999987, (float) 0.0439999998, 2);
                }
                else {
                    headAnimator.gameObject.SetActive(false);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}