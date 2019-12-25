Shader "Custom/DiscoInner" {
    Properties {
		_Color ("Color applied to transparent", Color) = (1,1,1,1)
		_MainTex ("Transparent, will change color", 2D) = ""
	}
	 
	Category {
		Tags {"Queue"="Transparent+101" "IgnoreProjector"="True"}
        Fog { Mode Off }
		Blend OneMinusDstColor One
	   
		SubShader {
			Pass {
				SetTexture[_MainTex] {
					combine texture * constant ConstantColor[_Color]
				}
			}
		}
	}
}