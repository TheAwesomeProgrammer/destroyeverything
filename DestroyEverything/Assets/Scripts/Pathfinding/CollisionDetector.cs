using UnityEngine;
using System.Collections.Generic;


public class UnWalkable
{
    public Vector3 Postion;
    public Vector3 LossyScale;
}

public class CollisionDetector : MonoBehaviour
{

    public List<UnWalkable> cUnwalkAbles { get; set; }

	// Use this for initialization
	void Start () {
        cUnwalkAbles = new List<UnWalkable>();
	    foreach (GameObject tUnWalkable in GameObject.FindGameObjectsWithTag("Unwalkable"))
	    {
          
	        cUnwalkAbles.Add(new UnWalkable()
	                             {
	                                 LossyScale = tUnWalkable.transform.lossyScale,
                                     Postion = tUnWalkable.transform.position
	                             });
        }

	   

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsPointCollidingWithUnWalkable(Vector3 pPosToCheck)
    {
      
        bool tIsCollingWithUnWalkable = false;
    
          foreach (UnWalkable tUnWalkable in cUnwalkAbles)
           {
               if (Vector3.Distance(tUnWalkable.Postion, pPosToCheck) < (tUnWalkable.LossyScale.x - tUnWalkable.LossyScale.x/10)) 
               {
                   tIsCollingWithUnWalkable = true;
               }
          }
           
        
        
        return tIsCollingWithUnWalkable;
    }
}
