using UnityEngine;
using System.Collections;

public class CostCalculating : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int CalculateGCost(Node pNode)
    {
        Node tNode = pNode;
        int tGCost = 0;
        while(tNode.Parent != null)
        {
         if (tNode.Position.x < tNode.Parent.Position.x || tNode.Position.x > tNode.Parent.Position.x ||
                tNode.Position.z < tNode.Parent.Position.z || tNode.Position.z > tNode.Parent.Position.z)
            {
                tGCost += 10;
            }

            if (tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z > tNode.Parent.Position.z ||
                tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x < tNode.Parent.Position.x ||
                tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z < tNode.Parent.Position.z ||
                tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x > tNode.Parent.Position.x)
            {
                tGCost += 10;
            }

            tNode = tNode.Parent;
        }
        return tGCost;
    }

    public int CalculateHCost(Node pNode,Vector3 pEndPos,float pGridExpandNumber)
    {
        int tHCost = 0;
        float tXDistance = Mathf.Abs(pNode.Position.x - pEndPos.x);
        float tZDistance = Mathf.Abs(pNode.Position.z - pEndPos.z);

        tHCost = (int)Mathf.Round((((tXDistance / pGridExpandNumber) * 10)) + ((tZDistance / pGridExpandNumber) * 10));
        return tHCost;
    }
}
