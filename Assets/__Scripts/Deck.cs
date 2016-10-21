using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public bool _________________;

	public PT_XMLReader xmlr;
	public List<string> cardNames;
	public List<Card> cards;
	public List<Decorator> decorators;
	public List<CardDefinition> cardDefs;
	public Transform deckAnchor;
	public Dictionary<string,Sprite> dictSuits;

	public void InitDeck(string deckXMLText)
	{
		ReadDeck (deckXMLText);
	}

	public void ReadDeck (string deckXMLText)
	{
		xmlr = new PT_XMLReader ();
		xmlr.Parse (deckXMLText);

		string s = "xml[0] decorator[0] ";
		s += "type=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("type");
		s += " x=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("x");
		s += " y=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("y");
		s += " scale=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("scale");
//		print (s);

		decorators = new List<Decorator> ();
		PT_XMLHashList xDecos = xmlr.xml ["xml"] [0] ["decorator"];
		Decorator deco;
		for (int i = 0; i < xDecos.Count; i++) {
			deco = new Decorator ();
			deco.type = xDecos [i].att ("type");
			deco.flip = (xDecos [i].att ("flip") == "1");
			deco.scale = float.Parse (xDecos [i].att ("scale"));
			deco.loc.x = float.Parse (xDecos [i].att ("x"));
			deco.loc.y = float.Parse (xDecos [i].att ("y"));
			deco.loc.z = float.Parse (xDecos [i].att ("z"));
			decorators.Add (deco);
		}

		cardDefs = new List<CardDefinition> ();
		PT_XMLHashList xCardDefs = xmlr.xml ["xml"] [0] ["card"];
		for (int i = 0; i < xCardDefs.Count; i++) {

		}
	}
}
