using UnityEngine;
using System.Collections;

public class PeabrainProbabilityFilter : PeabrainFilter{

	public float probability;
	
	public override bool Filter() {
		return (Random.value < probability);
	}
	
	protected override void ConfigureFromJSONHash(Hashtable hash, Peabrain brain) {
		base.ConfigureFromJSONHash(hash, brain);
		probability = (float) hash["probability"];
	}
}
