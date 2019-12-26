Shader "Custom/PlaneUnlit" {
    Properties {
		_Color ("Color applied to transparent", Color) = (1,1,1,1)
		_MainTex ("Transparent, will change color", 2D) = ""
	}
	 
	Category {
		Tags {"Queue"="Transparent-10" "IgnoreProjector"="True"}
        Fog { Mode Off }
		ZWrite Off
        ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
	   
		SubShader {
			Pass {
				SetTexture[_MainTex] {
					combine texture * constant ConstantColor[_Color]
				}
			}
		}
	}
}