using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private AudioClip upgradeAvailableSound;
    [SerializeField] private float upgradeAvailableSoundVol;
    [SerializeField] private AudioClip upgradedSound;
    [SerializeField] private float upgradedSoundVol;


    [Header("Player/Weapon Stats")]
    public float playerSpeed  = 5f;
    public float bulletSpeed  = 50f;
    public float rangedWeaponSpeed  = 25f;
    public int   bulletDamage = 10;

    public int totalKills;
    public int totalScore = 0;

    public int currentWave = 1;

    [HideInInspector]
    public int maxWaves = 5;

    public bool gameOver = false;
    public bool upgradeAvailable = false;

    private int highestScore;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if(gameOver)
        {
            if(totalScore > highestScore)
            {
                highestScore = totalScore;
            }

            StartCoroutine(nameof(ReloadScene), 2f);
        }
        else
        {
            UpdateTextUI();
            CheckForPlayerUpgrade();
        }
    }

    private void CheckForPlayerUpgrade()
    {
        if(totalKills >= currentWave * 10)
        {
            if(currentWave < maxWaves)
            {
                PlayerMovement.Instance.sfxAudioSource.PlayOneShot(upgradeAvailableSound, upgradeAvailableSoundVol);
                upgradeButton.SetActive(true);
                upgradeAvailable = true;
            }
        }
    }

    public void UpgradePlayer()
    {
        playerSpeed  += 1;
        bulletDamage += 2;
        bulletSpeed  += 10;
        PlayerMovement.Instance.sfxAudioSource.PlayOneShot(upgradedSound, upgradedSoundVol);
        CameraShake.Instance.ShakeCamera(5f, 0.2f);
        PlayerMovement.Instance.StartCoroutine("PlayerUpgradeFlash", 0.02f);
        upgradeButton.SetActive(false);
        upgradeAvailable = false;
    }

    IEnumerator ReloadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Scene1");
    }

    public void UpdateTextUI()
    {
        scoreText.text = "Score - " + totalScore;
        waveText.text = "Wave " + currentWave + "/" + maxWaves;
    }

    public void ResetKills()
    {
        totalKills = 0;
    }
}
