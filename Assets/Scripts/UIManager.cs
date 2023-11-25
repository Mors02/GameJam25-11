using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject StatUpgradeUI;
    public GameObject HealthBarUI;
    public GameObject DeathScreenUI;
    [SerializeField]
    TMP_Text button1Text;

    [SerializeField]
    TMP_Text button2Text;

    [SerializeField]
    TMP_Text button3Text;

    [SerializeField]
    Button Button1;
    [SerializeField]
    Button Button2;
    [SerializeField]
    Button Button3;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    Image sprite1;
    [SerializeField]
    Image sprite2;
    [SerializeField]
    Image sprite3;

    Stats stats;

    bool GameIsPaused = false;

    void Start()
    {
        this.stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

    }

    public void DecreaseHealth()
    {
        this.healthBar.value = stats.GetStat(PowerUpTypes.health);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
                Resume();
            else if (!GameIsPaused)
                Pause();


        }*/
    }

    public void Pause()
    {
        // GameManager.Instance.SetIsUIVisible(true);
        //pauseMenuUI.SetActive(true);
        //overlayUI.SetActive(false);

        StatUpgradeUI.SetActive(true);
        HealthBarUI.SetActive(false);

        Powerup upgrade1 = new Powerup(stats);
        Powerup upgrade2 = new Powerup(stats);
        Powerup upgrade3 = new Powerup(stats);
        upgrade1.RandomizeSetup(new int[] {});
        upgrade2.RandomizeSetup(new int[] { (int)upgrade1.type });
        upgrade3.RandomizeSetup(new int[] { (int)upgrade2.type, (int)upgrade1.type });

        this.button1Text.text = upgrade1.UpgradeName;
        this.button2Text.text = upgrade2.UpgradeName;
        this.button3Text.text = upgrade3.UpgradeName;

        Button1.onClick.AddListener(delegate { this.stats.UpdateStat(upgrade1); });
        Button2.onClick.AddListener(delegate { this.stats.UpdateStat(upgrade2); });
        Button3.onClick.AddListener(delegate { this.stats.UpdateStat(upgrade3); });

        sprite1.sprite = upgrade1.sprite;
        sprite2.sprite = upgrade2.sprite;
        sprite3.sprite = upgrade3.sprite;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        // pauseAnimator.SetBool("Open", false);
        Time.timeScale = 1f;
        StatUpgradeUI.SetActive(false);
        HealthBarUI.SetActive(true);
        GameIsPaused = false;

    }

    public void Death()
    {
        HealthBarUI.SetActive(false);
        DeathScreenUI.SetActive(true);
        
    }

    int playAgainSceneId = 1;

    public void Restart()
    {
       SceneManager.LoadScene(playAgainSceneId);   
    }

    public void RemoveAllListener()
    {
        Button1.onClick.RemoveAllListeners();
        Button1.onClick.AddListener(Resume);
        Button1.onClick.AddListener(RemoveAllListener);

        Button2.onClick.RemoveAllListeners();
        Button2.onClick.AddListener(Resume);
        Button2.onClick.AddListener(RemoveAllListener);

        Button3.onClick.RemoveAllListeners();
        Button3.onClick.AddListener(Resume);
        Button2.onClick.AddListener(RemoveAllListener);

    }

}

