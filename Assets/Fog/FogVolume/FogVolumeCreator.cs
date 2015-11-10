#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

public class FogVolumeCreator : Editor {


	[UnityEditor.MenuItem("GameObject/Create Other/Fog Volume")]
	static public void CreateFogVolume()
	{
		GameObject VolumeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		VolumeObj.name = "Fog Volume";
		VolumeObj.AddComponent <FogVolume>();
		VolumeObj.GetComponent<Renderer>().castShadows = false;
		VolumeObj.GetComponent<Renderer>().receiveShadows = false;
		Selection.activeObject = VolumeObj;
		DestroyImmediate (VolumeObj.GetComponent <BoxCollider> ());
		if (UnityEditor.SceneView.currentDrawingSceneView) UnityEditor.SceneView.currentDrawingSceneView.MoveToView(VolumeObj.transform);
	}

	static public void Wireframe(GameObject VolumeObj, bool Enable)
	{
		EditorUtility.SetSelectedWireframeHidden(VolumeObj.GetComponent<Renderer>(), Enable);
	}

}
#endif