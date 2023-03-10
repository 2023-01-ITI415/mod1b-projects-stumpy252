using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Prototype : MonoBehaviour{
    static private Prototype S;

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitScore;
    public Text uitHighScore;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Dynamic")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public int score;
    public int shotsHit;
    public int highScore;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    
    void Start(){
        
        
        S = this;
        score = 0;
        level = 0;
        shotsHit = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    //called in start function
    void StartLevel(){
        if(castle != null){
            Destroy(castle);
        }
        if(score > highScore){
            highScore = score;
        }
        shotsHit = 0;
        shotsTaken = 0;
        Projectile.DESTROY_PROJECTILES();
        //instansiate castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        //reset goal
        

        UpdateGUI();
        mode = GameMode.playing;

        
    }

    void UpdateGUI(){
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken;
        uitScore.text = "Score: "+score;
        uitHighScore.text = "High Score: "+highScore;
    }

    void Update(){
        UpdateGUI();
        if ((mode == GameMode.playing) && (shotsHit == 12)){
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
        if(score > highScore){
            highScore = score;
        }
         
    }
    
    void NextLevel(){
        level++;
        if(level == levelMax){
            level = 0;
            shotsTaken = 0;
            score = 0;
            shotsHit = 0;
            
        }
        if(score > highScore){
            highScore = score;
        }
        StartLevel();
    }
    static public void SHOT_TAKEN(){
        S.shotsTaken++;
    }
    static public void SHOT_HIT(){
        S.shotsHit++;
    }
    static public void SCORE(){
        if(S.shotsTaken >= 25){
            S.score += 5;
        }
        else{S.score += 100 - (S.shotsTaken * 4);}
        
    }

    static public GameObject GET_COLLECTIBLE(){
        return S.castle;
    }
}

