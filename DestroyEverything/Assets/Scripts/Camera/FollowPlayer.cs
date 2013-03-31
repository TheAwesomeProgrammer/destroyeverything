using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPath>().GetSelectedPlayer() != null)
        {
            Vector3 tWantedPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPath>().GetSelectedPlayer().transform.position;
            tWantedPosition.y += 5;
            transform.position = tWantedPosition;

        }
	    
	}
}
