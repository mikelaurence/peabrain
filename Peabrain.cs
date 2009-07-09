using UnityEngine;
using System;
using System.Collections;

public class Peabrain : PeabrainNode {
	
	public object actor;
	
	public PeabrainNode rootNode;
	public PeabrainNode currentNode;
	public ArrayList currentFilters = new ArrayList();
	
	public Peabrain(object actor) {
		this.actor = actor;
	}
	
	public void Think() {
		
		// Run filters
		if (currentFilters.Count > 0) {
			for (int f = 0; f < currentFilters.Count; f++) {
				if (((PeabrainFilter) currentFilters[f]).Filter()) {
					
					PeabrainNode filterParent = ((PeabrainNode) currentFilters[f]).parent;
					
					// Remove this and all later filters from stack
					currentFilters.RemoveRange(f, currentFilters.Count - f + 1);
					filterParent.OnChildFailure();
				}
			}
		}
		
		if (currentNode == null)
			currentNode = rootNode;
		
		currentNode.Execute();
	}
	
	public Type ActorType() {
		return actor.GetType();
	}
	
	public void LoadFromJSON(string jsonString) {
		Hashtable hash = (Hashtable) JSON.JsonDecode(jsonString);
		children = new PeabrainNode[]{ NodeFromJSONHash(hash, this) };
		rootNode = children[0];
	}
}
