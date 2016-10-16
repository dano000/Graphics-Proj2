// CubeReflect.shader
// This shader creates a dark metal effect.
// It uses a Cubemap that is generated using the GameObjectSpawner object.
// The surface should not 100% reflective like a mirror, and has manipulated
// ambient, diffuse, and specular components to create a dark metal effect.
// The Cubemap Reflection is based on the tutorial at
// https://en.wikibooks.org/wiki/Cg_Programming/Unity/Reflecting_Surfaces

Shader "Unlit/CubeReflect" {
   Properties {
      _Cube("ReflectiveGlassCubemap", Cube) = "" {}
   }
   SubShader {
      Pass {   
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 

         #include "UnityCG.cginc"

         // cubemap generated from surroundings
         uniform samplerCUBE _Cube;

         // variables for point light
         uniform float3 _PointLightColor;
		 uniform float3 _PointLightPosition;  

		 // input vertex structure
         struct vertIn {
            float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
         };

         // output vertex structure
         struct vertOut {
         	float4 vertex : SV_POSITION;
			float3 normal : TEXCOORD0;
			float3 view : TEXCOORD1;
			float4 color : COLOR;
         };

         // vertex shader
         vertOut vert(vertIn v) 
         {
            vertOut o;

            // calculate the direction of the view in terms of the world, and
            // account for the position of the camera
            o.view = mul(unity_ObjectToWorld, v.vertex).xyz - _WorldSpaceCameraPos;

            // calculate the direction of the normal in terms of the world
            o.normal = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);

            // transform vertex coordinates to camera coordinates
            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

            // pass color to fragment shader
            o.color = v.color;

            return o;
         }
 
         float4 frag(vertOut v) : COLOR
         {
         	// calculate the reflected direction of the ray
            float3 reflected = reflect(v.view, normalize(v.normal));

            // calculate the intersection of the reflected ray with the cubemap to get color
            float4 cm = texCUBE(_Cube, reflected);

            // implement Phong techniques to manipulate ambient, diffuse, and specular components
            // to provide a darker reflection

			// ambient light
			float Ka = 3;
			float3 amb = cm.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

			// diffuse reflection
			float fAtt = 1;
			float Kd = 1;
			float3 L = normalize(_PointLightPosition - v.vertex.xyz);
			float LdotN = dot(L, normalize(v.normal));
			float3 dif = fAtt * _PointLightColor.rgb * Kd * v.color.rgb * saturate(LdotN);

			// specular reflection
			float Ks = 10;
			float specN = 20;
			float3 V = normalize(_WorldSpaceCameraPos - v.vertex.xyz);
			float3 R = normalize((2.0 * LdotN * normalize(v.normal)) - L);
			float3 spe = fAtt * _PointLightColor.rgb * Ks * pow(saturate(dot(V, R)), specN);

			// combine Phong components
			cm.rgb = amb.rgb + dif.rgb + spe.rgb;
			cm.a = v.color.a;

			// return modified color from Cubemap
            return cm;
         }
 
         ENDCG
      }
   }
}