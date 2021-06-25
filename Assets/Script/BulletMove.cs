using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletMove : MonoBehaviour{
    [SerializeField]
    protected GameManager gameManager = null;
    [SerializeField]
    protected float speed = 10;
    public TrailRenderer trailRenderer{get; private set;}
    void Awake(){
        trailRenderer = GetComponent<TrailRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > gameManager.MaxPosition.x-3f) {
            trailRenderer.enabled = false;
        }
        if (transform.position.x > gameManager.MaxPosition.x) {
            transform.SetParent(gameManager.poolManager.transform);
            transform.localPosition=new Vector3(0f,0f,0f);
            gameObject.SetActive(false);
            
        }
    }
}
