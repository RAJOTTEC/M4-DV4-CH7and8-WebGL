using UnityEngine;

public class ShopPiece : MonoBehaviour
{
	[SerializeField]
	SOShopSelection shopSelection;
	public SOShopSelection ShopSelection
	{
		get { return shopSelection; }
		set { shopSelection = value; }
	}

	void Awake()
	{
		//icon slot
		if (GetComponentInChildren<SpriteRenderer>() != null)
		{
			GetComponentInChildren<SpriteRenderer>().sprite = shopSelection.icon;
		}

		//selection value
		if (transform.Find("itemText"))
		{
			GetComponentInChildren<TextMesh>().text = shopSelection.cost;
		}
	}
}