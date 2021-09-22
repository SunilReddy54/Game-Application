using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    public float minCuttingVelocity = .001f;
    
    bool isCutting = false;

    GameObject currentBladeTrail;

    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleCollider;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase) 
            {
                case TouchPhase.Began:
                    StartCutting();
                    break;
                case TouchPhase.Ended:
                    StopCutting();
                    break;
                default:
                    UpdateCut();
                    break;
            }
        }
    }

    void UpdateCut()
    {
        rb.position = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        circleCollider.enabled = true;
    }
    void StartCutting()
    {
        isCutting = true;
        currentBladeTrail = Instantiate(BladeTrailPrefab, transform);
        circleCollider.enabled = false;
    }

    void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2f);
        circleCollider.enabled = false;
    }
}
