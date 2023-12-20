using UnityEngine;

public class Shopkeeper : MonoBehaviour {
    [SerializeField] private GameObject welcome;

    private void OnTriggerEnter2D(Collider2D other) {
        welcome.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        welcome.SetActive(false);
    }
}