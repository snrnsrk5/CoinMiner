using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private GameObject skill1Prefab = null;
    [SerializeField]
    private GameObject skill2Prefab = null;
    [SerializeField]
    private float bulletDelay = 0.5f;
    private Vector2 targetPosition = Vector2.zero;
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private Transform[] bulletPosition;
    private poolManager poolManager;
    private GameManager gameManager;
    [SerializeField]
    private Text skill1CoolText; 
    [SerializeField]
    private Text skill2CoolText; 
    [SerializeField]
    private GameObject skill1CoolTextTime = null;
    [SerializeField]
    private GameObject skill2CoolTextTime = null;
    private void Awake(){
        gameManager = FindObjectOfType<GameManager>();
        poolManager = FindObjectOfType<poolManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Fire());
        StartCoroutine(SikllCool());
    }
    private void Update(){
        if (Input.GetMouseButton(0)&&UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
        GameObject bullet = null;
    private IEnumerator Fire(){
        while (true){
            for(int i=0;i<3;i++){
                bullet = PoolorSpawn(bulletPosition[i]);
                bullet.transform.SetParent(null);
            }
            
            yield return new WaitForSeconds(bulletDelay);
        }
    }
    private IEnumerator SikllCool(){
        while(true){
        skill1CoolText.text = string.Format("{0}",Skill1Cool);
        skill2CoolText.text = string.Format("{0}",Skill2Cool);
        if(Skill1Cool > 0){
            Skill1Cool--;
            skill1CoolTextTime.SetActive(true);
        }
        if(Skill2Cool > 0){
            Skill2Cool--;
            skill2CoolTextTime.SetActive(true);
        }
        if(Skill1Cool == 0) skill1CoolTextTime.SetActive(false);
        if(Skill2Cool == 0) skill2CoolTextTime.SetActive(false);
        yield return new WaitForSeconds(1f);
        }
    }
    GameObject skill1 = null;
    private int Skill1Cool = 0;
    GameObject skill2 = null;
    private int Skill2Cool = 0;
    public void Skill1(){
        if( Skill1Cool == 0){
        StartCoroutine(SkillCo1());
        Skill1Cool = 40;
        }
    }
    public void Skill2(){
        if( Skill2Cool == 0){
        StartCoroutine(SkillCo2());
        Skill2Cool = 40;
        }
    }
    private IEnumerator SkillCo1(){
        for(int k = 0; k < 10; k++){
        for(int j = 0; j < 60; j++){
        for(int i = 0; i < 3; i++){
            skill1 = Instantiate(skill1Prefab, bulletPosition[i]);
            skill1.transform.SetParent(null);
        }
        yield return new WaitForSeconds(bulletDelay/3);
        }
        yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator SkillCo2(){
        for(int i = 0; i < 20; i++){
            skill2 = Instantiate(skill2Prefab, bulletPosition[0]);
            skill2.transform.SetParent(null);
            skill2 = Instantiate(skill2Prefab, bulletPosition[2]);
            skill2.transform.SetParent(null);
            yield return new WaitForSeconds(0.5f);
        }
    }
    private GameObject PoolorSpawn(Transform shotPos)
    {
        GameObject item;
        if(poolManager.transform.childCount > 0){
            item = poolManager.transform.GetChild(0).gameObject;
        }
        else{
            item = Instantiate(bulletPrefab, shotPos);
        }
        
        item.transform.position = shotPos.position;
        item.transform.SetParent(null);
        item.SetActive(true);
        item.GetComponent<BulletMove>().trailRenderer.enabled = true;

        return item;
    }
    private bool isDead = false;
    private void OnTriggerEnter2D(Collider2D collision){
        if (isDead == true) return;
        if (collision.tag == "Enemy"){
            isDead = true;
            StartCoroutine(Dead());
        }
        
    }
    private IEnumerator Dead(){
        for (int i = 0; i < 5; i++){
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDead = false;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.tag == "Doge"){
            gameManager.playerDamDoge();
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "Roccat"){
            gameManager.playerDamRoc();
            Destroy(collision.gameObject);
        }
    }
}