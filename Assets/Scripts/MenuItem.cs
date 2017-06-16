using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItem : MonoBehaviour {

	void Start()
	{
		EventTrigger trigger = GetComponentInParent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener((eventData) => { Activate(); });
		trigger.triggers.Add(entry);
	}

	public virtual void Activate() { }
	
}
