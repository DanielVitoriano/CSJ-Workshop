using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public int health, sorted;
    public float speed, initialSpeed, stopDistance, atackTime;
    public bool isRight, isDefend, isAtack, isDead;
    float waitAtack = 0f;

    public Rigidbody2D rig;
    public Animator anim;

    Transform player;

    void Start()
    {
        speed = initialSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if(distance <= stopDistance ){
            speed = 0;
            waitAtack += Time.deltaTime;
            if(waitAtack > atackTime && sorted <50 && isAtack == false){ //ataca
                onAttack();
            }
            else if(waitAtack > atackTime && sorted >= 50 && isDefend == false){ //defende
                onDefend();
            }
            else{ // fica parado
                sorted = Random.Range(0, 100);
                anim.SetInteger("transition", 0);
             }
        }
        else{
            speed = initialSpeed;
        }

        if(transform.position.x - player.position.x < 0){
            isRight = true;
        }
        else{
            isRight = false;
        }
        if(isDead == true){
            speed = 0;
        }
    }

    void FixedUpdate(){
        if(isDead == false){
            if(isRight){
                rig.velocity = new Vector2(speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 0);
                anim.SetInteger("transition", 1);
            }
            else{
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, -180);
                anim.SetInteger("transition", 1);
            }
        }
        else{
            speed = 0;
        }
    }

    public void OnHit(){
        if(isDefend == false){
            health--;
            speed = 0;
            if(health <= 0){
                isDead = true;
                anim.SetTrigger("death");
                Destroy(gameObject, 0.5f);
            }
            else{
                anim.SetTrigger("hit");
            }
        }
    }
   IEnumerator OnWait(float wait){
        yield return new WaitForSeconds(wait);
        waitAtack = 0;
    }
    void onAttack(){
        isAtack = true;
        player.GetComponent<Player>().OnHit();
        anim.SetInteger("transition", 3);
        StartCoroutine(OnWait(0.57f));
        isAtack = false;
    
    }
    void onDefend(){
        isDefend = true;
        anim.SetInteger("transition", 2);

        StartCoroutine(OnWait(2f));
        isDefend = false;
    }
}