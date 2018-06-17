using UnityEngine;
using System.Collections;

public class Pixelation : MonoBehaviour {

	public RenderTexture renderTexture;

	void Start() {

		int res = 2;
		renderTexture.width = Screen.width / res;
		renderTexture.height = Screen.height / res;

	}

	void OnGUI() {
		GUI.depth = -9999;
		GUI.DrawTexture(new Rect(0, 0, Screen.width,  Screen.height), renderTexture, ScaleMode.ScaleToFit, false);
		GUI.depth = -9999;
	}

	int NearestSuperiorPowerOf2( int n ) {
		return (int) Mathf.Pow( 2, Mathf.Ceil( Mathf.Log( n ) / Mathf.Log( 2 ) ) );
	} 
}
