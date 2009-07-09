using System;

public interface Actionable {
	object Action(object action, object parameters);
	Type ActionEnum();
}