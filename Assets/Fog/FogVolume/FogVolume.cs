using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

class FogVolume : MonoBehaviour
{
		GameObject VolumeObj;
		Vector3 VolumeSize;
		Material FogMaterial;
		[SerializeField]
		Color
				InscatteringColor = Color.white, FogColor = new Color (.5f, .6f, .7f, 1);
		[SerializeField]
		float
				Visibility = 5, InscateringExponent = 2, InscatteringIntensity = 2, InscatteringTransitionWideness = 1;
		[SerializeField]
		//[Range (0, 1)]
		float
				InscatteringStartDistance = 0;
		[SerializeField]
		Light
				Sun;
		[SerializeField]
		int
				DrawOrder = 0;
	[SerializeField]
	bool HideWireframe = true, EnableInscattering = false;

		

		void OnEnable ()
		{			
				if (!FogMaterial)
						FogMaterial = new Material (Shader.Find ("Hidden/FogVolume"));
				VolumeObj = this.gameObject;
				VolumeObj.GetComponent<Renderer>().sharedMaterial = FogMaterial;				
				Camera.main.depthTextureMode |= DepthTextureMode.Depth;

			
		}
	
		// Update is called once per frame
		void Update ()
		{
		ToggleKeyword (EnableInscattering, "FOG_VOLUME_INSCATTERING_ON", "FOG_VOLUME_INSCATTERING_OFF");
		#if UNITY_EDITOR
		FogVolumeCreator.Wireframe (VolumeObj, HideWireframe);
		#endif
				FogMaterial.SetColor ("_Color", FogColor);
				FogMaterial.SetColor ("_InscatteringColor", InscatteringColor);

				if (Sun) {
						InscatteringIntensity = Mathf.Max (0, InscatteringIntensity);
						FogMaterial.SetFloat ("_InscatteringIntensity", InscatteringIntensity);
						FogMaterial.SetVector ("L", -Sun.transform.forward);
				}
				InscateringExponent = Mathf.Max (1, InscateringExponent);
				FogMaterial.SetFloat ("_InscateringExponent", InscateringExponent);
				InscatteringTransitionWideness = Mathf.Max (1, InscatteringTransitionWideness);
				FogMaterial.SetFloat ("InscatteringTransitionWideness", InscatteringTransitionWideness);
				InscatteringStartDistance = Mathf.Max (0, InscatteringStartDistance);
				FogMaterial.SetFloat ("InscatteringStartDistance", InscatteringStartDistance);
				VolumeSize = VolumeObj.transform.lossyScale;
				//bug fix. If the 3 axes values are equal, something gets broken :/
				transform.localScale = new Vector3 ((float)decimal.Round ((decimal)VolumeSize.x, 2), VolumeSize.y, VolumeSize.z);
				FogMaterial.SetVector ("_BoxMin", VolumeSize * -0.5051f);
				FogMaterial.SetVector ("_BoxMax", VolumeSize * 0.5051f);
				Visibility = Mathf.Max (0, Visibility);
				FogMaterial.SetFloat ("_Visibility", Visibility);	
				GetComponent<Renderer>().sortingOrder = DrawOrder;



		}

	private static void ToggleKeyword (bool toggle, string keywordON, string keywordOFF)
	{
		Shader.DisableKeyword (toggle ? keywordOFF : keywordON);
		Shader.EnableKeyword (toggle ? keywordON : keywordOFF);
	}
}
