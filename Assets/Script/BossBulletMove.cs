using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMove : BulletMove{
    protected override void Update(){
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < gameManager.MinPosition.x) {
            Destroy(gameObject);
        }
    }
}
