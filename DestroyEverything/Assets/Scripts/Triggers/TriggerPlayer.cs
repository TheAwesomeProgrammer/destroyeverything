using UnityEngine;
using System.Collections;

public class TriggerPlayer : MonoBehaviour
{

    public string TagToUse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position,transform.lossyScale);
    }

    void OnTriggerEnter(Collider pCollider)
    {
       if(pCollider.tag == TagToUse)
        {
            pCollider.gameObject.SendMessage("Triggered");
        }
    }
}
