using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hpBar;
    public int maxHP;

    public GameObject winMenu;
    public GameObject loseMenu;

    public int weaponType = 0;
    
    public GameObject[] enemyList;
    public int[] enemySpawnChance;
    //public Texture2D crosshair;
    public float ballSpawnDelay = 1.5f;
    public GameObject ammoText;


    public float reloadTime = 0.5f;
    public float timeBetweenShots = 0.2f;
   

    public int MaxAmmo = 6;
    

    private bool notGameOver = true;
    private bool mouseClicked = false;
    private bool isReloading = false;
    private bool isDelaying = false;
    private Vector2 clickedPos;
    private float nextBallTime = 0.0f;
    private float timeLeftToReload;
    private float timeLeftOfDelay;

    [SerializeField] private AudioSource reloadSound;
    [SerializeField] private AudioSource shootSound;
    float pos = 9.2f;
    float dir = 1.0f;
    private int ammo;
    private int hp;

    public delegate void OnScoreAdded(float scoreToAdd);
    public static event OnScoreAdded onScoreAdded;

    void OnEnable()
    {
        DamageZoneScript.onDamagePlayer += DamagePlayer;
        ScoreManager.onLevelFinished += FinishLevel;
    }
    void OnDisable()
    {
        DamageZoneScript.onDamagePlayer -= DamagePlayer;
        ScoreManager.onLevelFinished -= FinishLevel;
    }


    void Start()
    {
        Time.timeScale = 1;
        ammo = MaxAmmo;
        hp = maxHP;
        // Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
        ammoText.GetComponent<TMPro.TextMeshProUGUI>().text = ammo + "/" + MaxAmmo;
        hpBar.GetComponent<TMPro.TextMeshProUGUI>().text = hp + "/" + maxHP;

        if (!StaticDataStorage.GetSoundsStatus())
        {
            gameObject.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<AudioListener>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (notGameOver && Time.timeScale !=0)
        {
            if (Time.time >= nextBallTime)
            {
                SpawnBall();
                nextBallTime = Time.time + ballSpawnDelay;
            }

            if (weaponType == 0)
            {
                mouseClicked = Input.GetMouseButtonDown(0);


                if (mouseClicked && !isReloading)
                {
                    shootSound.Play();
                    SetAmmo(GetAmmo() - 1);
                    StaticDataStorage.PlayerShot();
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                    {
                        hit.transform.gameObject.GetComponent<BallMove>().Damage(1);
                        if (hit.transform.gameObject.tag != "BossBall" && hit.transform.gameObject.tag != "IgnoreBall")
                            onScoreAdded?.Invoke(1);
                    }

                    if (GetAmmo() == 0)
                    {
                        isReloading = true;
                        reloadSound.Play();
                        timeLeftToReload = reloadTime;
                    }
                }
            }
            else if(weaponType == 1)
            {
                if (Input.GetMouseButtonDown(0)){ mouseClicked = true; };
                if (Input.GetMouseButtonUp(0)){ mouseClicked = false; };


                if (mouseClicked && !isReloading && !isDelaying)
                {
                    shootSound.Play();
                    StaticDataStorage.PlayerShot();
                    SetAmmo(GetAmmo() - 1);
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                    {
                        hit.transform.gameObject.GetComponent<BallMove>().Damage(1);
                        if (hit.transform.gameObject.tag != "BossBall")
                            onScoreAdded?.Invoke(1);
                    }
                    isDelaying = true;
                    timeLeftOfDelay = timeBetweenShots;

                    if (GetAmmo() == 0)
                    {
                        isReloading = true;
                        reloadSound.Play();
                        timeLeftToReload = reloadTime;
                    }
                }
            }

            if (isDelaying)
            {
                timeLeftOfDelay -= Time.deltaTime;
                if (timeLeftOfDelay <= 0)
                {
                    isDelaying = false;
                }
            }

            if (isReloading)
            {
                timeLeftToReload -= Time.deltaTime;
                if (timeLeftToReload <= 0)
                {
                    SetAmmo(MaxAmmo);
                    isReloading = false;
                }
            }

        }
    }

    public int GetAmmo() { return ammo; }

    public void SetAmmo(int ammo)
    {
        this.ammo = ammo;
        ammoText.GetComponent<TMPro.TextMeshProUGUI>().text = ammo+"/"+MaxAmmo;
    }

    public void SpawnBall()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), -7.0f, 0.0f);
        int ChancesToSpawn = 0;
        for (int i=0; i<enemySpawnChance.Length; i++)
        {
            ChancesToSpawn += enemySpawnChance[i];
        }
        int ChosenChanceSpawn = UnityEngine.Random.Range(0,ChancesToSpawn);
        int ChosenEnemySpawn = 0;
   


        while (ChosenChanceSpawn>0)
        {
            if (ChosenChanceSpawn <= enemySpawnChance[ChosenEnemySpawn]) { break; }
            ChosenChanceSpawn -= enemySpawnChance[ChosenEnemySpawn];
            ChosenEnemySpawn++;
        }


        GameObject newBall;
        newBall = Instantiate(enemyList[ChosenEnemySpawn], position, Quaternion.identity);
        newBall.transform.localEulerAngles = new Vector3(-90, 0, 0);
    }


    public void DamagePlayer(int damage)
    {
        hp = hp - damage;
        if (hp < 0) { hp = 0; };
        if (hp > maxHP) { hp = maxHP; };
        hpBar.GetComponent<TMPro.TextMeshProUGUI>().text = hp+"/"+maxHP;
 

       
        if (hp <= 0)
        {
            Time.timeScale = 0;
            notGameOver = false;
            loseMenu.SetActive(true);
            winMenu.SetActive(false);

        }
    }

    public void DamagePlayer()
    {
        hp = hp - 1;
        if (hp < 0) { hp = 0; };
        if (hp > maxHP) { hp = maxHP; };
        hpBar.GetComponent<TMPro.TextMeshProUGUI>().text = hp + "/" + maxHP;
        


        if (hp <= 0)
        {
            Time.timeScale = 0;
            notGameOver = false;
            loseMenu.SetActive(true);
            winMenu.SetActive(false);
        }
    }

    public void FinishLevel()
    {
        Time.timeScale = 0;
        notGameOver = false;
        loseMenu.SetActive(false);
        winMenu.SetActive(true);

        StaticDataStorage.SetLevelCompleteStatus((uint)int.Parse(SceneManager.GetActiveScene().name.Replace("Level", ""))-1, true);
        if (SceneManager.GetActiveScene().name == "Level2" && hp == maxHP)
        {
            StaticDataStorage.LevelIsBeaten();
        }

        Camera.main.GetComponent<SaveSerial>().SaveGame();

    }


}
