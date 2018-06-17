// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/clouds" {


Properties 
	{
		_DiffuseTexture ("Diffuse Texture", 2D) = "white" {}
		_DiffuseTint ( "Diffuse Tint", Color) = (1, 1, 1, 1)

		Coverage ("Coverage", Float) = 0.5

	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }

		pass
		{		
			Tags { "LightMode"="ForwardBase"}

			CULL Off
			ZWrite Off // don't write to depth buffer 
         	Blend SrcAlpha OneMinusSrcAlpha // use alpha blending


			CGPROGRAM

			#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest

			#pragma vertex vertShadow
			#pragma fragment fragShadow
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			sampler2D _DiffuseTexture;
			float4 _DiffuseTint;
			float4 _LightColor0;
			float Coverage;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float2 uv : TEXCOORD2;
				LIGHTING_COORDS(3, 4)
			};

			v2f vertShadow(appdata_base v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
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
				float4 diffuseTerm = NdotL * _LightColor0 * _DiffuseTint * attenuation;

				// Discretize the intensity, based on a few cutoff points
			    if (diffuseTerm.r > 0.95) {
			        diffuseTerm = float4(1.0,1,1,1.0);
			    } else if (diffuseTerm.r > 0.5) {
			        diffuseTerm = float4(0.7,0.7,0.7,1.0);
			    } else if (diffuseTerm.r > 0.05) {
			        diffuseTerm = float4(0.35,0.35,0.35,1.0);
			    } else {
			        diffuseTerm = float4(0.1,0.1,0.1,1.0);
			    }

				float4 diffuse = tex2D(_DiffuseTexture, i.uv);

				float4 pColor;

                // Alpha
                if(diffuse.r >= Coverage + (_SinTime.g / 20)) {
                	diffuse = float4(1.0, 1.0, 1.0, 1.0);
                } else {
                	diffuse.w = 0.0;
                }

                float4 finalColor = (ambient + diffuseTerm) * diffuse;

				return finalColor;

			}

			ENDCG
		}		

	} 
	FallBack "Diffuse"
}