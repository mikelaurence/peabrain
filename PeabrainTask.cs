using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class PeabrainTask : PeabrainNode {
	
	public override void Execute() {
		if (action != null) {
			switch ((Peabrain.Status) InvokeDelegate()) {
			case Peabrain.Status.Success:
				parent.OnChildSuccess();
				break;
			case Peabrain.Status.Continue:
				brain.currentNode = this;
				break;
			case Peabrain.Status.Failure:
				parent.OnChildFailure();
				break;
			case Peabrain.Status.Exception:
				parent.OnChildException();
				break;
			}
		}
		else
			parent.OnChildException();
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		base.ConfigureDelegateFromJSONHash(hash, brain);
	}
}
