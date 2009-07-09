using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class PeabrainDelegateFilter : PeabrainFilter {
		
	public override bool Filter() {
		return (action != null && ((bool) InvokeDelegate() == false));
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		base.ConfigureDelegateFromJSONHash(hash, brain);
	}	
}
