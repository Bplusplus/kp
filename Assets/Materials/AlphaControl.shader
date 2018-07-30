Shader "Unlit/AlphaControl"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_Alpha ("Alpha", Range(0.0,1)) = 1
		//_isGlitch("IsGlitch",bool)=true
		_Distance("Distance", float) =1
		_Amplitude("Amplitude", float)=1
		_Speed("Speed", float)=1
		_Amount("Amount",Range(0.0,1))=1
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100
		ZWrite OFF
		Blend SrcAlpha OneMinusSrcAlpha

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
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Alpha;
			float _Distance;
			float _Amplitude;
			float _Speed;
			float _Amount;
			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.x+=sin(_Time.y*_Speed +v.vertex.y*_Amplitude) *_Distance *_Amount;
				v.vertex.y+=sin(_Time.y*_Speed +v.vertex.x*_Amplitude) *_Distance *_Amount;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
		
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a=_Alpha;
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
