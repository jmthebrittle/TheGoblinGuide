using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldpickedupscript : MonoBehaviour
{
    public Text goldText;
    public int playergold;


    // Start is called before the first frame update
    void Start()
    {
        playergold = gameObject.GetComponent<PlayerMovement>().goldvalue;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + playergold;
        UnityEngine.Debug.Log(playergold);
    }
}
