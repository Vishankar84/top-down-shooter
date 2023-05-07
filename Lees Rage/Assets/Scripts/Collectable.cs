using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum ItemType { Paper, PlatformButton, Health }
    [SerializeField] private ItemType itemtype;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            if (itemtype == ItemType.Health && PlayerMovement.Instance.playerHealth < PlayerMovement.Instance.playerHealthMax)
            {
                PlayerMovement.Instance.playerHealth += 10;
            }

            PlayerMovement.Instance.UpdateUI();

            Destroy(gameObject);
        }
    }
}
