using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public SpriteRenderer weaponRenderer;
    public AudioSource sfxAudioSource;
    public Image playerHitFlash;
    public Image playerUpgradeFlash;

    public float playerHealth = 100;
    public float playerHealthMax = 100;

    private Rigidbody2D   rb;
    private Animator      anim;

    private Vector2 moveAmount;

    [Header("Sounds")]
    [SerializeField] private AudioClip playerDeath;

    void Start()
    {
        Instance = this;
        rb   = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            ClampPlayerPosition();

            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * GameManager.Instance.playerSpeed;

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            if (!PauseMenu.gameIsPaused)
            {
                RotateAccToMouse();
            }
        }
    }

    private void ClampPlayerPosition()
    {
        if(transform.position.y <= -21f) transform.position = new Vector2(transform.position.x, -21f);
        else if (transform.position.y >= 17.91f) transform.position = new Vector2(transform.position.x, 17.91f);

        if (transform.position.x <= -36.75f) transform.position = new Vector2(-36.75f, transform.position.y);
        else if (transform.position.x >= 36.75f) transform.position = new Vector2(36.75f, transform.position.y);
    }

    private void RotateAccToMouse()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 cross     = Vector3.Cross(Vector2.up, direction);

        if(cross.z > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            weaponRenderer.flipY    = true;
        }
        else if(cross.z < 0) 
        { 
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponRenderer.flipY    = false;
        }
    }

    private void FixedUpdate()
    {
        if(!GameManager.Instance.gameOver)
        {
            rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        }
    }

    public void takeDamage(int damage)
    {
        playerHealth -= damage;
        CameraShake.Instance.ShakeCamera(5f, 0.2f);
        StartCoroutine("PlayerHitFlash", 0.02f);
        sfxAudioSource.PlayOneShot(playerDeath, 0.7f);

        if (playerHealth <= 0 && !GameManager.Instance.gameOver)
        {
            GameManager.Instance.gameOver = true;
            sfxAudioSource.PlayOneShot(playerDeath, 1f);
            anim.SetTrigger("Died");
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.UpdateHealthBar();
    }

    IEnumerator PlayerHitFlash(float time)
    {
        playerHitFlash.rectTransform.sizeDelta = new Vector2(2500, 2100);
        yield return new WaitForSeconds(time);
        playerHitFlash.rectTransform.sizeDelta = Vector2.zero;
    }

    public IEnumerator PlayerUpgradeFlash(float time)
    {
        playerUpgradeFlash.rectTransform.sizeDelta = new Vector2(2500, 2100);
        yield return new WaitForSeconds(time);
        playerUpgradeFlash.rectTransform.sizeDelta = Vector2.zero;
    }
}
