using UnityEngine;
using System.Collections;

public class PeabrainDurationFilter : PeabrainFilter {

	public float duration;
	private float start;
	
	public override bool Filter() {
		return ((Time.realtimeSinceStartup - start) < duration);
	}
	
	public override void Reset() {
		start = Time.realtimeSinceStartup;
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		duration = (float) hash["duration"];
		Reset();
	}
}
