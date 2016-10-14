// This shader uses Unity sufrace shaders, which are able to combine vertex and fragment elements an easier method.
// This shader was modified from Daniel Schulz's Project 1, to incorporate a BumpMap and edit specular/diffuse components.
// References:
// http://docs.unity3d.com/Manual/SL-SurfaceShaderLightingExamples.html
// http://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html
// http://docs.unity3d.com/Manual/SL-SurfaceShaderLighting.html
// 

Shader "Custom/DiffuseSpecBumpSurf" {
	Properties{
	_MainTex("MainTexture", 2D) = "white" {}
	_BumpMap("BumpMap", 2D) = "bump" {}
	_Detail("Detail",2D) = "grey" {}
	}

		SubShader{
		Tags{
		"RenderType" = "Opaque"
	}
		CGPROGRAM
#pragma surface surf SimplDiffuseSpecular vertex:vert

	half4 LightingSimplDiffuseSpecular(SurfaceOutput s, half3 lightDir,half3 viewDir, half atten)
	{
	
		//Diffuse Portion
		// Dot the incident light with the normal of the surface.
		half NdotL = dot(s.Normal,lightDir); 
		
		// Specular Portion
		// Set the amount of specularity
		float specN = 0.2f;
		float fAtt = 0.1f;
		// calculate H based on Blinn-Phong (more efficient as it's a dot product).
		
		
		half diff = max(0, dot(s.Normal, lightDir)); // take only positive difference (between zero and the dot product).

		half3 h = normalize(lightDir + viewDir);
		float nh = max(0, dot(s.Normal, h));  // Find the difference when incorporating the normilized light vector as well.
		float spec = fAtt * pow(nh, specN); // speclarity is based on the power of this dot product and a constant.

		//Combine Both Diffusion and Specular Together
		half4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
		c.rgb += (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
		c.a = s.Alpha;
		return c;
	}

	struct Input {
		float2 uv_MainTex;
		float2 uv_Detail;
		float2 uv_BumpMap;
	};
	// Ambient Portion
	void vert(inout appdata_full v) {
		v.color.rgb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb; // outputting vertex colour based on input and ambient colour.
	}
	// Sampling the Main, Detail and BumpMap Textures.
	sampler2D _MainTex; 
	sampler2D _Detail;
	sampler2D _BumpMap;

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb; //Map the Albedo/Base Colour to the RGB values in the main texture
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)); // Takes normal map, and processes it for both vertex/fragment portions.
		o.Albedo *= tex2D(_Detail, IN.uv_Detail).rgb * 2; // Add/Multiply The detail texture to the output surface as well.
		
	}
	ENDCG
	}
	Fallback "Diffuse"
}