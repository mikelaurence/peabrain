using UnityEngine;
using System.Collections;

/*
	Executes a single child cross-referenced by index with a values array. The
	value used is generated by calling the delegate method.
*/
public class PeabrainRouter : PeabrainNode {
	
	private ArrayList values;
	
	public override void Execute() {
		int index = values.IndexOf(InvokeDelegate());
		if (index >= 0 && index < values.Count)
			children[index].Execute();
		else
			parent.OnChildFailure();
	}
	
	public override void OnChildSuccess() {
		parent.OnChildSuccess();
	}
	
	public override void OnChildFailure() {
		parent.OnChildFailure();
	}
	
	public override void OnChildException() {
		parent.OnChildException();
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		if (base.ConfigureDelegateFromJSONHash(hash, brain)) {
			values = (ArrayList) hash["values"];
			if (values == null)
				Debug.Log("[PeabrainRouter] Warning: router has no values with which to route");
		}
	}
}