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
	
	// Method (delegate) calling, used in several node types
	protected object action;
	//protected string methodName;
	//protected MethodInfo method;
	protected object methodParams;
	
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
	
	protected object InvokeDelegate() {
		//this.brain.lastDelegateResult = null;
		//this.brain.actor.SendMessage(methodName);
		//return this.brain.lastDelegateResult;
		return this.brain.actor.Action(this.action, null);
	}
	
	protected static PeabrainNode NodeFromJSONHash(Hashtable hash, Peabrain brain) {

		string baseType = (String) hash["type"];
		Debug.Log("Trying base type " + baseType);
		//Type type = assem.GetType("Peabrain" + char.ToUpper(baseType[0]) + baseType.Substring(1, baseType.Length - 1));
		//Type type = Type.GetType("Peabrain" + char.ToUpper(baseType[0]) + baseType.Substring(1, baseType.Length - 1));
		//PeabrainNode node = (PeabrainNode) Activator.CreateInstance(type);
		
		// Unfortunately verbose code for instantiating via string (when using iPhone code stripping)
		PeabrainNode node = null;
		switch (baseType) {
			case "DelegateFilter": 
				node = new PeabrainDelegateFilter(); break;
			case "DurationFilter":
				node = new PeabrainDurationFilter(); break;
			case "ProbabilityFilter":
				node = new PeabrainProbabilityFilter(); break;
			case "Router":
				node = new PeabrainRouter(); break;
			case "Selector":
				node = new PeabrainSelector(); break;
			case "Sequence":
				node = new PeabrainSequence(); break;
			case "Task":
				node = new PeabrainTask(); break;
		}
		
		if (node != null) {		
			Debug.Log("Configuring type " + baseType);
			node.ConfigureFromJSONHash(hash, brain);
			Debug.Log("Done with " + baseType);
		}
		
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
	
	protected bool ConfigureDelegateFromJSONHash(Hashtable hash, Peabrain brain) {
		string actionString = (string) hash["action"];
		if (actionString != null) {
			action = Enum.Parse(this.brain.actor.ActionEnum(), actionString);
			return true;
		}
		else {
			Debug.Log("[" + GetType() + "] Warning: no action defined");
			return false;
		}
	}
}
