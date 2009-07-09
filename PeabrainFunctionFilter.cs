using UnityEngine;
using System.Collections;

public delegate bool BoolDelegate();

public class PeabrainFunctionFilter : PeabrainFilter {
	
	public BoolDelegate function;
		
	public override bool Filter() {
		return (function != null && function());
	}
	
}
