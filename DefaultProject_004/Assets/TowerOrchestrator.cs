using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOrchestrator : MonoBehaviour 
{
	private LerpMover[] _lerpMover;

	private void OnValidate()
	{
		_lerpMover = new LerpMover[transform.childCount];

		for (int i = 0 ; i < _lerpMover.Length ; ++i)
		{
			_lerpMover[i] = transform.GetChild(i).GetComponent<LerpMover>();
		}
	}

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) && CanTowersMove())
		{
			for (int i = 0 ; i < _lerpMover.Length ; ++i)
			{
				_lerpMover[i].MoveThroughMainPath();
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) && CanTowersMove())
		{
			for (int i = 0 ; i < _lerpMover.Length ; ++i)
			{
				_lerpMover[i].MoveThroughSecondaryPath();
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && CanTowersMove())
		{
			for (int i = 0 ; i < _lerpMover.Length ; ++i)
			{
				_lerpMover[i].MoveThroughAlternativePath();
			}
		}
	}

	public bool CanTowersMove()
	{
		int count = 0;
		for	(int i = 0 ; i < _lerpMover.Length ; ++i)
		{
			if (!_lerpMover[i].IsMoving)
			{
				count++;
			}
		}

		if (count == _lerpMover.Length)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}