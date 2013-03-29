using UnityEngine;
using System.Collections;

public class NpcWaypoint : MonoBehaviour
{

    public string WaypointName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position,new Vector3(1,1,1));
    }
}
