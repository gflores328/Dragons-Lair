using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDebris : MonoBehaviour
{
    public AudioClip crumble;
    public GameObject child;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0, 0, Time.deltaTime) * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);

            StartCoroutine(RockBreak());
        }
        if (collision.gameObject.tag == "Enemy")
        {

            StartCoroutine(RockBreak());
        }

        if (collision.gameObject.tag == "Bullet")
        {

            StartCoroutine(RockBreak());
        }

        if (collision.gameObject.layer == 7)
        {

            StartCoroutine(RockBreak());
        }

    }


    IEnumerator RockBreak()
    {
        GetComponent<SphereCollider>().enabled = false;
        child.GetComponent<MeshRenderer>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(crumble);
        yield return new WaitForSeconds(.5f);
    }
}
