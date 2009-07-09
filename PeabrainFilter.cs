using UnityEngine;
using System;
using System.Collections;

public class PeabrainFilter : PeabrainNode {

	protected Peabrain.Status onFilterAction = Peabrain.Status.Failure;

	public override void Execute() {
		Reset();
		if (children.Length > 0) {
			if (!Filter()) {
				brain.currentFilters.Add(this);
				children[0].Execute();
				return;
			}
		}
		OnFilter();
	}
	
	public virtual bool Filter() {
		return false;
	}
	
	public virtual void Reset() {}
	
	public void Pop() {
		brain.currentFilters.Remove(this);
	}
	
	public void OnFilter() {
		switch (onFilterAction) {
		case Peabrain.Status.Success: parent.OnChildSuccess(); break;
		case Peabrain.Status.Failure: parent.OnChildFailure(); break;
		case Peabrain.Status.Exception: parent.OnChildException(); break;
		}
	}
	
	public override void OnChildSuccess() {
		Pop();
		parent.OnChildSuccess();
	}
	
	public override void OnChildFailure() {
		Pop();
		parent.OnChildFailure();
	}
	
	public override void OnChildException() {
		Pop();
		parent.OnChildException();	
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		string onFilterString = (string) hash["onFilter"];
		if (onFilterString != null)
			onFilterAction = (Peabrain.Status) Enum.Parse(typeof(Peabrain.Status), onFilterString);
	}
}
