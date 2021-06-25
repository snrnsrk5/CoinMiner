using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{    
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }
    [SerializeField]
    private GameObject[] enemyPrefab = null; 
    [SerializeField]
    private GameObject linePrefab = null;
    [SerializeField]
    private float time = 10.5f;
    private float upPos=0;
    private float downPos=0;
    private float hard = 0;
    private float highMoney = 0;
    private int stage = 1;
    public Canvas canvas;
    private GameManager game;
    [SerializeField]
    private BossMove boss;
    [SerializeField]
    public EnemyMove enemyOver;
    [SerializeField]
    private PlayerSoundFaster soundFaster;
    [SerializeField]
    private float money=0;
    [SerializeField]
    private Text moneyText; 
    [SerializeField]
    private Text stageText; 
    [SerializeField]
    private Text highMoneyText; 
    [SerializeField]
    private AudioSource popAudio;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject stopButton;
    [SerializeField]
    private GameObject stop;
    public poolManager poolManager{get; private set;}
    GameObject bossSp;
    private bool isBoss = false;
    void Save()
    {
        PlayerPrefs.SetFloat("Best", highMoney);
    }
    void Load()
    {
        highMoney = PlayerPrefs.GetFloat("Best");
    }
    private void Awake(){
        Load();
        poolManager = FindObjectOfType<poolManager>();
        //enemyOver = GetComponent<EnemyMove>();
        Time.timeScale = 1;
        gameOver.SetActive(false);
        stopButton.SetActive(true);
        canvas = FindObjectOfType<Canvas>();
        MinPosition = new Vector2(-14f, 8f);
        MaxPosition = new Vector2(16f, 8f);
        StartCoroutine(Spawn());
        StartCoroutine(Times());
        StartCoroutine(SpawnLine());
        StartCoroutine(moneyup());
    }
    void Update(){
        moneyText.text = string.Format("GGM {0:N0}",money);
        stageText.text = string.Format("level {0}",stage);
        if(highMoney < money){
            highMoney = money;
            //highMoneyText.text = string.Format("GGM {0:N0}",highMoney);
            Save();
        }
    }
    public void StartButton(){
        SceneManager.LoadScene("GameOverScene");
        SceneManager.LoadScene("SampleScene");
    }
    public void MainButton(){
        SceneManager.LoadScene("GameOverScene");
    }
    public void QuitButton(){
        Application.Quit();
    }
    public void StopButton(){
        stop.SetActive(true);
        stopButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void BackButton(){
        Time.timeScale = 1;
         stop.SetActive(false);
         stopButton.SetActive(true);
    }
    private IEnumerator Times(){
        while (true){
            if (time <= 0){
                highMoneyText.text = string.Format("GGM {0:N0}",highMoney);
                stopButton.SetActive(false);
                gameOver.SetActive(true);
                Time.timeScale = 0;
            }
            if (time >= 9.5){
                float downPos2 = Random.Range(0.5f-bossToMove, 1f-(bossToMove*2));
                time -= downPos2;
                yield return new WaitForSeconds(0.02f);
            }
            else {
                time += upPos + downPos;
                yield return new WaitForSeconds(0.02f);
                float downPos2 = Random.Range(-0.1f, 0.1f);
                time += downPos2;
            }
        }
    }
    private int bossTimAdd = 1;
    private int bossKill = 0;
    public void BossKill(){
        bossKill = 1;
    }
    public IEnumerator Up(){
        upPos = +0.02f;
        yield return new WaitForSeconds(0.1f);
        upPos = 0f;
    }
    public IEnumerator Down(){
        downPos = -0.08f;
        yield return new WaitForSeconds(0.1f);
        downPos = 0f;
    }
    private IEnumerator moneyup(){
        while(true){
            yield return new WaitForSeconds(0.01f);
            money += time*bossTimAdd;
        }
    }
    public void BossKillReward(){
        money+=10000;
    }
    public void EnemyDeadFull(){
        popAudio.Play();
        StartCoroutine(Up());
    }
    public void EnemyDeadSleep(){
        StartCoroutine(Down());
    }
    private float bossToMove = 0;
    public void playerDamDoge(){
        time -= 1f;
    }
    public void playerDamRoc(){
        time -= 2f;
    }
    private IEnumerator SpawnLine(){
        while (true){
            yield return new WaitForSeconds(0.1f);
            Instantiate(linePrefab, new Vector3(12f, -12.5f + time ,1), Quaternion.identity);
            }
        }
    public float overW = 1f;
    private IEnumerator Spawn(){
        while(true){
            float randomY = Random.Range(MinPosition.y, MaxPosition.y);
            if ( money > 5000){
            hard = 0.1f;
            stage = 2;
            }
            if ( money > 15000){
            hard = 0.25f;
            stage = 3;
            }
            if ( money > 30000){
            hard = 0.4f;
            stage = 4;
            }
            if ( money > 50000){
            hard = 0.6f;
            stage = 5;
            }
            if ( money > 75000 && isBoss == false){
            isBoss = true;
            boss.BossSp();
            bossToMove = -0.2f;
            soundFaster.Speed();
            }
            if ( money > 75000){
            bossTimAdd = 0;
            hard = -0.4f;
            stage = 6;
            }
            if ( money > 75000 && bossKill == 1){
            bossTimAdd = 1;
            soundFaster.LowSpeed();
            hard = 0.8f;
            stage = 6;
            }
            if ( money > 100000 && bossKill == 1){
            hard = 0.8f;
            stage = 7;
            overW = 1.1f;
            }
            if ( money > 150000 && bossKill == 1){
            hard = 0.8f;
            stage = 8;
            overW = 1.225f;
            }
            if ( money > 200000 && bossKill == 1){
            hard = 0.8f;
            stage = 9;
            overW = 1.375f;
            }
            if ( money > 300000 && bossKill == 1){
            hard = 0.8f;
            stage = 10;
            overW = 1.5f;

            }
            float spawnDelay = Random.Range(0.8f-hard, 1.6f-(hard*1.5f));
            int spawn = Random.Range(0, 6);
            switch(spawn){
				case 1:
					for (int i = 0; i < 6; i++){
                            Instantiate(enemyPrefab[0], new Vector2(10f, randomY), Quaternion.identity);
                            yield return new WaitForSeconds(0.1f);
                        }
                        yield return new WaitForSeconds(spawnDelay);
					break;
				case 2:
					for (int i = 0; i < 6; i++){
                        Instantiate(enemyPrefab[1], new Vector2(10f, randomY), Quaternion.identity);
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(spawnDelay);
					break;
                case 3:
					for (int i = 0; i < 4; i++){
                        Instantiate(enemyPrefab[2], new Vector2(10f, randomY), Quaternion.identity);
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(spawnDelay * 1.2f);
					break;
                case 4:
					for (int i = 0; i < 4; i++){
                        Instantiate(enemyPrefab[3], new Vector2(10f, randomY), Quaternion.identity);
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(spawnDelay * 1.5f);
					break;
                case 5:
					for (int i = 0; i < 4; i++){
                        Instantiate(enemyPrefab[4], new Vector2(10f, randomY), Quaternion.identity);
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(spawnDelay * 1.5f);
					break;
            }
        }
    }
}