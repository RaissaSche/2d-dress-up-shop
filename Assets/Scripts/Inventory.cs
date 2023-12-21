using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item {
    GoldOutfit,
    BlueOutfit,
    SilverOutfit,
    SilverHair,
    BrownHat,
    WitchHat
};

public class Inventory : MonoBehaviour {
    
    public static event Action<int> SellAction;
    public Dictionary<Item, bool> itemsOwned;
    private int _money = 2000;
    [SerializeField] private Text currentMoney;

    [SerializeField] private GameObject goldOutfitInventory,
        blueOutfitInventory,
        silverOutfitInventory,
        silverHairInventory,
        brownHatInventory,
        witchHatInventory;

    [SerializeField]
    private Button goldOutfitBuy, blueOutfitBuy, silverOutfitBuy, silverHairBuy, brownHatBuy, witchHatBuy;

    [SerializeField]
    private Button goldOutfitSell, blueOutfitSell, silverOutfitSell, silverHairSell, brownHatSell, witchHatSell;

    public void Start() {
        itemsOwned = new Dictionary<Item, bool> {
            {Item.GoldOutfit, false},
            {Item.BlueOutfit, false},
            {Item.SilverOutfit, false},
            {Item.SilverHair, false},
            {Item.BrownHat, false},
            {Item.WitchHat, false}
        };
        
        Buy(0);
    }

    public void Buy(int item) {
        Item enumItem = (Item) item;

        switch (enumItem) {
            case Item.GoldOutfit:
                if (_money >= 500) _money -= 500;

                goldOutfitInventory.SetActive(true);
                goldOutfitBuy.interactable = false;
                goldOutfitSell.interactable = true;
                break;
            case Item.BlueOutfit:
                if (_money >= 500) _money -= 500;

                blueOutfitInventory.SetActive(true);
                blueOutfitBuy.interactable = false;
                blueOutfitSell.interactable = true;
                break;
            case Item.SilverOutfit:
                if (_money >= 500) _money -= 500;

                silverOutfitInventory.SetActive(true);
                silverOutfitBuy.interactable = false;
                silverOutfitSell.interactable = true;
                break;
            case Item.SilverHair:
                if (_money >= 250) _money -= 250;

                silverHairInventory.SetActive(true);
                silverHairBuy.interactable = false;
                silverHairSell.interactable = true;
                break;
            case Item.BrownHat:
                if (_money >= 250) _money -= 250;

                brownHatInventory.SetActive(true);
                brownHatBuy.interactable = false;
                brownHatSell.interactable = true;
                break;
            case Item.WitchHat:
                if (_money >= 250) _money -= 250;

                witchHatInventory.SetActive(true);
                witchHatBuy.interactable = false;
                witchHatSell.interactable = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(item), item, null);
        }
        itemsOwned[enumItem] = true;
        currentMoney.text = "Munny: " + _money;
    }

    public void Sell(int item) {
        Item enumItem = (Item) item;

        switch (enumItem) {
            case Item.GoldOutfit:
                _money += 500;

                goldOutfitInventory.SetActive(false);
                goldOutfitBuy.interactable = true;
                goldOutfitSell.interactable = false;
                break;
            case Item.BlueOutfit:
                _money += 500;

                blueOutfitInventory.SetActive(false);
                blueOutfitBuy.interactable = true;
                blueOutfitSell.interactable = false;
                break;
            case Item.SilverOutfit:
                _money += 500;

                silverOutfitInventory.SetActive(false);
                silverOutfitBuy.interactable = true;
                silverOutfitSell.interactable = false;
                break;
            case Item.SilverHair:
                _money += 250;

                silverHairInventory.SetActive(false);
                silverHairBuy.interactable = true;
                silverHairSell.interactable = false;
                break;
            case Item.BrownHat:
                _money += 250;

                brownHatInventory.SetActive(false);
                brownHatBuy.interactable = true;
                brownHatSell.interactable = false;
                break;
            case Item.WitchHat:
                _money += 250;

                witchHatInventory.SetActive(false);
                witchHatBuy.interactable = true;
                witchHatSell.interactable = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(item), item, null);
        }

        itemsOwned[enumItem] = false;
        currentMoney.text = "Munny: " + _money;
        SellAction?.Invoke(item);
    }
}