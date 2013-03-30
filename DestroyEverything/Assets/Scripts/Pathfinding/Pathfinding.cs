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
    public float TimeOut;

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
            Position = new Vector3(tNodeSquaresAround.Position.x + GridExpandNumber, tNodeSquaresAround.Position.y,tNodeSquaresAround.Position.z)
        };

        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tRightPos.Position) && !IsNodeInClosedOrOpenList(tRightPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tLeftPos.Position) && !IsNodeInClosedOrOpenList(tLeftPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpPos.Position) && !IsNodeInClosedOrOpenList(tUpPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpLeftPos.Position) && !IsNodeInClosedOrOpenList(tUpLeftPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tUpRightPos.Position) && !IsNodeInClosedOrOpenList(tUpRightPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownPos.Position) && !IsNodeInClosedOrOpenList(tDownPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownLeftPos.Position) && !IsNodeInClosedOrOpenList(tDownLeftPos))
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
        if (!mCollisionDetector.IsPointCollidingWithUnWalkable(tDownRightPos.Position) && !IsNodeInClosedOrOpenList(tDownRightPos))
        {
            tDownRightPos.GCost = mCostCalculater.CalculateGCost(tDownRightPos);
            tDownRightPos.HCost = mCostCalculater.CalculateHCost(tDownRightPos, pPosToFindRoadTo, GridExpandNumber);
            tDownRightPos.FCost = tDownRightPos.GCost + tDownRightPos.HCost;
            cOpenList.Add(tDownRightPos);
        }

    }

   bool BetterPathGCost(Node pNode)
   {
       bool tBetterPath = false;
        Node tNode = pNode;
        int tCost = mCurrentNode.GCost;
        if (tNode.Position.x < mCurrentNode.Position.x || tNode.Position.x > mCurrentNode.Position.x ||
             tNode.Position.z < mCurrentNode.Position.z || tNode.Position.z > mCurrentNode.Position.z)
        {
            tCost += 10;
        }
        if (tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z > tNode.Parent.Position.z ||
            tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x < tNode.Parent.Position.x ||
            tNode.Position.x > tNode.Parent.Position.x && tNode.Position.z < tNode.Parent.Position.z ||
            tNode.Position.z < tNode.Parent.Position.z && tNode.Position.x > tNode.Parent.Position.x)
        {
            tCost += 14;
        }

       if(tNode.GCost > tCost)
       {
           tBetterPath = true;
       }

       return tBetterPath;
    }

    bool IsNodeInClosedOrOpenList(Node pNodeToCheck)
    {
        bool tIsNodeInClosedOrOpenList = false;

        foreach (Node tClosedNode in cClosedList)
        {
            if(Vector3.Distance(pNodeToCheck.Position,tClosedNode.Position) < (GridExpandNumber/2))
            {
                tIsNodeInClosedOrOpenList = true;
            }
        }

        foreach (Node tOpenNode in cOpenList)
        {
            if (Vector3.Distance(pNodeToCheck.Position, tOpenNode.Position) < (GridExpandNumber /2))
            {
                if(BetterPathGCost(tOpenNode))
                {
                    tOpenNode.Parent = mCurrentNode;
                }
                tIsNodeInClosedOrOpenList = true;
            }
        }

        return tIsNodeInClosedOrOpenList;
    }

    public List<Node> FindFastestRoadToPoint(Vector3 pStartPos,Vector3 pPosToFindRoadTo)
    {
        cOpenList = new List<Node>();
        cClosedList = new List<Node>();

        UnWalkable tUnWalkableCollidingWith =
            mCollisionDetector.IsPointCollidingWithUnWalkableAndGetUnwalkable(pPosToFindRoadTo);

        if(tUnWalkableCollidingWith != null)
        {
            if (tUnWalkableCollidingWith.Postion.x >= pPosToFindRoadTo.x)
            {
                pPosToFindRoadTo.x -= tUnWalkableCollidingWith.LossyScale.x;
            }

            else if (tUnWalkableCollidingWith.Postion.x < pPosToFindRoadTo.x)
            {
                pPosToFindRoadTo.x += tUnWalkableCollidingWith.LossyScale.x;
            }
          
        }

      

    mCurrentNode = new Node()
        {
            Parent = null,
            Position = pStartPos
        };
        cOpenList.Add(mCurrentNode);

        int tTimesToRun = 5000;
        int tCount = 0;

        float tTimeOut = Time.time + TimeOut;
        bool run = true;
        while (tCount < tTimesToRun)
        {
            tCount++;
            AddSquaresToOpenListAroundYou(mCurrentNode, pPosToFindRoadTo);
          
            if (cOpenList.Count <= 0)
            {
                run = false;
                return MakeListToFollow();
            }

            // Find lowest Fvalue in openlist
            Node tLowestFNode = null;
            float LowestF = float.MaxValue;
            foreach (Node tNode in cOpenList)
            {
                if (tNode.FCost <= LowestF)
                {
                    LowestF = tNode.FCost;
                    tLowestFNode = tNode;
                }
            }
        

            // Add the lowest fValue to closedlist and remove it from the openlist.
            cOpenList.Remove(tLowestFNode);
            cClosedList.Add(tLowestFNode);
            mCurrentNode = tLowestFNode;

            

         

            if(mCurrentNode == null || Mathf.Abs(mCurrentNode.Position.x - pPosToFindRoadTo.x) < GridExpandNumber  &&
                Mathf.Abs(mCurrentNode.Position.z - pPosToFindRoadTo.z) < GridExpandNumber)
            {
                run = false;
                return MakeListToFollow();
            }

       

            if((tTimeOut) < Time.time)
            {
                run = false;
                return MakeListToFollow();
            }
        }

        return MakeListToFollow();


    }

    List<Node> MakeListToFollow()
    {
        List<Node> tListToFollow = new List<Node>();
        Node tNode = cClosedList.ToArray()[cClosedList.Count - 1];
       
        while (tNode.Parent != null)
        {
            tListToFollow.Add(tNode);
            tNode = tNode.Parent;
        }

        return tListToFollow;
    }

    void OnGUI()
    {
        
    }
    

}
