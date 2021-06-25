using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyMove : MonoBehaviour{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float w;
    [SerializeField]
    private float limit_W;
    private float randomY = 0;
    private bool isdead = false;
    private Image gpuHpBar;
    [SerializeField]
    private GameObject GpuHpBarPrefab;
    private SpriteRenderer mesh;
    private GameManager game;
    private Image gpuHpBarFill;
    private void Awake(){
        game = FindObjectOfType<GameManager>();
        mesh = GetComponent<SpriteRenderer>();
        randomY = Random.Range(-4f, 4f);
        gpuHpBar = Instantiate(GpuHpBarPrefab, game.canvas.transform).GetComponent<Image>();
        gpuHpBarFill = gpuHpBar.transform.GetChild(0).GetComponent<Image>();
        gpuHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        gpuHpBar.gameObject.SetActive(true);
    }
    protected virtual void Update(){
        gpuHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        gpuHpBarFill.fillAmount = w / limit_W;
        if(transform.position.y >= randomY){
            transform.Translate(Vector2.down * speed * Time.deltaTime * 5);
        }
        else{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (w >= limit_W){
                game.EnemyDeadFull();
                StartCoroutine(Dead());
            }
            if(transform.position.x <= game.MinPosition.x){
                game.EnemyDeadSleep();
                Destroy(gameObject);
                Destroy(gpuHpBar.gameObject);
            }
        }
    }
        private void OnCollisionEnter2D(Collision2D collision){
        if (transform.position.y <= randomY){
            if (collision.collider.tag == "Bullet"){
                StartCoroutine(Damaged());
                Destroy(collision.gameObject);
                w += 6/game.overW;
            }
            if (collision.collider.tag == "Bullet2"){
                StartCoroutine(Damaged());
                w += 300;
            }
        }
    }
    private IEnumerator Damaged(){
        mesh.material.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        mesh.material.color = Color.white;
    }
    private IEnumerator Dead(){
        yield return null;
        Destroy(gameObject);
        Destroy(gpuHpBar.gameObject);
    }
}
