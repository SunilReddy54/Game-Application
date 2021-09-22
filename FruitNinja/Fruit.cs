using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{

    public GameObject fruitSlicedPrefab;
    public float startForce = 15f;

    Rigidbody2D rb;

    Action fruitCut;
    Action fruitNoCut;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if(rb.velocity.y < 10)
        {
            rb.velocity = Vector2.down * 10;
        }
    }

    public void Init(Action fruitCut, Action fruitNoCut)
    {
        this.fruitCut = fruitCut;
        this.fruitNoCut = fruitNoCut;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("Cutting the fruit");
        if(col.tag == "Blade" )
        {
            fruitCut.Invoke();
            Vector3 direction = (col.transform.position - transform.position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
            Destroy(slicedFruit, 3f);
            Destroy(gameObject);
        }
        else if(col.tag == "EndTrigger")
        {
            fruitNoCut.Invoke();
            Destroy(gameObject);
        }
    }


}
