using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Client client = new Client();
    // Use this for initialization
    void Start () {

        client.Connect();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            client.Send();
        }
    }
}
