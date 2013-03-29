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

    public Vector3 cPLayerLossyScale { get; set; }

	// Use this for initialization
	void Start () {
        cUnwalkAbles = new List<UnWalkable>();
	    foreach (GameObject tUnWalkable in GameObject.FindGameObjectsWithTag("Unwalkable"))
	    {
            print(tUnWalkable.transform.lossyScale.z / 2);
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

    void SetPlayerLossyScale(Vector3 pPlayerLossyScale)
    {
       cPLayerLossyScale = pPlayerLossyScale / 2;
    }

    public bool IsPointCollidingWithUnWalkable(Vector3 pPosToCheck)
    {
      
        bool tIsCollingWithUnWalkable = false;
    
          foreach (UnWalkable tUnWalkable in cUnwalkAbles)
           {
              if (Mathf.Abs(tUnWalkable.Postion.x - pPosToCheck.x) < (tUnWalkable.LossyScale.x / 2 + cPLayerLossyScale.x) &&
                  Mathf.Abs(tUnWalkable.Postion.z - pPosToCheck.z) < (tUnWalkable.LossyScale.z / 2) + cPLayerLossyScale.z) 
               {
                 
                   tIsCollingWithUnWalkable = true;
               }
          }
           
        
        
        return tIsCollingWithUnWalkable;
    }

    public UnWalkable IsPointCollidingWithUnWalkableAndGetUnwalkable(Vector3 pPosToCheck)
    {

        UnWalkable tIsCollingWithUnWalkable = null;

        foreach (UnWalkable tUnWalkable in cUnwalkAbles)
        {
            if (Mathf.Abs(tUnWalkable.Postion.x - pPosToCheck.x) < (tUnWalkable.LossyScale.x / 2 + cPLayerLossyScale.x) &&
                Mathf.Abs(tUnWalkable.Postion.z - pPosToCheck.z) < (tUnWalkable.LossyScale.z / 2) + cPLayerLossyScale.z)
            {

                tIsCollingWithUnWalkable = tUnWalkable;
            }
        }



        return tIsCollingWithUnWalkable;
    }
}
