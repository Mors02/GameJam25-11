using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject StatUpgradeUI;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
                Resume();
            else if (!GameIsPaused)
                Pause();


        }
    }

    public void Pause()
    {
        // GameManager.Instance.SetIsUIVisible(true);
        //pauseMenuUI.SetActive(true);
        //overlayUI.SetActive(false);

        StatUpgradeUI.SetActive(true);

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

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        // pauseAnimator.SetBool("Open", false);
        Time.timeScale = 1f;
        StatUpgradeUI.SetActive(false);
        GameIsPaused = false;

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
