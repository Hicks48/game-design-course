using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	private List<InventoryItem> items;

	void Start () {
		this.items = new List<InventoryItem> ();
	}
	
	void Update () {
		
	}

	public InventoryItem getItemOfType(string id) {
		IEnumerator enumerator = this.items.GetEnumerator ();

		while(enumerator.MoveNext()) {
			InventoryItem item = (InventoryItem)enumerator.Current;

			if(item.id.Equals(id)) {
				return item;
			}
		}

		return null;
	}
}
