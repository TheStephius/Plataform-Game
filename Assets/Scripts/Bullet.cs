using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=10;
    public GameObject bala;

    Animator myAnimator;
    Rigidbody2D myBody;
    CapsuleCollider2D myCollider;

    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
         if (col.gameObject.tag=="Plataformas")
         {
             myAnimator.SetBool("Explosion",true);
             
             Destroy(gameObject,0.03f);
         }
    }
    
}
