using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(0, 4.34f, -15.46f);
        transform.position = Player.transform.position + new Vector3(0, 4.34f, -5f);
    }
}
