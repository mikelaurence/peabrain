using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class PeabrainNode {
	
	public Peabrain brain;
	public PeabrainNode parent;
	public PeabrainNode[] _children;
	public string description;
	
	protected bool isExecuting = false;
	
	public virtual void Execute() {}
	public virtual void OnChildSuccess() {}
	public virtual void OnChildFailure() {}
	public virtual void OnChildException() {}
	
	public PeabrainNode[] children {
		set {
			foreach (PeabrainNode child in value) {
				child.brain = brain;
				child.parent = this;
			}
			_children = value;
		}
		get {
			return _children;
		}
	}
	
	protected static PeabrainNode NodeFromJSONHash(Hashtable hash, Peabrain brain) {
		Assembly assem = Assembly.GetExecutingAssembly();
		string baseType = (String) hash["type"];
		Type type = assem.GetType("Peabrain" + char.ToUpper(baseType[0]) + baseType.Substring(1, baseType.Length - 1));
		PeabrainNode node = (PeabrainNode) Activator.CreateInstance(type);
		node.ConfigureFromJSONHash(hash, brain);
		return node;
	}
	
	protected virtual void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		this.brain = brain;

		ArrayList childrenJson = (ArrayList) hash["children"];
		if (childrenJson != null) {
			PeabrainNode[] nodes = new PeabrainNode[childrenJson.Count];
			for (int c = 0; c < childrenJson.Count; c++) {
				nodes[c] = NodeFromJSONHash((Hashtable) childrenJson[c], brain);
			}
			children = nodes;
		}
		
		description = (string) hash["description"];
	}
}
