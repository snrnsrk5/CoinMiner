using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveSkillRed : BulletMove{
    protected override void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > gameManager.MaxPosition.x+3f) {
            Destroy(gameObject);
        }
    }
}
