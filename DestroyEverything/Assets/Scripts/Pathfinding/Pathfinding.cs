using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public int GCost = 0;
    public int HCost = 0;
    public int FCost = 0;
    public Vector3 Position = Vector3.zero;
    public Node Parent = null;
}

public class Pathfinding : MonoBehaviour
{

 

    public float GridExpandNumber = 0.1f;

    public List<Node> cClosedList { get; set; }
    public List<Node> cOpenList { get; set; }

    private Node mCurrentNode; 

    private CollisionDetector mCollisionDetector;
    private CostCalculating mCostCalculater;


	// Use this for initialization
	void Start ()
	{
	    mCollisionDetector = GameObject.Find("CollisionDetector").GetComponent<CollisionDetector>();
        mCostCalculater = GameObject.Find("CostCalculator").GetComponent<CostCalculating>();

	}

    void AddSquaresToOpenListAroundYou(Node tNodeSquaresAround, Vector3 pPosToFindRoadTo)
    {
        // Right position of player
        Node tRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position =
                new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y,
                            tNodeSquaresAround.Position.z)
        };

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tRightPos.Position))
        {
            tRightPos.GCost = mCostCalculater.CalculateGCost(tRightPos);
            tRightPos.HCost = mCostCalculater.CalculateHCost(tRightPos, pPosToFindRoadTo, GridExpandNumber);
            tRightPos.FCost = tRightPos.GCost + tRightPos.HCost;
            cOpenList.Add(tRightPos);
        }


        // Left position of player
        Node tLeftPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tLeftPos.Position))
        {
            tLeftPos.GCost = mCostCalculater.CalculateGCost(tLeftPos);
            tLeftPos.HCost = mCostCalculater.CalculateHCost(tLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tLeftPos.FCost = tLeftPos.GCost + tLeftPos.HCost;
            cOpenList.Add(tLeftPos);
        }

        // Up position of player
        Node tUpPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpPos.Position))
        {
            tUpPos.GCost = mCostCalculater.CalculateGCost(tUpPos);
            tUpPos.HCost = mCostCalculater.CalculateHCost(tUpPos, pPosToFindRoadTo, GridExpandNumber);
            tUpPos.FCost = tUpPos.GCost + tUpPos.HCost;
            cOpenList.Add(tUpPos);
        }

        // UpLeft position of player
        Node tUpLeftPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpLeftPos.Position))
        {
            tUpLeftPos.GCost = mCostCalculater.CalculateGCost(tUpLeftPos);
            tUpLeftPos.HCost = mCostCalculater.CalculateHCost(tUpLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tUpLeftPos.FCost = tUpLeftPos.GCost + tUpLeftPos.HCost;
            cOpenList.Add(tUpLeftPos);
        }

        // UpRight position of player
        Node tUpRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z + GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpRightPos.Position))
        {
            tUpRightPos.GCost = mCostCalculater.CalculateGCost(tUpRightPos);
            tUpRightPos.HCost = mCostCalculater.CalculateHCost(tUpRightPos, pPosToFindRoadTo, GridExpandNumber);
            tUpRightPos.FCost = tUpRightPos.GCost + tUpRightPos.HCost;
            cOpenList.Add(tUpRightPos);
        }

        // Down position of player
        Node tDownPos = new Node()
        {
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownPos.Position))
        {
            tDownPos.GCost = mCostCalculater.CalculateGCost(tDownPos);
            tDownPos.HCost = mCostCalculater.CalculateHCost(tDownPos, pPosToFindRoadTo, GridExpandNumber);
            tDownPos.FCost = tDownPos.GCost + tDownPos.HCost;
       
            cOpenList.Add(tDownPos);
        }

        // DownLeft position of player
        Node tDownLeftPos = new Node()
        {
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x - GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownLeftPos.Position))
        {
            tDownLeftPos.GCost = mCostCalculater.CalculateGCost(tDownLeftPos);
            tDownLeftPos.HCost = mCostCalculater.CalculateHCost(tDownLeftPos, pPosToFindRoadTo, GridExpandNumber);
            tDownLeftPos.FCost = tDownLeftPos.GCost + tDownLeftPos.HCost;
            cOpenList.Add(tDownLeftPos);
        }

        // DownRight position of player
        Node tDownRightPos = new Node()
        {
            GCost = 0,
            Parent = tNodeSquaresAround,
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y, tNodeSquaresAround.Position.z - GridExpandNumber)
        };
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownRightPos.Position))
        {
            tDownRightPos.GCost = mCostCalculater.CalculateGCost(tDownRightPos);
            tDownRightPos.HCost = mCostCalculater.CalculateHCost(tDownRightPos, pPosToFindRoadTo, GridExpandNumber);
            tDownRightPos.FCost = tDownRightPos.GCost + tDownRightPos.HCost;
            cOpenList.Add(tDownRightPos);
        }

    }

    public List<Node> FindFastestRoadToPoint(Vector3 pStartPos,Vector3 pPosToFindRoadTo)
    {
        cOpenList = new List<Node>();
        cClosedList = new List<Node>();

        mCurrentNode = new Node()
        {
            GCost = 0,
            Parent = null,
            Position = pStartPos
        };
        cOpenList.Add(mCurrentNode);

        int tTimesToRun = 50;
        int tCount = 0;

       

        while(tCount < tTimesToRun)
        {
            tCount++;

            AddSquaresToOpenListAroundYou(mCurrentNode, pPosToFindRoadTo);


            // Find lowest Fvalue in openlist
            Node tLowestFNode = null;
            float LowestF = float.MaxValue;
            foreach (Node tNode in cOpenList)
            {
                if (tNode.FCost < LowestF)
                {
                    LowestF = tNode.FCost;
                    tLowestFNode = tNode;
                }

            }
            // Add the lowest fValue to closedlist and remove it from the openlist.
            cOpenList.Remove(tLowestFNode);
            cClosedList.Add(tLowestFNode);
            mCurrentNode = tLowestFNode;

            if(mCurrentNode == null || Vector3.Distance(mCurrentNode.Position,pPosToFindRoadTo) < GridExpandNumber)
            {
               return cClosedList;
            }
          
        }

        

       
       
        return cClosedList;

    }

    

}
