// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MeanBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {} //include a texture as a property
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off //ZTest Always

		Pass
		{
			CGPROGRAM


			#pragma vertex vertexToFragment
			#pragma fragment giveColor
			
			#include "UnityCG.cginc"

            //info from vertex on the mesh
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

            //info for fragment function
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vertexToFragment (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
            

			float4 gridOverPixel(sampler2D tex, float2 uv, float4 size) //average values surrounding pixel together and return the result
			{

                float4 newFragColor = 0;
                newFragColor += tex2D(tex, uv+float2(-size.x, size.y)) +  tex2D(tex, uv+float2(0, size.y)) +  tex2D(tex, uv+float2(size.x, size.y)); //top row
                newFragColor += tex2D(tex, uv+float2(-size.x, 0)) +       tex2D(tex, uv+float2(0, 0)) +       tex2D(tex, uv+float2(size.x, 0)); //middle row
                newFragColor += tex2D(tex, uv+float2(-size.x, -size.y)) + tex2D(tex, uv+float2(0, -size.y)) + tex2D(tex, uv+float2(size.x, -size.y)); //bottom row


				return newFragColor / 9;
			}

            //returns color in float 4 variable given our v2f
			float4 giveColor (v2f i) : SV_Target
			{
				float4 col = gridOverPixel(_MainTex, i.uv, _MainTex_TexelSize);
				return col;
			}
			ENDCG
		}
	}
}