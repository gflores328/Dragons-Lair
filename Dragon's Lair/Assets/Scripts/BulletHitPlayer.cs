using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitPlayer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    { 
            Destroy(gameObject);   
    }
}
