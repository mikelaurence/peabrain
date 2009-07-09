using UnityEngine;
using System.Collections;

public class PeabrainFilter : PeabrainNode {

	public override void Execute() {
		if (children.Length > 0) {
			if (!Filter()) {
				brain.currentFilters.Add(this);
				children[0].Execute();
				Reset();
				return;
			}
		}
		parent.OnChildFailure();
	}
	
	public virtual bool Filter() {
		return false;
	}
	
	public virtual void Reset() {}
	
	public void Pop() {
		brain.currentFilters.Remove(this);	
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
}
