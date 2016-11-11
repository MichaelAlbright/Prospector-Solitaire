using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prospector : MonoBehaviour {
	static public Prospector S;

	public Deck deck;
	public TextAsset deckXML;

	public Layout layout;
	public TextAsset layoutXML;
	public Vector3 layoutCenter;
	public float xOffset = 3;
	public float yOffset = -2.5f;
	public Transform layoutAnchor;

	public CardProspector target;
	public List<CardProspector> tableau;
	public List<CardProspector> discardPile;

	void Awake ()
	{
		S = this;
	}

	public List<CardProspector> drawPile;

	void Start ()
	{
		deck = GetComponent<Deck> ();
		deck.InitDeck (deckXML.text);
//		Deck.Shuffle (ref deck.cards);

		layout = GetComponent<Layout> ();
		layout.ReadLayout (layoutXML.text);

		drawPile = ConvertListCardsToLostCardProspectors (deck.cards);
		LayoutGame ();
	}

	CardProspector Draw()
	{
		CardProspector cd = drawPile [0];
		drawPile.RemoveAt (0);
		return(cd);
	}

	void LayoutGame()
	{
		if (layoutAnchor == null) {
			GameObject tGO = new GameObject ("_LayoutAnchor");
			layoutAnchor = tGO.transform;
			layoutAnchor.transform.position = layoutCenter;
		}

		CardProspector cp;
		foreach (SlotDef tSD in layout.slotDefs) {
			cp = Draw ();
			cp.faceUp = tSD.faceUp;
			cp.transform.parent = layoutAnchor;
			cp.transform.localPosition = new Vector3 (
				layout.multiplier.x * tSD.x,
				layout.multiplier.y * tSD.y,
				-tSD.layerID);
			cp.layoutID = tSD.id;
			cp.slotDef = tSD;
			cp.state = CardState.tableau;

			cp.SetSortingLayerName (tSD.layerName);

			tableau.Add (cp);
		}

		MoveToTarget (Draw ());
		UpdateDrawPile ();
	}

	List<CardProspector> ConvertListCardsToLostCardProspectors(List<Card> lCD)
	{
		List<CardProspector> lCP = new List<CardProspector> ();
		CardProspector tCP;
		foreach (Card tCD in lCD) {
			tCP = tCD as CardProspector;
			lCP.Add (tCP);
		}
		return(lCP);
	}

	public void CardClicked(CardProspector cd)
	{
		switch (cd.state) {
		case CardState.target:
			break;
		case CardState.drawpile:
			MoveToDiscard (target);
			MoveToTarget (Draw ());
			UpdateDrawPile ();
			break:
		case CardState.tableau:
			break;
		}
	}

	void MoveToDiscard(CardProspector cd)
	{
		cd.state = CardState.discard;
		discardPile.Add (cd);
		cd.transform.parent = layoutAnchor;
		cd.transform.localPosition = new Vector3 (
			layout.multiplier.x * layout.discardPile.x,
			layout.multiplier.y * layout.discardPile.y,
			-layout.discardPile.layerID + 0.5f);
		cd.faceUp = true;
		cd.SetSortingLayerName (layout.discardPile.layerName);
		cd.SetSortOrder (-100 + discardPile.Count);
	}

	void MoveToTarget(CardProspector cd)
	{
		if (target != null)
			MoveToDiscard (target);
		target = cd;
		cd.state = CardState.target;
		cd.transform.parent = layoutAnchor;
		cd.transform.localPosition = new Vector3 (
			layout.multiplier.x * layout.discardPile.x,
			layout.multiplier.y * layout.discardPile.y,
			-layout.discardPile.layerID);
		cd.faceUp = true;
		cd.SetSortingLayerName (layout.discardPile.layerName);
		cd.SetSortOrder (0);
	}
}
