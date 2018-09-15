using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpotsOrchestrator : MonoBehaviour 
{
	private Spot[] _spots;
	private Transform[] _spotsNames;

	public void SetReferences()
	{
		_spots = new Spot[transform.childCount];

		for (int i = 0 ; i < _spots.Length ; ++i)
		{
			_spots[i] = transform.GetChild(i).GetComponent<Spot>();
			Transform[] childrenOfSpot = new Transform[_spots[i].transform.childCount];

			_spots[i]._mainPath = new Transform[childrenOfSpot.Length];
			_spots[i]._secondaryPath = new Transform[childrenOfSpot.Length];

			for (int j = 0 ; j < childrenOfSpot.Length ; ++j)
			{
				childrenOfSpot[j] = _spots[i].transform.GetChild(j);
				
				_spots[i]._mainPath[j] = childrenOfSpot[j];
				_spots[i]._secondaryPath[j] = childrenOfSpot[childrenOfSpot.Length - j];
			}
		}
	}

	public void SetNames()
	{
		_spotsNames = new Transform[transform.childCount];

		for (int i = 0 ; i < _spotsNames.Length ; ++i)
		{
			_spotsNames[i] = transform.GetChild(i);
			_spotsNames[i].name = "Spot (" + i + ")";
			Transform[] childrenOfSpot = new Transform[_spotsNames[i].childCount];

			for (int j = 0 ; j < childrenOfSpot.Length ; ++j)
			{
				childrenOfSpot[j] = _spotsNames[i].GetChild(j);
				if (j == 0) childrenOfSpot[j].name = "Start_Spot_" + i;
				else if (j == childrenOfSpot.Length - 1) childrenOfSpot[j].name = "End_Spot_" + i;
				else
				{
					childrenOfSpot[j].name = "Waypoint_" + j + "_Spot_" + i;
				}				
			}
		}
	}
}

[CustomEditor(typeof(SpotsOrchestrator))]
public class SpotsOrchestratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		SpotsOrchestrator spotsOrchestrator = (SpotsOrchestrator)target;

		if (GUILayout.Button("Set all"))
		{
			spotsOrchestrator.SetNames();
			spotsOrchestrator.SetReferences();
		}

		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Set references"))
		{
			spotsOrchestrator.SetReferences();
		}

		if (GUILayout.Button("Set names"))
		{
			spotsOrchestrator.SetNames();
		}

		EditorGUILayout.EndHorizontal();
	}
}