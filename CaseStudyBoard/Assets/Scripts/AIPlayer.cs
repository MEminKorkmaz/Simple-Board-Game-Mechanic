using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public GameManager gameManager;

    public float speed;

    public bool isMoving;


    void Awake(){
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        checkPosition();
        if(isMoving){
            move();
        }
    }

    public void move(){
        
        if(gameManager.isGameEnded) return;

        transform.position = Vector2.MoveTowards(transform.position ,
        gameManager.WayPoints[gameManager.aiPlayerWayPointIndex].position ,
        speed * Time.deltaTime);

        if(transform.position == gameManager.WayPoints[gameManager.aiPlayerWayPointIndex].position){
            gameManager.aiPlayerWayPointIndex++;
            gameManager.dice--;
            if(gameManager.dice <= 0){
                isMoving = false;
                gameManager.playingTurn *= -1;
                gameManager.isDiceUsed = false;
                gameManager.aiPlayerTurnCounter++;
            }
        }
    }

    public void checkPosition(){
        if(transform.position == gameManager.WayPoints[gameManager.WayPoints.Length - 1].position){
            gameManager.isGameEnded = true;
            gameManager.playerText.gameObject.SetActive(false);
            gameManager.aiPlayerText.gameObject.SetActive(true);
            gameManager.diceText.gameObject.SetActive(false);
            gameManager.playerTurnText.gameObject.SetActive(false);
        }
    }
}
