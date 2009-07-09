using UnityEngine;
using System;
using System.Collections;

public class Peabrain : PeabrainNode {
	
	public enum Status {
		Success, Continue, Failure, Exception
	}
	
	public Actionable actor;
	public object lastDelegateResult;
	
	public PeabrainNode rootNode;
	public PeabrainNode currentNode;
	public ArrayList currentFilters = new ArrayList();
	
	public Peabrain(Actionable actor) {
		this.actor = actor;
	}
	
	public void Think() {
		
		// Run filters
		if (currentFilters.Count > 0) {
			for (int f = 0; f < currentFilters.Count; f++) {
				if (((PeabrainFilter) currentFilters[f]).Filter()) {
					//PeabrainNode filterParent = ((PeabrainNode) currentFilters[f]).parent;
					PeabrainFilter filter = (PeabrainFilter) currentFilters[f];
					
					// Remove this and all later filters from stack
					currentFilters.RemoveRange(f, currentFilters.Count - f);
					
					filter.OnFilter();
					
					// Move action to node just above filter
					//filterParent.OnChildFailure();
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
	
	public override void OnChildSuccess() { currentNode = rootNode; }
	public override void OnChildFailure() { currentNode = rootNode; }
	public override void OnChildException() { currentNode = rootNode; }
}
