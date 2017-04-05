using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{

    private Animator _arenaAnimator;
	public void Start ()
	{
	    var arena = transform.parent.gameObject;
	    _arenaAnimator = arena.GetComponent<Animator>();
	}
	
	public void Update () 
	{
		
	}

    public void OnTriggerEnter(Collider other)
    {
        _arenaAnimator.SetBool("IsLowered", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _arenaAnimator.SetBool("IsLowered", false);
    }
}
