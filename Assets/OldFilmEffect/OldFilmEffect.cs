using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OldFilmEffect : MonoBehaviour {
	
	#region Variables
	public Shader oldFilmShader;
	
	public float oldFilmEffectAmount = 1.0f;
	
	public Color sepiaColor = Color.white;
	public Texture2D vignetteTexture;
	public float vignetteAmount = 1.0f;
	
	public Texture2D scratchesTexture;
	public float scratchesXSpeed;
	public float scratchesYSpeed;
	
	public Texture2D dustTexture;
	public float dustXSpeed;
	public float dustYSpeed;
	
	private Material curMaterial;
	private float randomValue;
	#endregion
	
	#region Properties
	public Material material {
		get {
			if (curMaterial == null) {
				curMaterial = new Material(oldFilmShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}
	#endregion
	
	// Use this for initialization
	void Start () {
		if (SystemInfo.supportsImageEffects == false) {
			enabled = false;
			return;
		}
		
		if (oldFilmShader != null && oldFilmShader.isSupported == false) {
			enabled = false;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture){
		if (oldFilmShader != null) {
			material.SetColor("_SepiaColor", sepiaColor);
			material.SetFloat("_VignetteAmount", vignetteAmount);
			material.SetFloat("_EffectAmount", oldFilmEffectAmount);
			
			if (vignetteTexture) {
				material.SetTexture("_VignetteTex", vignetteTexture);
			}
			
			if (scratchesTexture) {
				material.SetTexture("_ScratchesTex", scratchesTexture);
				material.SetFloat("_ScratchesXSpeed", scratchesXSpeed);
				material.SetFloat("_ScratchesYSpeed", scratchesYSpeed);
			}
			
			if (dustTexture) {
				material.SetTexture("_DustTex", dustTexture);
				material.SetFloat("_DustXSpeed", dustXSpeed);
				material.SetFloat("_DustYSpeed", dustYSpeed);
				material.SetFloat("_RandomValue", randomValue);
			}
			
			Graphics.Blit(sourceTexture, destTexture, material);
		} else {
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
	
	// Update is called once per frame
	void Update () {
		vignetteAmount = Mathf.Clamp(vignetteAmount, 0.0f, 1.0f);
		oldFilmEffectAmount = Mathf.Clamp(oldFilmEffectAmount, 0.0f, 1.0f);
		randomValue = Random.Range(-1.0f, 1.0f);
	}
	
	void OnDisable () {
		if (curMaterial != null) {
			DestroyImmediate(curMaterial);
		}
	}
}
