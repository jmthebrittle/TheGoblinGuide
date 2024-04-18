using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
	
	public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider other){
		if(this.tag == "Coin" && other.tag == "Player"){
			Destroy(gameObject);
		}
    }
	
}
