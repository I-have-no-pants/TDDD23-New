using UnityEngine;
using System.Collections;

public class AttachmentComponent : MonoBehaviour {
	
	void Attach(GameObject child) {
		child.transform.parent = this.transform;
	}

}
