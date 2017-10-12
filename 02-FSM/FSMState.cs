using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//转换条件
public enum Transition{
	NullTransiton=0,
}

//状态id
public enum StateID{
	NullStateId=0,
	Patrol=1,
}

public abstract class FSMState{

	protected StateID stateID;
	public StateID ID{get { return stateID;}}
	protected Dictionary<Transition,StateID> map = new Dictionary<Transition, StateID> ();
	protected FSMSystem fsm;

	public FSMState(FSMSystem fsm){
		this.fsm = fsm;
	}

	/// <summary>
	/// 添加切换条件
	/// </summary>
	/// <param name="trans">条件.</param>
	/// <param name="id">状态id.</param>
	public void AddTranstion(Transition trans,StateID id){
		if(trans==Transition.NullTransiton){
			Debug.LogError ("不允许NullTransiton");return;
		}
		if (id == StateID.NullStateId) {
			Debug.LogError ("不允许NullStateId");return;
		}
		if(map.ContainsKey(trans)){
			Debug.LogError ("添加转换条件得时候，"+trans+"已经存在于Map中");return;
		}
		map.Add (trans,id);
	}

	/// <summary>
	/// 删除切换条件
	/// </summary>
	/// <param name="trans">条件.</param>
	public void DeleteTransition(Transition trans){
		if(trans==Transition.NullTransiton){
			Debug.LogError ("不允许NullTransiton");return;
		}
		if(map.ContainsKey(trans)==false){
			Debug.LogError("添加转换条件得时候，"+trans+"已经存在于Map中");return;
		}
		map.Remove (trans);
	}

	public StateID GetOutputState(Transition trans){
		if(map.ContainsKey(trans)){
			return map [trans];
		}
		return StateID.NullStateId;
	}
	//进入这个状态时
	public virtual void DobeforeEntering(){}
	//离开这个状态时
	public virtual void DoAfterLeaving(){}
	//在这个状态时
	public abstract void Act(GameObject npc);
	//发成状态转换时
	public abstract void Reason(GameObject npc);//判断转换条件

}
