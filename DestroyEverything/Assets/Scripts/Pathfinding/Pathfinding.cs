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

    public void FindFastestRoadToPoint(Vector3 pStartPos,Vector3 pPosToFindRoadTo)
    {
        cOpenList = new List<Node>();
        cClosedList = new List<Node>();
        mCurrentNode = new Node()
                           {
                               GCost = 0,
                               Parent = null,
                               Position = pStartPos
                           };

        
            // Right position of player
        Node tRightPos = new Node()
                             {
                                 GCost = 0,
                                Parent = mCurrentNode,
                                 Position =
                                     new Vector3(mCurrentNode.Position.x + GridExpandNumber, mCurrentNode.Position.y,
                                                 mCurrentNode.Position.z)
                             };
        
        if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tRightPos.Position))
        {
            tRightPos.GCost = mCostCalculater.CalculateGCost(tRightPos);
            tRightPos.HCost = mCostCalculater.CalculateHCost(tRightPos,pPosToFindRoadTo,GridExpandNumber);
           cOpenList.Add(tRightPos);
        }
           

            // Left position of player
             Node tLeftPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x - GridExpandNumber,mCurrentNode.Position.y,mCurrentNode.Position.z)
        };
         if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tLeftPos.Position))
         {
             tLeftPos.GCost = mCostCalculater.CalculateGCost(tLeftPos);
             cOpenList.Add(tLeftPos);
         }

            // Up position of player
             Node tUpPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x,mCurrentNode.Position.y,mCurrentNode.Position.z+GridExpandNumber)
        };
         if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpPos.Position))
         {
             tUpPos.GCost = mCostCalculater.CalculateGCost(tUpPos);
             cOpenList.Add(tUpPos);
         }

            // UpLeft position of player
            Node tUpLeftPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x - GridExpandNumber,mCurrentNode.Position.y,mCurrentNode.Position.z+GridExpandNumber)
        };
          if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpLeftPos.Position))
          {
              tUpLeftPos.GCost = mCostCalculater.CalculateGCost(tUpLeftPos);
              cOpenList.Add(tUpLeftPos);
          }

            // UpRight position of player
            Node tUpRightPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x + GridExpandNumber,mCurrentNode.Position.y,mCurrentNode.Position.z + GridExpandNumber)
        };
          if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpRightPos.Position))
          {
              tUpRightPos.GCost = mCostCalculater.CalculateGCost(tUpRightPos);
              cOpenList.Add(tUpRightPos);
          }

            // Down position of player
             Node tDownPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x,mCurrentNode.Position.y,mCurrentNode.Position.z - GridExpandNumber)
        };
          if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownPos.Position))
          {
              tDownPos.GCost = mCostCalculater.CalculateGCost(tDownPos);
              cOpenList.Add(tDownPos);
          }

            // DownLeft position of player
            Node tDownLeftPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x - GridExpandNumber,mCurrentNode.Position.y,mCurrentNode.Position.z - GridExpandNumber)
        };
          if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownLeftPos.Position))
          {
              tDownLeftPos.GCost = mCostCalculater.CalculateGCost(tDownLeftPos);
              cOpenList.Add(tDownLeftPos);
          }

            // DownRight position of player
            Node tDownRightPos =  new Node(){
                GCost = 0,
                Parent = mCurrentNode,
                Position = new Vector3(mCurrentNode.Position.x + GridExpandNumber,mCurrentNode.Position.y,mCurrentNode.Position.z - GridExpandNumber)
        };
          if(!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownRightPos.Position))
          {
              tDownRightPos.GCost = mCostCalculater.CalculateGCost(tDownRightPos);
              cOpenList.Add(tDownRightPos);
          }

        cClosedList.Add(mCurrentNode);

        }

    

}
