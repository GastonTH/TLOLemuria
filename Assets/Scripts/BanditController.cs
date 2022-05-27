using System;
using UnityEngine;
using System.Collections;
using UIScripts;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BanditController : MonoBehaviour, CommonActions {
    
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] int        tiempoEntreDaño = 1;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemieLayerMask;
    private float coldDown;
    private float timeNextAttack;
    
    public Heroe myHeroe;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        coldDown = 0.8f;
        timeNextAttack = coldDown;
        
        // el boton al hacer click llamara a la funcion TakeDamage
        //GameObject.Find("Canvas").transform.Find("dmg").GetComponent<Button>().onClick.AddListener(TakeDamage);
        //GameObject.Find("Canvas").transform.Find("attack").GetComponent<Button>().onClick.AddListener(Attack);
        //_joystick = GameObject.Find("Canvas").transform.Find("Dynamic Joystick").GetComponent<Joystick>();
        
        // recogeremos el heroe del gamemanager
        //Debug.Log("DESDE EL BANDIDO RECOJO EL HEROE + " + myHeroe.Name);

    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if (m_grounded)
        {
            StartCoroutine(SavePosition());
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        /*if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }*/

        //Attack
        if (timeNextAttack > 0)
        {
            timeNextAttack -= Time.deltaTime;
        }
        
        else if(Input.GetMouseButtonDown(0) && timeNextAttack <=0) {
            DoDamage();
            timeNextAttack = coldDown;
        }

        //Jump
        else if ((Input.GetKeyDown("space")) && m_grounded)
        {
            //Debug.Log("saltando");
            Jump();
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

    IEnumerator SavePosition()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SetLastPosition(WhoIsUrLastPosition());
        //Debug.Log("ultima posicion guardada");
        yield return new WaitForSeconds(10f);
    }

    // esta funcion actuara cuando entre en contacto con el enemigo el jugador
    void OnCollisionEnter2D(Collision2D col)
    {

       // Debug.Log("TAG -> " + col.gameObject.tag);
        
        // el bandido detectara si se golpea con un enemigo 
        if (col.gameObject.tag == "Enemie")
        {   
            var e = col.gameObject.GetComponent<EnemyController>();
            var dmg = GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.Str;
            Debug.Log("el heroe a chocado con el enemigo");
            m_animator.SetTrigger("Hurt");
            //el enemigo recibe el daño del jugador
           e.TakeDamage(dmg);
           if (e.currentHealth <= 0)
           {
               // el enemigo a muerto por colisionar con el jugador
               //primero recogeremos la experiencia del enemigo
               GameObject.Find("GameManager").GetComponent<GameManager>().GetExperienceFromX(e.xp);
               Debug.Log("enemigo muerto, recogida de xp + " + e.xp);
               //despues lo destruiremos
               e.Die();
           }
        }

        // que el bandido detecte la moneda y la sume al heroe
        if (col.gameObject.tag == "Coin")
        {
            Debug.Log("el heroe a cogido la moneda");
            // primero recojemos el valor de la moneda
            int value = col.gameObject.GetComponent<CoinController>().value;
            // seguidamente se lo añadimos a las monedas del heroe
            GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.Coins += value;
            // y finalmente lo actualizamos en el canvas de las monedas
            GameObject.Find("GameManager").GetComponent<GameManager>().coinCountController.addCoins(value);
            Destroy(col.gameObject);
        }

    }

    public void Jump()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    public Vector3 WhoIsUrLastPosition()
    {
        return gameObject.transform.position;
    }
    
    IEnumerator waitHit()
    {
        var h = GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe;
        yield return new WaitForSeconds(0.4f);
        
        Collider2D[] weaponHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemieLayerMask);

        foreach (Collider2D enemy in weaponHit)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(h.Str);

            if (enemy.GetComponent<EnemyController>().currentHealth <= 0)
            {
                //primero recogeremos la experiencia del enemigo
                int xp = enemy.GetComponent<EnemyController>().xp;
                
                GameObject.Find("GameManager").GetComponent<GameManager>().GetExperienceFromX(xp);

                Debug.Log("enemigo muerto, recogida de xp + " + enemy.GetComponent<EnemyController>().xp);
            }
        }

        // funcion que despues de 3 segundos dejara de estar en combate a idle
        yield return new WaitForSeconds(3f);
        m_combatIdle = false;

    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null){ return;}

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    // esta funcion sirve para que el personaje reciba daño
    public void TakeDamage(int dmg)
    {
        // en esta funcion el heroe recibira daño
        var vit = GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit -= dmg;
        
        Debug.Log("VIDA DEL HEROE = " + GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit );
        // y de esta forma actualizamos el ui
        GameObject.Find("GameManager").GetComponent<GameManager>().healthBarController.setHealth(vit);
    }

    public void DoDamage()
    {        
        m_animator.SetTrigger("Attack");
        StartCoroutine(waitHit());
        m_combatIdle = true;
    }

    public void Run()
    {
        m_animator.SetInteger("AnimState", 2);
    }
}
