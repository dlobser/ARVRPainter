Shader "Unlit/Add"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MainTex2 ("Texture", 2D) = "white" {}
		_Color ("Color",color) = (1,1,1,1)
		_Mult("Mult",float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MainTex2;
			float4 _Color;
			float _Mult;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

//			fixed4 frag (v2f i) : SV_Target
//			{
//				// sample the texture
//				float4 col2 = tex2D(_MainTex2, i.uv);
//				float mult = max(0,col2.x-.5)*2;
//
//				float4 col = tex2D(_MainTex, i.uv );//- float2(0,-.02)*mult);
//				float4 col4 = tex2D(_MainTex2, i.uv);// - float2(0,-.02)*mult);
//
////				float4 col = tex2D(_MainTex, i.uv - float2(0,-.02)*mult);
//				float4 col4b = tex2D(_MainTex2, i.uv - float2(0,-.001));
////				col4+=max(0,col4b.x-3)*2;
//
//				float4 col3 = lerp(max(0, col + (col4+max(0,col4b.x-3) )), max(0,col4 - col), _Mult);// (1 - col.x), _Mult);
//
////				float4 col6 = max(0,col3-1);
////				float4 col7 = lerp(float4(0,0,1,1),float4(1,0,0,1),col6);
//				// apply fog
////				UNITY_APPLY_FOG(i.fogCoord, col);
//				return col3;// - _Color*.1;
//			}

			float4 Desaturate(float3 color, float Desaturation)
			{
				float3 grayXfer = float3(0.3, 0.59, 0.11);
				float f = dot(grayXfer, color);
				float3 gray = float3(f,f,f);
				return float4(lerp(color, gray, Desaturation), 1.0);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float4 col2 = tex2D(_MainTex2, i.uv);
				float mult = max(0,Desaturate(col2.xyz,0)-.9);

				float4 col = tex2D(_MainTex, i.uv - float2(0,-.002)*mult);
				float4 col4 = tex2D(_MainTex2, i.uv - float2(0,-.002)*mult);

				float4 col4b = tex2D(_MainTex2, i.uv - float2(0,-.0004));

				float4 col3 = lerp( max(0, col + (col4+max(0,Desaturate(col4b,0)-3) )) , max(0,col4 - col), _Mult);// (1 - col.x), _Mult);

//				float4 col6 = max(0,col3-1);
//				float4 col7 = lerp(float4(0,0,1,1),float4(1,0,0,1),col6);
				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);
				return col3;// - _Color*.1;
			}
			ENDCG
		}
	}
}
