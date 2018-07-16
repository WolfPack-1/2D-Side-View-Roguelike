using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxAutoDestroy : MonoBehaviour
{

	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		if(animator == null)
			Destroy(this);
	}

	void Start ()
	{
		Destroy (gameObject, animator.GetCurrentAnimatorStateInfo(0).length); 	
	}
}
