//References: 
// http://kylehalladay.com/blog/tutorial/bestof/2013/10/13/Multi-Light-Diffuse.html
// https://en.wikibooks.org/wiki/Cg_Programming/Unity/Multiple_Lights
// https://en.wikibooks.org/wiki/Cg_Programming/Unity/Lighting_of_Bumpy_Surfaces 
Shader "Custom/MyCustomShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Colour("Colour", Color) = (1,1,1,1) 

	}
	SubShader {
			
		Pass {
			Tags {"LightMode" = "ForwardBase"}
			
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag	
		#include "UnityCG.cginc"
		#include "AutoLight.cginc"
		#pragma multi_compile_fwdbase

		struct input {
			float2 uv : TEXCOORD0;
			float4 vertex : POSITION;
			fixed4 normal : NORMAL;
		};

		struct output {
			float2 uv: TEXCOORD0;
			float4 vertex: SV_POSITION;
			float3 lightDir : TEXCOORD1;
			float3 normal : TEXCOORD2;
			LIGHTING_COORDS(3, 4)
			float3 lighting : TEXCOORD5;
		};

		uniform fixed3 _Colour;
		sampler2D _MainTex;
		uniform half4 _MainTex_ST;
		uniform fixed4 _LightColor0;

		output vert(input vertIn)
		{
			output o;
			// calculate the position of the output vertex, relative to View of the scene (MVP)
			o.vertex = mul(UNITY_MATRIX_MVP, vertIn.vertex);
			// Convert normal to word coordinates.
			o.normal = normalize(mul(vertIn.normal, _World2Object));
			// Get a vector from the light to the current vertex, unnormalised
			o.lightDir = ObjSpaceLightDir(vertIn.vertex);
			// pass the uv/texture in this first pass
			o.uv = vertIn.uv;

			// send attenuation to the fragment shader (ie. the falloff of light intensity)
			TRANSFER_VERTEX_TO_FRAGMENT(o);

			o.lighting = float3(0.0, 0.0, 0.0);

			// calculate the position of the vertex relative to the world
			float4 worldPosition = mul(_Object2World, vertIn.vertex);
			// calculate the normal with respecto the the world
			float3 worldNormal = mul(transpose((float3x3)_World2Object), vertIn.normal.xyz);

			for (int i = 0; i < 4; i++)
			{
				// Combine all of unity's pre-built in Light varables
				float4 lightPos = float4(unity_4LightPosX0[i],unity_4LightPosY0[i],	unity_4LightPosZ0[i], 1.0);
				// Get the distance between the light and the world position of the vertex
				float3 vertexLightDist = float3(worldPosition.xyz -lightPos.xyz);
				// Normalise it to get the unit vector (ie. the direction).
				float3 lightDir = normalize(vertexLightDist);
				// calcuate the attenuation being the invese of the square of the distance (ie. dot product with itself), 
				//so that the light falls off gradually with distance.
				float attenuation = 1.0 / (1.0 + unity_4LightAtten0[i] * dot(vertexLightDist, vertexLightDist));
				// combine everything to get the difuse
				float3 diffuse = attenuation * float3(unity_LightColor[i].xyz) * float3(_Colour.xyz)*  dot(worldNormal.xyz, lightDir.xyz);
				o.lighting += diffuse;
			}
			
			return o;
		}
		// based on Lab4/Lab7
		float4 frag(output o): SV_Target
		{
			// Set a constant which will determine specularity
			float K = 10;
			
			// get the direction of the World Light from its position
			float4 lightDir = normalize(_WorldSpaceLightPos0);
			// light is based on the normal of the vertex and angle at which the incident light is hitting it and of course it's colour.
			float4 diffuseLight = saturate(dot(lightDir, o.normal)) * _LightColor0 ;

			float4 worldPosition = normalize(float4(_WorldSpaceCameraPos, 1.0) - o.vertex);
			//Blinn-Phong : Tale the half angle between the light direction and world position
			float4 H = normalize((lightDir + worldPosition));
			float specular = pow(saturate(dot(o.normal, H)), K);
			// add up defuse and specular
			return diffuseLight + specular;
		}	
		ENDCG
	}
			// Second pass so that lights can be addatively blended on top of the ambient light in the first pass
	Pass {
			Tags {"LightMode" = "ForwardAdd"}
			Blend One One
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdadd                        
			#include "AutoLight.cginc"
			#include "UnityCG.cginc"

			struct output {
			float2 uv: TEXCOORD0;
			float4 vertex: SV_POSITION;
			float3 lightDir : TEXCOORD1;
			float3 normal : TEXCOORD2;
			LIGHTING_COORDS(3, 4)

		};

		// source: https://docs.unity3d.com/Manual/SL-VertexProgramInputs.html
		output vert(appdata_tan v)
		{
			output o;
			// same as above, but just passing data  straight out to the fragment, as this is just used for fragment.
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.texcoord.xy;
			o.lightDir = ObjSpaceLightDir(v.vertex);
			o.normal = v.vertex;

			TRANSFER_VERTEX_TO_FRAGMENT(o);
			return o;
		}

		sampler2D _MainTex;
		fixed4 _Colour;
		fixed4 _LightColor0;

		fixed4 frag(output o) : COLOR
		{
			// only take the direction of the light coming in.
			fixed4 colour;
			o.lightDir = normalize(o.lightDir);
			// get the attenutation that was passed from vertex part
			fixed attenuation = LIGHT_ATTENUATION(o);
			// unpack the actual texture to the output uv coordinates.
			fixed4 mainTex = tex2D(_MainTex, o.uv);
			// add the colour of the shader ontop of the  texture for added effect.
			mainTex *= _Colour;
			// calculate the diffuse, again based on the angle that is being hit and the normal of the vertex.
			fixed diffuse = saturate(dot(o.normal, o.lightDir));
			// add all of the colours up of the components and weight them accordingly (greater attenuation to bring out more highlights/light and texture).
			colour.rgb = (mainTex.rgb*2) *_LightColor0.rgb * diffuse * (attenuation*2);
			// alpha
			colour.a = mainTex.a;
			return colour;
		}


			ENDCG
		}
		
	}
}
