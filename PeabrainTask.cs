using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class PeabrainTask : PeabrainNode {

	private string methodName;
	private MethodInfo method;
	private object[] methodParams;
	
	public override void Execute() {
		if (method != null) {
			switch ((int) method.Invoke(this.brain.actor, methodParams)) {
			case 1:
				parent.OnChildSuccess();
				break;
			case 0:
				brain.currentNode = this;
				break;
			case -1:
				parent.OnChildFailure();
				break;
			case -2:
				parent.OnChildException();
				break;
			}
		}
		else
			parent.OnChildException();
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);

		methodName = (string) hash["method"];
		if (methodName != null) {
			Type actorType = this.brain.ActorType();
			method = actorType.GetMethod(methodName);
			//methodParams = hash["params"];
		}
		else
			Debug.Log("[PeabrainTask] Warning: no method found in task definition with description: " + description);
	}
}
