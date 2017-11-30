Shader "Unlit/aurora"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		Thickness ("Thickness",float) = 1
		resolution ("resolution",float) = 1
		_Pos("Positions",vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			 CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"

            #pragma vertex InitCustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0


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
			float Thickness;
			float resolution;
			float4 _Pos;
			
//			v2f vert (appdata v)
//			{
//				v2f o;
//				o.vertex = UnityObjectToClipPos(v.vertex);
//				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				UNITY_TRANSFER_FOG(o,o.vertex);
//				return o;
//			}

			float clamp(float i, float l, float h){
				return max(l,min(h,i));
			}
			float drawLine(float2 UV, float2 p1, float2 p2) {
			  float2 uv = UV;

			  float a = abs(distance(p1, uv));
			  float a2 = clamp((a-Thickness)/-Thickness*.5,0,1);
			  float b = abs(distance(p2, uv));
  			  float b2 = clamp((b-Thickness)/-Thickness*.02,0,1);

			  float c = abs(distance(p1, p2));

  			  float m = distance(lerp(p1,p2,.5),uv)-(c*.5);
//  			  float m2 = m/Thickness;
  			  float m3 = 1-m;//clamp(m/Thickness,0,1);

//			  if ( a >= c || b >=  c ) m=0;return 0.0;

			  float p = (a + b + c) * 0.5;

			  // median to (p1, p2) vector
			  float h = (2 / c * sqrt( p * ( p - a) * ( p - b) * ( p - c)) );
			  float h2 = max(0,min(1,smoothstep(0.5 * Thickness, 1.5 * Thickness, h)));

			  return m;//*lerp(1.0, 0.0, h2 );//+ a2*.5;// + b2;
			}

			
//			fixed4 frag (v2f i) : SV_Target
//			{
//				// sample the texture
//				fixed4 col = tex2D(_MainTex, i.uv);
//				float penis = drawLine(i.uv.xy, _Pos.xy, _Pos.zw);
//				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);
//				return penis;
//			}
//
			 float4 frag(v2f_init_customrendertexture IN) : COLOR
            {
            	fixed4 col = tex2D(_MainTex, IN.texcoord.xy) ;
                return min(1.0,drawLine(IN.texcoord.xy, _Pos.xy, _Pos.zw)) * resolution ;//* float4(.5,1,.2,0);
            }
			ENDCG
		}
	}
}
//
//#define resolution vec2(500.0, 500.0)
//#define Thickness 0.003
//
//void main()
//{
//  gl_FragColor = vec4(
//      max(
//        max(
//          drawLine(vec2(0.1, 0.1), vec2(0.1, 0.9)),
//          drawLine(vec2(0.1, 0.9), vec2(0.7, 0.5))),
//        drawLine(vec2(0.1, 0.1), vec2(0.7, 0.5))));
//}