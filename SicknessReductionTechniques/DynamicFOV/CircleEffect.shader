// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CircleEffect"
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
			float _blackRatio = 1;
			float _viewRadius = .25;
			float _leftEye = 0;
			float _rightEye = 0;
            

			float4 gridOverPixel(sampler2D tex, float2 uv, float4 size) //average values surrounding pixel together and return the result
			{

                float4 newFragColor = 0;
               
				newFragColor += tex2D(tex, uv+float2(0, 0)); //this should be the same
				//we can just use sin and cos 
				return newFragColor;
				
			
			}

            //returns color in float 4 variable given our v2f
			float4 giveColor (v2f i) : SV_Target
			{
				float4 col = gridOverPixel(_MainTex, i.uv, _MainTex_TexelSize);

				float2 coord = i.uv;
				coord -= float2(.5,.5);


				// if(_leftEye == 1){
				// 	if(sqrt((coord.y * coord.y) + (coord.x - .5) * (coord.x - .5)) > _viewRadius){
				// 		return col * _blackRatio;
				// 	}
				// }

				// if(_rightEye == 1){
				// 	if(sqrt((coord.y * coord.y) + (coord.x + .5) * (coord.x + .5)) > _viewRadius){
				// 		return col * _blackRatio;
				// 	}
				// }

				if(sqrt((coord.y * coord.y) + (coord.x) * (coord.x )) > _viewRadius){
						return col * _blackRatio;
				}

				// if(_leftEye == 1){
				// 	if(sqrt((coord.y * coord.y) + (coord.x + .5) * (coord.x + .5)) > _viewRadius){
				// 		return col * _blackRatio;
				// 	}
				// }
				
				// if(_leftEye == 1){
				// 	if(coord.)
				// }
				
				// if(coord.x * coord.x + coord.y * coord.y > _viewRadius){
				// 	if(_leftEye == 0 || coord.x < 0){
				// 		if(_rightEye == 0 || coord.x > 0){
				// 			return col * (_blackRatio);
				// 		}
				// 	}
					
				// }
				return col;
				//so this returns the rgb
				//i.uv.x and i.uv.y returns the color for that position
				//return float4(0, 0, 0, 1);
			}
			ENDCG
		}
	}
}