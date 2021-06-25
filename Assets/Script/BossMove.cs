using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    private GameObject dogeCoinPrefab;
    [SerializeField]
    private GameObject roccatPrefab;
    [SerializeField]
    private Transform[] taretPosition;
    [SerializeField]
    private int BossHp = 200000;
    private SpriteRenderer mesh;
    [SerializeField]
    private GameManager game;
    [SerializeField]
    private GameObject BossHpObject = null;
    [SerializeField]
    private Text BossHpObjectText; 
    public Canvas canvas;
    void Awake(){
        BossHpObject.SetActive(false);
        gameObject.SetActive(false);
        mesh = GetComponent<SpriteRenderer>();

    }
    void Update(){
        if(BossHp <= 0){
            game.BossKill();
            game.BossKillReward();
            BossHpObject.SetActive(false);
            gameObject.SetActive(false);
        }
        BossHpObjectText.text = string.Format("{0}",BossHp);
    }
    public void BossSp(){
        BossHpObject.SetActive(true);
        gameObject.SetActive(true);
        StartCoroutine(BossDogeFire());
        StartCoroutine(BossRoccatFire());
    }
    GameObject dogeCoin = null;
    GameObject roccat = null;
    private IEnumerator BossDogeFire(){
        while(true){
        dogeCoin = Instantiate(dogeCoinPrefab, taretPosition[0]);
        dogeCoin.transform.SetParent(null);
        yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator BossRoccatFire(){
        while(true){
        yield return new WaitForSeconds(1.2f);
        roccat = Instantiate(roccatPrefab, taretPosition[1]);
        roccat.transform.SetParent(null);
        roccat = Instantiate(roccatPrefab, taretPosition[2]);
        roccat.transform.SetParent(null);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.tag == "Bullet"){
            Destroy(collision.gameObject);
            BossHp -= 6;
        }
        if (collision.collider.tag == "Bullet2"){
            BossHp -= 300;
        }
    }
}
