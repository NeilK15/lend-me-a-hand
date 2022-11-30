using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rigidbody;
    public float speed = 4f;
    public float rotationSpeed = 5f;
    public Camera main;
    
    private bool _shouldFollowMouse;
    

    private void Start()
    {
        _shouldFollowMouse = true;
        //StartCoroutine(FollowMouse(1000));
        rigidbody.velocity = rigidbody.transform.up * speed;
        
    }

    private void Update()
    {
        print(rigidbody.velocity);
        if (_shouldFollowMouse)
        {
            Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = (float) Math.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            
        }
    }

    IEnumerator FollowMouse(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        _shouldFollowMouse = false;
    }
}
