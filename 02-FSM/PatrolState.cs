using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : FSMState {

	private List<Transform> path = new List<Transform> ();
	private int index;

	public PatrolState(FSMSystem fsm):base(fsm)
	{
		stateID = StateID.Patrol;
		Transform pathTransform = GameObject.Find ("Path").transform;
		Transform[] children = pathTransform.GetComponentsInChildren<Transform> ();
		foreach (var item in children) {
			if (item != pathTransform) {
				path.Add (item);
			}
		}
	}

	public override void Act (GameObject npc)
	{
		npc.transform.LookAt (path [index].position);
		npc.transform.Translate (Vector3.forward * Time.deltaTime * 3);
		if(Vector3.Distance(npc.transform.position,path[index].position)<1){
			index++;
			index %= path.Count;
		}
	}
	public override void Reason (GameObject npc)
	{
//		throw new System.NotImplementedException ();
	}

}
