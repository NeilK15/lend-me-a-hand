using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Transform firePoint;
    public float fireDistance = 15f;
    public LineRenderer lineRenderer;


    public override void UseHoldable()
    {

        base.UseHoldable();

        StartCoroutine(Shoot());

    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, fireDistance, enemyCheck);

        if (hitInfo)
        {
            IDamageable enemy = hitInfo.transform.GetComponent<IDamageable>();

            if (enemy != null)
                enemy.Damage(equipment.damageModifier);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);

        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;

        // wait one frame
        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;

    }


}
