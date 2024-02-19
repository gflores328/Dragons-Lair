using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtFire : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    private Vector3 fireDirection;
    private bool firing = false;

    private void Update()
    {
        fireDirection = (player.transform.position - transform.position).normalized;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !firing)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        Debug.Log("Firing");
        firing = true;
        GameObject clonedBullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody rb = clonedBullet.GetComponent<Rigidbody>();
        rb.velocity = fireDirection * bulletSpeed;

        

        yield return new WaitForSeconds(2);
        Destroy(clonedBullet);
        firing = false;
    }

}
