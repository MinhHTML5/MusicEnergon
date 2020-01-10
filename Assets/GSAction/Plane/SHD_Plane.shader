Shader "Custom/PlaneUnlit" {
    Properties {
		_Color ("Color applied to transparent", Color) = (1,1,1,1)
		_MainTex ("Transparent, will change color", 2D) = ""
	}
	 
	SubShader {
		Tags {"Queue"="Transparent-10" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
        ZTest Always
        LOD 100
        
        Pass {
            Lighting Off
            
            SetTexture [_MainTex] { 
                // Sets our color as the 'constant' variable
                constantColor [_Color]
                
                // Multiplies color (in constant) with texture
                combine constant * texture
            }
        }
	}
}