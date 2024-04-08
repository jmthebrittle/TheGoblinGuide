using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // to disable
        if (Input.GetKeyDown(KeyCode.Return))
        {
            rigidbody.isKinematic = false;
        }

        if(gameObject.transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }
}
