using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject disparador;

    public bool shooting;
    private float shootT;
    public float time;
    public bool Dash;
    public float DashT;
    public float SDash;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    private int SaltosAdicionales;
    public int ValorSaltos;
    //public int ValorSaltos;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("Is Running", true);

        myBody = GetComponent <Rigidbody2D>();

        myCollider = GetComponent <BoxCollider2D>();

        SaltosAdicionales=ValorSaltos;
    }

    void Update()
    {
        Correr();
        Saltar();
        Caer();
        Disparar();
        DashSkill();
    }  
    void Disparar()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Instantiate(bala, disparador.transform.position, transform.rotation);
            
            if (!shooting)
            {
                shooting = true;
            }
        }
        if (shooting)
        {
            shootT += 1 * Time.deltaTime;
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
         myAnimator.SetLayerWeight(1, 0);
        }
            
        if (shootT >=time)
        {
            shooting = false;
            shootT = 0;
        }
    }
    void Caer()
    {
        if(myBody.velocity.y < 0 & myAnimator.GetBool("TakeOff"))
        {
            myAnimator.SetBool("Is Falling", true);
        }

    }
    void FinishJump()
    {
        myAnimator.SetBool("Is Falling", true);
        myAnimator.SetBool("TakeOff", false); 
    }
    void Saltar()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myAnimator.GetBool("TakeOff"))
        {
            myAnimator.SetBool("Is Falling", false);
            myAnimator.SetBool("TakeOff", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnimator.SetTrigger("Jump");
                myAnimator.SetBool("TakeOff", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            myAnimator.SetTrigger("Jump");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnimator.SetTrigger("Jump");
            }
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            SaltosAdicionales = ValorSaltos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && SaltosAdicionales > 0)
        {
            myBody.velocity = Vector2.up * jumpForce;
            SaltosAdicionales--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && SaltosAdicionales == 0 && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            myBody.velocity = Vector2.up * jumpForce;
        }

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Is Falling", true);
        }

    }
    void DashSkill()
    {
        if (Input.GetKey(KeyCode.X))
        {
            DashT += 1 * Time.deltaTime;

            if (DashT <0.35f)
            {
                myAnimator.SetBool("Dash", true);
                transform.Translate(Vector2.right * SDash * Time.fixedDeltaTime);
                transform.Translate(Vector2.left * SDash * Time.fixedDeltaTime);
            }
            else
            {
                Dash = false;
                myAnimator.SetBool("Dash", false);
            }
        }
        else
        {
            Dash = false;
            myAnimator.SetBool("Dash", false);
            DashT = 0;
        }
    }
    void Correr()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(movH * speed, movV * speed) * Time.deltaTime;
        transform.Translate(movimiento);

        if (movH != 0 )
        {
            myAnimator.SetBool("Is Running", true);
            
            if (movH < 0 )
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
                transform.localScale = new Vector2(1, 1);
        }
        else
        {
            myAnimator.SetBool("Is Running", false);
        }
    }
}
