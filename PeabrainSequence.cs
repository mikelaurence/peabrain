using UnityEngine;
using System.Collections;

public class PeabrainSequence : PeabrainNode {

	private int currentBranch = -1;
	
	public override void Execute() {
		currentBranch += 1;
		if (currentBranch >= children.Length) {
			currentBranch = -1;
			parent.OnChildSuccess();
		}
		else
			children[currentBranch].Execute();
	}
	
	public override void OnChildSuccess() {
		Execute();
	}
	
	public override void OnChildFailure() {
		currentBranch = -1;
		parent.OnChildFailure();
	}
	
	public override void OnChildException() {
		currentBranch = -1;
		parent.OnChildException();
	}
}
