using UnityEngine;
using System.Collections;

public enum Size
{
    Small,
    Medium,
    Large
}


public class Player : MonoBehaviour
{

    public float GrowTime;

    public float[] Sizes;

    private Size mSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	SetSize();
	}

    void SetSize()
    {
        if (transform.lossyScale.x > Sizes[0] && transform.lossyScale.x < Sizes[1])
        {
            mSize = Size.Small;
        }
        else if (transform.lossyScale.x > Sizes[1] && transform.lossyScale.x < Sizes[2])
        {
            mSize = Size.Medium;
        }
        else if (transform.lossyScale.x > Sizes[2])
        {
            mSize = Size.Large;
        }
    }

    public void Grow(Vector3 pAmount)
    {
        iTween.ScaleAdd(gameObject,pAmount,GrowTime);
    }
}
