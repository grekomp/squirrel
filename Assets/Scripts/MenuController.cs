using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject[] menuItems;
	public int currentItem;
	public GameObject arrow;
	public Vector3 arrowOffset;
	float lastAxis;
	

	void Start () {
		arrow = GameObject.Find("Arrow");
		
	}
	
	void Update () {
		if (lastAxis != Input.GetAxisRaw("Vertical"))
		{


			if (Input.GetAxisRaw("Vertical") < 0)
			{
				currentItem++;
				if (currentItem >= menuItems.Length)
				{
					currentItem = 0;
				}
			}

			if (Input.GetAxisRaw("Vertical") > 0)
			{
				currentItem--;
				if (currentItem < 0)
				{
					currentItem = menuItems.Length - 1;
				}
			}

			lastAxis = Input.GetAxisRaw("Vertical");
			MoveArrow(menuItems[currentItem].GetComponent<RectTransform>());
		}

		if (Input.GetButtonDown("Submit"))
		{
			menuItems[currentItem].GetComponent<MenuItem>().Activate();
		}
	}

	public void MoveArrow(RectTransform target)
	{
		Vector3[] corners = new Vector3[4];
		target.GetWorldCorners(corners);
		arrow.GetComponent<Arrow>().targetPosition = (corners[0] + corners[1]) / 2 + arrowOffset * transform.localScale.x;

	} 

	public void SetCurrentItem(int index)
	{
		currentItem = index;
		MoveArrow(menuItems[currentItem].GetComponent<RectTransform>());
	}

}
