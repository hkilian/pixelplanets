// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/planet_surface" {

Properties 
	{
		_DiffuseTexture ("Diffuse Texture", 2D) = "white" {}
		_DitherTexture ("Dither Texture", 2D) = "white" {}
		_CivTexture ("Civ Texture", 2D) = "white" {}
		_DiffuseTint ( "Diffuse Tint", Color) = (1, 1, 1, 1)

		SeaLevel ("SeaLevel", Float) = 0.5

		OceanCol ( "OceanCol", Color) = (1, 1, 1, 1)
		LandCol ( "LandCol", Color) = (1, 1, 1, 1)
		SandCol ( "SandCol", Color) = (1, 1, 1, 1)
		MountainCol ( "MountainCol", Color) = (1, 1, 1, 1)

		NightLightsCol ( "NightLightsCol", Color) = (1, 1, 1, 1)

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

			sampler2D _DiffuseTexture;
			sampler2D _DitherTexture;
			sampler2D _CivTexture;
			float4 _DiffuseTint;
			float4 _LightColor0;
			float SeaLevel;

			// Colors
			float4 OceanCol;
			float4 LandCol;
			float4 SandCol;
			float4 MountainCol;
			float4 NightLightsCol;
			

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float2 uv : TEXCOORD2;
				float2 screenPos:TEXCOORD4;
				LIGHTING_COORDS(3, 4)
			};

			v2f vertShadow(appdata_base v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
				o.normal = normalize(v.normal).xyz;
				o.screenPos = ComputeScreenPos(v.vertex);

				TRANSFER_VERTEX_TO_FRAGMENT(o);

				return o; 
			}

			float4 fragShadow(v2f i) : COLOR {	

				// Flag for nightime level
				int night = 0;
						
				float3 L = normalize(i.lightDir);
				float3 N = normalize(i.normal);	 

				float attenuation = LIGHT_ATTENUATION(i) * 2;
				float4 ambient = UNITY_LIGHTMODEL_AMBIENT * 2;

				float NdotL = saturate(dot(N, L));

				float4 diffuseTerm = NdotL * _LightColor0 * _DiffuseTint * attenuation;


				// Dither texture
				float4 dither = tex2D(_DitherTexture, i.screenPos.xy);
				float dith = dither.r / 25;
				float diff = diffuseTerm.r + dith;

				// Discretize the intensity, based on a few cutoff points
			    if (diff > 0.95) {
			        diffuseTerm = float4(1.3, 1.3, 1.3, 1.0);
			    } else if (diff > 0.5) {
			        diffuseTerm = float4(0.7,0.7,0.7,1.0);
			        night = 1;
			    } else if (diff > 0.05) {
			        diffuseTerm = float4(0.35,0.35,0.35,1.0);
			        night = 2;
			    } else {
			        diffuseTerm = float4(0.1,0.1,0.1,1.0);
			        night = 3;
			    }


				float4 diffuse = tex2D(_DiffuseTexture, i.uv);

				float4 pColor;

                if(diffuse.r < SeaLevel - 0.17) {
                	pColor = OceanCol;
                } else if(diffuse.r < SeaLevel) {
                	pColor = OceanCol + 0.1;
                } else if(diffuse.r < SeaLevel + 0.05) {
                	pColor = SandCol;
                } else if(diffuse.r > SeaLevel + 0.35) {
                	pColor = LandCol - 0.1;
                } else {
                	pColor = LandCol;
                }

                if(night != 0) {
                	float4 civ = tex2D(_CivTexture, i.uv);

                	if(night == 1) {
                		if(civ.r >= 0.3) { return NightLightsCol; }
                	} else if(night == 2) {
                		if(civ.r >= 0.2) { return NightLightsCol; }
                	} else if(night == 3) {
                		if(civ.r >= 0.05) { return NightLightsCol; }
                	}

                } 

                float4 finalColor = (ambient + diffuseTerm) * pColor;

				return finalColor;

			}

			ENDCG
		}		

	} 
	FallBack "Diffuse"
}