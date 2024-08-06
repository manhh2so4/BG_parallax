using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    
    [Header("Player_Property")]
    public float speedRun = 5f;
    [SerializeField] float speedJump = 12f;
    public int velocityView = 0; 
    float MoveInput = 0f;
    public int amountOfJumps = 1;
    Rigidbody2D myRigibody;
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();      
    }
    void Update()
    {          
        CheckInput();
        Move();     
    }
    private void FixedUpdate() {            
        
    }
    void Move(){   
        myRigibody.velocity = new Vector2 (MoveInput*speedRun, myRigibody.velocity.y);     
    }
    void CheckInput(){
        MoveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")){  
            Debug.Log("jump");          
            Jump(); 
        }
    }
    void Jump(){
            myRigibody.velocity  = new Vector2(myRigibody.velocity.x ,speedJump);
    }
}