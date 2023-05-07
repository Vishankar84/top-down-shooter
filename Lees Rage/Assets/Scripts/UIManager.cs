using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Image healthBar;

    private void Start()
    {
        Instance = this;
    }

    public void UpdateHealthBar()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(300 * (PlayerMovement.Instance.playerHealth / PlayerMovement.Instance.playerHealthMax),
            healthBar.rectTransform.sizeDelta.y);
    }
}
