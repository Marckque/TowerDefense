using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMover : MonoBehaviour
{
	public float _moveDuration = 2f;
	public Spot _currentSpot;
	public BoxCollider _boxCollider;
	private float _percentage;
	private float _timeElapsed;
	public bool IsMoving { get; private set; }
	private Vector3 _startPosition;
	private Vector3 _endPostion;
	private int _waypointIndex;
	public AnimationCurve _movementCurve;

	private void OnTriggerEnter(Collider other) 
	{
		Spot spot = other.GetComponent<Spot>();
		if (spot)
		{
			_currentSpot = spot;
		}	
	}

	public void MoveThroughMainPath()
	{
		StartCoroutine(MoveThroughPath(_currentSpot.GetMainPath(), _currentSpot._mainPathDuration));
	}

	public void MoveThroughSecondaryPath()
	{
		StartCoroutine(MoveThroughPath(_currentSpot.GetSecondaryPath(), _currentSpot._secondaryPathDuration));
	}

	public void MoveThroughAlternativePath()
	{
		StartCoroutine(MoveThroughPath(_currentSpot.GetAlternativePath(), _currentSpot._alternativePathDuration));
	}

	private IEnumerator MoveThroughPath(Transform[] path, float moveDuration = 1f)
	{
		IsMoving = true;
		_boxCollider.enabled = false;
		float _lerpBetweenWaypoints = moveDuration / path.Length;

		while (_waypointIndex < path.Length - 1)
		{
			_startPosition = path[_waypointIndex].position;
			_endPostion = path[_waypointIndex + 1].position;

			do
			{
				_percentage = _timeElapsed / _moveDuration;
				transform.position = Vector3.Lerp(_startPosition, _endPostion, _movementCurve.Evaluate(_percentage));

				_timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			} while (_percentage < 1f);
			
			_percentage = 0f;
			_timeElapsed = 0f;
			_waypointIndex++;
		}
		
		_waypointIndex = 0;

		IsMoving = false;
		_boxCollider.enabled = true;

		yield return new WaitForEndOfFrame();
	}
}