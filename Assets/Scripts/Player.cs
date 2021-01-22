using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    public int lifePoints;
    public float speed, jumpForce, atkRadius, stamina;
    private bool isJumping, isAttack, isDead = false;
    float recoveryCount;

    [Header("Objetos")]
    public GameControler gc;
    public Transform FirePoint;
    public Rigidbody2D rig;
    public Animator anim;
    public LayerMask enemyLayer;
    public Image lifeBar, staminaBar;

    [Header("Audio Settings")]
    public AudioClip atackSound;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false){
        Jump();
        onAttack();
        staminaBar.fillAmount = (float)stamina / 100;
        if(stamina < 100){
            stamina += 0.7f;
        }
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && isJumping == false){
            anim.SetInteger("transition", 5);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    
    void onAttack(){
        if(Input.GetButtonDown("Fire1") && stamina >= 30 && isAttack == false){
            anim.SetInteger("transition", 3);
            stamina -= 30;
            isAttack = true;
            sound.PlayOneShot(atackSound);
            Collider2D hit = Physics2D.OverlapCircle(FirePoint.position, atkRadius, enemyLayer);

            if(hit != null){
                if(hit.tag == "Esqueleto"){
                    hit.GetComponent<Enemy1>().OnHit();
                    if(hit.GetComponent<Enemy1>().health <= 0){
                        gc.increseScore(13);
                    }
                }
                else if(hit.tag == "Goblin"){
                    hit.GetComponent<Enemy2>().OnHit();
                     if(hit.GetComponent<Enemy2>().health <= 0){
                        gc.increseScore(9);
                    }
                }
                
            }

            StartCoroutine(OnAttacking());
        }
    }
    
    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(FirePoint.position, atkRadius);
    }

    IEnumerator OnAttacking(){
        yield return new WaitForSeconds(0.3f);
        isAttack = false;
    }

    void OnMove(){
         float direction = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(direction * speed, rig.velocity.y);

        if(direction > 0  && isJumping == false && isAttack == false){
            transform.eulerAngles = new Vector2(0,0);
            anim.SetInteger("transition", 2);
        }
        if(direction < 0  && isJumping == false && isAttack == false){
            transform.eulerAngles = new Vector2(0, -180);
            anim.SetInteger("transition", 2);
        }
        if(direction == 0 && isJumping == false && isAttack == false){
            anim.SetInteger("transition", 0);
        }
    }
    void FixedUpdate() {
       if(isDead == false){
           OnMove();
       }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            isJumping = false;
        } 
    }

    public void OnHit(){
        if(isDead == false){
            if(lifePoints >= 1){
                anim.SetTrigger("hit");
                lifePoints --;
                lifeBar.fillAmount = (float)lifePoints / 100;
            }
            else{
                isDead = true;
                anim.SetTrigger("death");
                gc.ShowGameOver();
            }
        }
    }
}