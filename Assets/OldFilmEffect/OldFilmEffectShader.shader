Shader "Custom/OldFilmEffectShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VignetteTex ("Vignette Texture", 2D) = "white" {}
		_VignetteAmount ("Vignette Opacity", Range(0, 1)) = 1
		_ScratchesTex ("Scraches Texture", 2D) = "white" {}
		_ScratchesXSpeed ("Scraches X Speed", Float) = 10.0
		_ScratchesYSpeed ("Scraches Y Speed", Float) = 10.0
		_DustTex ("Dust Texture", 2D) = "white" {}
		_DustXSpeed ("Dust X Speed", Float) = 10.0
		_DustYSpeed ("Dust Y Speed", Float) = 10.0
		_SepiaColor ("Sepia Color", Color) = (1, 1, 1, 1)
		_EffectAmount ("Old Film Effect Amount", Range(0, 1)) = 1
		_RandomValue ("Random Value", Float) = 1.0
	}
	SubShader {
		Pass {
			CGPROGRAM
			
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform sampler2D _VignetteTex;
			uniform sampler2D _ScratchesTex;
			uniform sampler2D _DustTex;
			fixed4 _SepiaColor;
			fixed _VignetteAmount;
			fixed _ScratchesXSpeed;
			fixed _ScratchesYSpeed;
			fixed _DustXSpeed;
			fixed _DustYSpeed;
			fixed _EffectAmount;
			fixed _RandomValue;
			
			fixed4 frag (v2f_img i) : COLOR {
				half2 renderTexUV = half2(i.uv.x, i.uv.y + (_RandomValue * _SinTime.z * 0));
				fixed4 renderTex = tex2D(_MainTex, renderTexUV);
				
				// Get teh pixed from the Vignette Texture
				fixed4 vignetteTex = tex2D(_VignetteTex, i.uv);
				
				// Process the Scratches UV and pixels
				half2 scratchesUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _ScratchesXSpeed), 
														i.uv.y + (_Time.x * _ScratchesYSpeed));
				fixed4 scratchesTex = tex2D(_ScratchesTex, scratchesUV);
				
				// Process the Dust UV and pixels
				half2 dustUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _DustXSpeed), 
														i.uv.y + (_Time.x * _DustYSpeed));
				fixed4 dustTex = tex2D(_DustTex, dustUV);
				
				// Get the luminosity values from the render texture using the YIQ values
				fixed lum = dot(fixed3(0.299, 0.587, 0.114), renderTex.rgb);
				
				// Add the constant calor to the lum values
				fixed4 finalColor = lum + _SepiaColor;// lerp(_SepiaColor, _SepiaColor + fixed4(0.01f, 0.01f, 0.01f, 0.01f), _RandomValue);
				
				// Create a constant white color we can use to adjust opacity of effects
				fixed3 constantWhite = fixed3(1, 1, 1);
				
				// Composite together the different layers to create final Screen Effect
				finalColor = lerp(finalColor, finalColor * vignetteTex, _VignetteAmount);
				finalColor.rgb *= lerp(scratchesTex, constantWhite, _RandomValue);
				finalColor.rgb *= lerp(dustTex, constantWhite, (_RandomValue * _SinTime.z));
				finalColor = lerp(renderTex, finalColor, _EffectAmount);
				
				return finalColor;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
