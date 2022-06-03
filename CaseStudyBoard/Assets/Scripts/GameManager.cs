using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text diceText;
    public Text playerText;
    public Text aiPlayerText;
    public Text playerTurnText;
    public Text errorText;
    
    [HideInInspector]
    public int dice;
    
    [HideInInspector]
    public bool isDiceUsed;

    [HideInInspector]
    public bool canPlayerUseSpecialDice;

    [HideInInspector]
    public bool canAiPlayerUseSpecialDice;

    [HideInInspector]
    public int playingTurn; // 1 for player -- -1 for AI

    public int playerTurnCounter;
    public int aiPlayerTurnCounter;

    public Transform[] WayPoints;
    public int playerWayPointIndex;
    public int aiPlayerWayPointIndex;

    public Player player;
    public AIPlayer aiPlayer;

    [HideInInspector]
    public bool isGameEnded;


    void Awake(){
        player = GameObject.FindWithTag("player").GetComponent<Player>();
        aiPlayer = GameObject.FindWithTag("AI").GetComponent<AIPlayer>();
    }

    void Start(){
        playerWayPointIndex = 0;
        aiPlayerWayPointIndex = 0;
        playingTurn = 1;
        isGameEnded = false;
        canPlayerUseSpecialDice = true;
        canAiPlayerUseSpecialDice = true;

        diceText.gameObject.SetActive(false);
        playerText.gameObject.SetActive(false);
        aiPlayerText.gameObject.SetActive(false);
        playerTurnText.gameObject.SetActive(true);
        errorText.gameObject.SetActive(false);
    }

    void Update(){
        if(isGameEnded) return;

        if(playingTurn == 1){
            playerTurnText.text = "PLAYER'S TURN";
        }
        else if(playingTurn == -1){
            playerTurnText.text = "AI'S TURN";
        }

        if(playerTurnCounter > 3){
            canPlayerUseSpecialDice = true;
            playerTurnCounter = 0;
        }

        if(aiPlayerTurnCounter > 3){
            canAiPlayerUseSpecialDice = true;
            aiPlayerTurnCounter = 0;
        }

        
        if(playingTurn == 1){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isDiceUsed) return;

            diceText.gameObject.SetActive(true);
            isDiceUsed = true;
            dice = Random.Range(1 , 7);
            diceText.text = dice.ToString();

            player.isMoving = true;
            player.move();
            }

        if(Input.GetKeyDown(KeyCode.S)){
            if(!canPlayerUseSpecialDice || isDiceUsed) return;

            diceText.gameObject.SetActive(true);
            canPlayerUseSpecialDice = false;
            isDiceUsed = true;
            dice = Random.Range(5 , 11);
            diceText.text = dice.ToString();

            player.isMoving = true;
            player.move();
            }
        }

        if(playingTurn == -1){
            
            if(canAiPlayerUseSpecialDice){
                canAiPlayerUseSpecialDice = false;
                isDiceUsed = true;
                dice = Random.Range(5 , 11);
                diceText.gameObject.SetActive(true);
                diceText.text = dice.ToString();

                aiPlayer.isMoving = true;
                aiPlayer.move();
            }

            else if(!isDiceUsed){
                isDiceUsed = true;
                dice = Random.Range(1 , 7);
                diceText.gameObject.SetActive(true);
                diceText.text = dice.ToString();

                aiPlayer.isMoving = true;
                aiPlayer.move();
            }
        }
    }
}
