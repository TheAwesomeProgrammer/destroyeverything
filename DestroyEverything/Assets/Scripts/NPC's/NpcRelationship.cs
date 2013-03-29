using UnityEngine;
using System.Collections;

public enum Relationship
{
    Good,
    Bad
}

[System.Serializable]
public class FeelingOfOther
{
    public string Name;
    public Relationship Relationship;
}

public class NpcRelationship : MonoBehaviour
{
   public FeelingOfOther[] Relationsships;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
