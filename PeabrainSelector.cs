using UnityEngine;
using System.Collections;

public class PeabrainSelector : PeabrainNode {
	
	private int currentBranch = -1;
	
	public override void Execute() {
		currentBranch += 1;
		if (currentBranch >= children.Length) {
			currentBranch = -1;
			parent.OnChildFailure();
		}
		else
			children[currentBranch].Execute();
	}
	
	public override void OnChildSuccess() {
		currentBranch = -1;
		parent.OnChildSuccess();
	}
	
	public override void OnChildFailure() {
		Execute();
	}
	
	public override void OnChildException() {
		Execute();
	}
}
