using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirection;
    public float secondsBetweenAppleDrop;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropApple", 2f);
        //dropping apples every second
    }
    void DropApple(){
        GameObject apple = Instantiate<GameObject>( applePrefab );
        apple.transform.position = transform.position;
        Invoke ("DropApple", secondsBetweenAppleDrop);
    }

    // Update is called once per frame
    void Update()
    {
        // basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        // changing direction
        if(pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed); //right
        }else if (pos.x > leftAndRightEdge){
            speed = -Mathf.Abs(speed); //left
        }
    }
    void FixedUpdate(){
        if(Random.value < chanceToChangeDirection){
            speed *= -1;
        }
    }
}
