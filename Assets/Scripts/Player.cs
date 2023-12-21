using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 40f;
    private float _horizontalMovement = 0f;
    private float _verticalMovement = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _walkingLeft, _walkingFront;
    private Dictionary<Item, bool> _itemsEquipped;
    private Dictionary<Item, Vector3> _itemsOffsets;
    private Dictionary<Item, RuntimeAnimatorController> _itemsAnimatorControllers;

    [SerializeField] private Inventory inventory;
    [Range(0, .3f)] [SerializeField] private float movementDampening = .05f;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Animator characterAnimator, clothesAnimator, headAnimator;

    [SerializeField] private RuntimeAnimatorController goldOutfitAnimatorController,
        blueOutfitAnimatorController,
        silverOutfitAnimatorController,
        silverHairAnimatorController,
        brownHatAnimatorController,
        witchHatAnimatorController;

    public void Start() {
        Inventory.SellAction += Equip;

        _itemsEquipped = new Dictionary<Item, bool> {
            {Item.GoldOutfit, true},
            {Item.BlueOutfit, false},
            {Item.SilverOutfit, false},
            {Item.SilverHair, false},
            {Item.BrownHat, false},
            {Item.WitchHat, false}
        };

        _itemsOffsets = new Dictionary<Item, Vector3> {
            {Item.GoldOutfit, new Vector3(0.252000004f, -0.101000004f, 2f)},
            {Item.BlueOutfit, new Vector3(0.252000004f, -0.128000006f, 2f)},
            {Item.SilverOutfit, new Vector3(0.252000004f, -0.101000004f, 2f)},
            {Item.SilverHair, new Vector3(0.250999987f, 0.0439999998f, 2f)},
            {Item.BrownHat, new Vector3(0.250999987f, 0.00600000005f, 2f)},
            {Item.WitchHat, new Vector3(0.250999987f, 0.0439999998f, 2f)}
        };

        _itemsAnimatorControllers = new Dictionary<Item, RuntimeAnimatorController > {
            {Item.GoldOutfit, goldOutfitAnimatorController},
            {Item.BlueOutfit, blueOutfitAnimatorController},
            {Item.SilverOutfit, silverOutfitAnimatorController},
            {Item.SilverHair, silverHairAnimatorController},
            {Item.BrownHat, brownHatAnimatorController},
            {Item.WitchHat, witchHatAnimatorController}
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

    public void Equip(int value) {
        Item item = (Item) value;
        _itemsEquipped[item] = !_itemsEquipped[item];

        if (inventory.itemsOwned[item] == false) {
            _itemsEquipped[item] = false;
        }

        bool isClothing = item <= Item.SilverOutfit;

        if (_itemsEquipped[item] == true) {
            if (isClothing) {
                clothesAnimator.gameObject.SetActive(true);
                clothesAnimator.runtimeAnimatorController = _itemsAnimatorControllers[item];
                clothesAnimator.transform.localPosition = _itemsOffsets[item];
            }
            else {
                headAnimator.gameObject.SetActive(true);
                headAnimator.runtimeAnimatorController = _itemsAnimatorControllers[item];
                headAnimator.transform.localPosition = _itemsOffsets[item];
            }
        }
        else {
            if (isClothing) {
                clothesAnimator.gameObject.SetActive(false);
            }
            else {
                headAnimator.gameObject.SetActive(false);
            }
        }
    }
}