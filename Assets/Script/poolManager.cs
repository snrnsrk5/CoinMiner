using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolManager : MonoBehaviour
{
    private PlayerMove playerMove;
    private void Awake(){
        playerMove = FindObjectOfType<PlayerMove>();
    }
    private void Update(){
        transform.position=playerMove.transform.position;
    }
}
