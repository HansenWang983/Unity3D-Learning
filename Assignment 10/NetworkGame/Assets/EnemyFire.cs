using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

using UnityEngine;

public class EnemyFire : NetworkBehaviour {

	public GameObject bulletPrefab;
	
	private float time = 0;

	[Command]
  void CmdFire()
  {
     // This [Command] code is run on the server!

     // create the bullet object locally
     var bullet1 = (GameObject)Instantiate(
          bulletPrefab,
          transform.position - transform.forward,
          Quaternion.identity);

     bullet1.GetComponent<Rigidbody>().velocity = -transform.forward*4;

     // var bullet2 = (GameObject)Instantiate(
     //      bulletPrefab,
     //      transform.position + transform.forward,
     //      Quaternion.identity);

     // bullet2.GetComponent<Rigidbody>().velocity = -transform.forward*4;
     
     // spawn the bullet on the clients
     NetworkServer.Spawn(bullet1);
     // NetworkServer.Spawn(bullet2);
     // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
     Destroy(bullet1, 2.0f);
     // Destroy(bullet2, 2.0f);
  }

	
	// Update is called once per frame
	void Update () {
		time+=Time.deltaTime;
		if(time>1){
        	// Command function is called from the client, but invoked on the server
        	CmdFire();
        	time = 0;
    }
	}
}
