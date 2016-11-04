using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SlotDef
{
	public float x;
	public float y;
	public bool faceUp = false;
	public string layerName = "Default";
	public int layerID = 0;
	public int id;
	public List<int> hiddenBy = new List<int> ();
	public string type = "slot";
	public Vector2 stagger;
}

public class Layout : MonoBehaviour {
	public PT_XMLReader xmlr;
	public PT_XMLHashtable xml;
	public Vector2 multiplier;
	public List<SlotDef> slotDefs;
	public SlotDef drawPile;
	public SlotDef discardPile;
	public string[] sortingLayerNames = new string[] { "Row0", "Row1", "Row2", "Row3", "Discard", "Draw" };

	public void ReadLayout(string xmlText)
	{
		xmlr = new PT_XMLReader ();
		xmlr.Parse (xmlText);
		xml = xmlr.xml ["xml"] [0];

		multiplier.x = float.Parse (xml ["multiplier"] [0].att ("x"));
		multiplier.y = float.Parse (xml ["multiplier"] [0].att ("y"));

		SlotDef tSD;
		PT_XMLHashList slotsX = xml ["slot"];

		for (int i = 0; i < slotsX.Count; i++) {
			tSD = new SlotDef ();
			if (slotsX [i].HasAtt ("type")) {
				tSD.type = slotsX [i].att ("type");
			} else {
				tSD.type = "slot";
			}
			tSD.x = float.Parse (slotsX [i].att ("x"));
			tSD.y = float.Parse (slotsX [i].att ("y"));
			tSD.layerID = int.Parse (slotsX [i].att ("layer"));
			tSD.layerName = sortingLayerNames = sortingLayerNames[tSD.layerID]:
		}
	}
}
