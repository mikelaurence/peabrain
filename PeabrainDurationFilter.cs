using UnityEngine;
using System.Collections;

public class PeabrainDurationFilter : PeabrainFilter {

	public float baseDuration;
	public float duration;
	public float random;
	private float start = 0;
	
	public override bool Filter() {
		return ((Time.realtimeSinceStartup - start) > duration);
	}
	
	public override void Reset() {
		start = Time.realtimeSinceStartup;
		duration = baseDuration + Random.Range(0, random);
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		baseDuration = (float) hash["duration"];
		random = (float) hash["random"];
	}
}
