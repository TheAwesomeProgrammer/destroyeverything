using UnityEngine;
using System.Collections.Generic;

public class ClickShow : MonoBehaviour
{

    public GameObject ShowClick;

    public float TimeToShowClick;

    private bool mAnyPlayerSelected;

	// Use this for initialization
	void Start ()
	{
	    mAnyPlayerSelected = false;
	}
	
    void SetAnyPlayerSelected(bool pPlayerSelected)
    {
        mAnyPlayerSelected = pPlayerSelected;
    }

    
	// Update is called once per frame
	void Update () {
	if(Input.GetMouseButtonDown(1))
	{
	    Clicked();
	}
	}

    void Clicked()
    {
      if(mAnyPlayerSelected)
      {
          Vector3 tMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          tMousePosition.y = transform.position.y - 1;
          GameObject tShowClick = Instantiate(ShowClick, tMousePosition, Quaternion.identity) as GameObject;
          Destroy(tShowClick, TimeToShowClick);
      }
           
        
        
    }
}
