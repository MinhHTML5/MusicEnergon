Shader "Custom/DiscoBallUnlitTransparent" {
    Properties {
		_Color ("Color applied to transparent", Color) = (1,1,1,1)
		_MainTex ("Transparent, will change color", 2D) = ""
		_HighlightTex ("Highlight, not affected by color", 2D) = ""
	}
	 
	
	SubShader {
		Tags {"Queue"="Transparent-5" "IgnoreProjector"="True" "RenderType" = "Transparent"}
		ZWrite Off
		
		// ===============================
		// Choose one of these two
		// ===============================
		Blend SrcAlpha OneMinusSrcAlpha
		// ===============================
		//Cull Off
		//Blend OneMinusDstColor One
		// ===============================
		
        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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
			
			float4 _Color;
			sampler2D _MainTex;
			sampler2D _HighlightTex;
			float4 _MainTex_ST;
			float4 _HighlightTex_ST;
		 
		 
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
		 
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 mainCol = tex2D(_MainTex, i.uv);
				fixed4 highCol = tex2D(_HighlightTex, i.uv);
				fixed4 output = mainCol * _Color;
				if (highCol.a > 0) {
					output.rgb = output.rgb + highCol.rgb * highCol.a;
					if (highCol.a > mainCol.a) {
						output.a = highCol.a;
					}
				}
				// ===================================
				// If you want fog
				//UNITY_APPLY_FOG(i.fogCoord, output);
				//UNITY_OPAQUE_ALPHA(output.a);
				// ===================================
				return output;
			}
			ENDCG
		} 
	}
}