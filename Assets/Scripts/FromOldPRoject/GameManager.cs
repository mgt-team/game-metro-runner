using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Player player;
    public float playerSpeed;

    public MapGenerator mapGenerator;
    public EnemyGenerator enemyGenerator;
    public UIManager uiManager;
    public SoundManager soundManager;

    public GameObject gameCamera;
    public float cameraSpeed;
    public float scoreEarnTime;
    public int scoreForKill = 0;

    private int killsCount = 0;

    public int scoresNeedToUpHardLevel = 10;
    public UpLevelProperties upLevelProperties;
    
    [System.Serializable]
    public class UpLevelProperties
    {
        public float playerShiftYDelta;

        // GameManager
        public float cameraSpeedDelta;
        public float scoreEarnTimeDelta;
        public float scoresNeedToUpHardLevelDelta;
        // EnemyGenerator
        public float enemyPropabilityDelta;
        public float enemyGenerateCooldownDelta;
        // Enemy
        public float enemySpeedDelta;
        public float enemyMassDelta;
        // MapGenerator
        public float obstacleRowProbabilityDelta;
        public float obstacleProbabilityDelta;

    }

    private int score;
    private float scoreCoolDown = 0;
    private Vector2 zoneSize;
    private Vector2 zoneCenter;
    private float playerRadius;

    private Camera cameraObject;

    private GameStateEnum gameState;

    void Start () {
        Time.timeScale = 0;
        gameState = GameStateEnum.Pause;
        uiManager.ShowMenu(uiManager.tutorialMenu);
        uiManager.HideMenu(uiManager.gameElements);
        soundManager.SetStopFlag(true);

        cameraObject = gameCamera.GetComponent<Camera>();

        enemyGenerator.SetCamera(cameraObject);
        enemyGenerator.SetCameraRadius(Vector3.Distance(cameraObject.transform.position,
            cameraObject.ScreenToWorldPoint(new Vector2(cameraObject.pixelWidth, cameraObject.pixelHeight))));

        zoneSize = mapGenerator.zonePrefab.GetComponent<BoxCollider2D>().size;
        zoneCenter = mapGenerator.zonePrefab.transform.position;

        playerRadius = player.GetComponent<CircleCollider2D>().radius;
    }
	
	void Update () {
        CheckOtherActions();

        if (!gameState.Equals(GameStateEnum.Play))
            return;

        if (!CommonHandler.IsObjectInCamera(cameraObject, gameCamera.transform, player.transform.position))
        {
            GameOver();
        }

        //uiManager.CheckForBackLine(player.transform.position);


        MoveCamera();
        MovePlayer();

        CheckScore();
    }

    public bool IsObjectDownerCamera(Vector2 position)
    {
        return CommonHandler.IsObjectDownerCamera(cameraObject, gameCamera.transform, position);
    }

    public void AddScoreForKill()
    {
        //AddCombo();
        AddScore(scoreForKill);
        uiManager.ChangeKills(++killsCount);
    }

    bool CheckTap()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 velocity = new Vector2(horizontal, vertical);

        player.SetVelocity(velocity);

        return velocity.Equals(Vector2.zero);
    }
    
    void CheckOtherActions()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState.Equals(GameStateEnum.Pause))
            {
                Play();
            }
            else if(gameState.Equals(GameStateEnum.Play))
            {
                Pause();
            }
            else if (gameState.Equals(GameStateEnum.GameOver))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    void AddScore(int value)
    {
        score += value;
        uiManager.ChangeScore(score);
    }

    void CheckScore()
    {
        if(scoreCoolDown <= 0)
        {
            AddScore(1);
            scoreCoolDown = scoreEarnTime;
        }

        scoreCoolDown -= Time.deltaTime;

        if(score > scoresNeedToUpHardLevel)
        {
            UpHardLevel();
        }
    }

    public void StartGame()
    {
        uiManager.tutorialMenu.menuObject.SetActive(false);
        soundManager.SetStopFlag(false);
        Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpHardLevel()
    {
        Debug.Log("Up level");

        player.shiftPower.y += upLevelProperties.playerShiftYDelta;

        cameraSpeed += upLevelProperties.cameraSpeedDelta;
        scoreEarnTime -= upLevelProperties.scoreEarnTimeDelta;
        scoresNeedToUpHardLevel = (int) (scoresNeedToUpHardLevel * upLevelProperties.scoresNeedToUpHardLevelDelta);

        enemyGenerator.enemyProbability += upLevelProperties.enemyPropabilityDelta;
        enemyGenerator.generateCooldown -= upLevelProperties.enemyGenerateCooldownDelta;

        enemyGenerator.enemySpeedProperties += upLevelProperties.enemySpeedDelta;
        enemyGenerator.enemyMassProperties += upLevelProperties.enemyMassDelta;
        player.enemyMass += upLevelProperties.enemyMassDelta;

        mapGenerator.zoneProperties.obstacleProbability += upLevelProperties.obstacleProbabilityDelta;
        mapGenerator.zoneProperties.obstacleRowProbability += upLevelProperties.obstacleRowProbabilityDelta;
    }

    // Move camera up with cameraSpeed
    void MoveCamera()
    {
        gameCamera.transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
       
    }

    // Move camera up with cameraSpeed
    void MovePlayer()
    {
        CheckTap();

        player.transform.position += Vector3.up * playerSpeed * Time.deltaTime; 

        // Check for exit from horizontal bounds
        Vector2 campPosition = new Vector2(Mathf.Clamp(player.transform.position.x,
            zoneCenter.x - zoneSize.x / 2 + playerRadius, zoneCenter.x + zoneSize.x / 2 - playerRadius),
            player.transform.position.y);
        player.transform.position = campPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Zone)))
        {
            mapGenerator.GenerateNextZone();
        }
    }

    void Play()
    {
        uiManager.ChangeState(true);
        Time.timeScale = 1;
        gameState = GameStateEnum.Play;
    }

    void Pause()
    {
        Time.timeScale = 0;
        gameState = GameStateEnum.Pause;

        uiManager.ChangeState(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameState = GameStateEnum.GameOver;
        uiManager.ShowMenu(uiManager.gameOverMenu);
        soundManager.StopMusic();
        soundManager.PlayGameOver();
    }
}
