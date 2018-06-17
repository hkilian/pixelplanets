// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Meteorite" {
	Properties {

		MeteoriteCol ( "MeteoriteCol", Color) = (1, 1, 1, 1)

	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }

		pass
		{		
			Tags { "LightMode"="ForwardBase"}

			CGPROGRAM

			#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest

			#pragma vertex vertShadow
			#pragma fragment fragShadow
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			float4 _LightColor0;

			// Colors
			float4 MeteoriteCol;
			

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 normal : TEXCOORD1;
				LIGHTING_COORDS(3, 4)
			};

			v2f vertShadow(appdata_base v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
				o.normal = normalize(v.normal).xyz;

				TRANSFER_VERTEX_TO_FRAGMENT(o);

				return o; 
			}

			float4 fragShadow(v2f i) : COLOR {		
						
				float3 L = normalize(i.lightDir);
				float3 N = normalize(i.normal);	 

				float attenuation = LIGHT_ATTENUATION(i) * 2;
				float4 ambient = UNITY_LIGHTMODEL_AMBIENT * 2;

				float NdotL = saturate(dot(N, L));

				float4 diffuseTerm = NdotL * _LightColor0 * attenuation;

				float shade = 0.0f;

				if(diffuseTerm.r > 0.5) {
					shade = 1.0;
				} else {
					shade = 0.0;
				}


                float4 finalColor = (ambient + shade) * MeteoriteCol;

				return finalColor;

			}

			ENDCG
		}		

	} 
	FallBack "Diffuse"
}