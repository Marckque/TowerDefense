using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour 
{
	public float _mainPathDuration = 2f;
	public Transform[] _mainPath;
	public float _secondaryPathDuration = 2f;
	public Transform[] _secondaryPath;
	public float _alternativePathDuration = 4f;
	public Transform[] _alternativePath;

	public Transform[] GetMainPath()
	{
		return _mainPath;
	}

	public Transform[] GetSecondaryPath()
	{
		return _secondaryPath;
	}

	public Transform[] GetAlternativePath()
	{
		return _alternativePath;
	}
}