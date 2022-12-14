using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBomb : MonoBehaviour
{
    public Rigidbody2D rb;
    public float timer;
    public Transform center;
    public float explosionRadius;
    public CircleCollider2D circleCollider;
    public float upForce = 5f;
    public float speed = 10f;

    public ParticleSystem explosionEffect;

    public float explosionForce = 10f;

    public LayerMask explosionLayerMask;

    public Equipment bombItem;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;

        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {

        yield return new WaitForSeconds(timer);

        Explode();

    }

    private void Explode()
    {

        circleCollider.enabled = true;



        Collider2D[] colliders = Physics2D.OverlapCircleAll(center.position, explosionRadius, explosionLayerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].gameObject.Equals(gameObject))
            {
                Rigidbody2D rb = colliders[i].GetComponent<Rigidbody2D>();
                Character character = colliders[i].GetComponent<Character>();


                if (rb != null)
                {
                    rb.AddForce((Vector3.Normalize(colliders[i].transform.position - center.position) * explosionForce * 1 / rb.mass * 1 / Vector3.Distance(colliders[i].transform.position, center.position)) /*+ new Vector3(0, upForce)*/, ForceMode2D.Impulse);
                }

                if (character != null)
                {
                    character.Damage(bombItem.damageModifier);
                }

            }
        }



        // Apply Explosion effect
        Instantiate(explosionEffect, center.position, center.rotation);


        Destroy(gameObject);
    }


}
