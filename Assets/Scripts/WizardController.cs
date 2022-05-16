using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemieLayerMask;
    private float coldDown;
    private float timeNextAttack;
    
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        coldDown = 0.8f;
        timeNextAttack = coldDown;
    }

    // Update is called once per frame
    void Update()
    {
 /*if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
 }*/

 //Check if character just started falling
 /*if(m_grounded && !m_groundSensor.State()) {
     m_grounded = false;
     m_animator.SetBool("Grounded", m_grounded);
 }*/

 // -- Handle input and movement --
 float inputX = Input.GetAxis("Horizontal");

 // Swap direction of sprite depending on walk direction
 if (inputX > 0)
     transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
 else if (inputX < 0)
     transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);

 // Move
 m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

 //Set AirSpeed in animator
 m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

 // -- Handle Animations --
 //Death
 if (Input.GetKeyDown("e")) {
     if(!m_isDead)
         m_animator.SetTrigger("Death");
     else
         m_animator.SetTrigger("Recover");

     m_isDead = !m_isDead;
 }
            
 //Hurt
 else if (Input.GetKeyDown("q"))
     m_animator.SetTrigger("Hurt");
        
 //Attack
 else if (timeNextAttack > 0)
 {
     timeNextAttack -= Time.deltaTime;
 }
        
 else if(Input.GetMouseButtonDown(0) && timeNextAttack <=0) {
     Attack();
     timeNextAttack = coldDown;
 }

 //Change between idle and combat idle
 else if (Input.GetKeyDown("f"))
     m_combatIdle = !m_combatIdle;

 //Jump
 else if (Input.GetKeyDown("space") && m_grounded) {
     m_animator.SetTrigger("Jump");
     m_grounded = false;
     m_animator.SetBool("Grounded", m_grounded);
     m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
     //m_groundSensor.Disable(0.2f);
 }

 //Run
 else if (Mathf.Abs(inputX) > Mathf.Epsilon)
     m_animator.SetInteger("AnimState", 2);

 //Combat Idle
 else if (m_combatIdle)
     m_animator.SetInteger("AnimState", 1);

 //Idle
 else
     m_animator.SetInteger("AnimState", 0);
        
    }

    private void Attack()
    {
        m_animator.SetTrigger("Attack");
        StartCoroutine(waitHit());
    }
    
    IEnumerator waitHit()
    {
        yield return new WaitForSeconds(0.4f);
        
        Collider2D[] weaponHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemieLayerMask);

        foreach (Collider2D enemy in weaponHit)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(20);

            if (enemy.GetComponent<EnemyController>().currentHealth <= 0)
            {
                StartCoroutine(DestroyEnemy(enemy.GetComponent<GameObject>()));
            }
        }
        
    }

    IEnumerator DestroyEnemy(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        Destroy(obj);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null){ return;}

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}