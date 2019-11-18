Shader "Custom/GlowLane" {
    Properties {
        // Adds Color field we can modify
        _Color ("Main Color", Color) = (1, 1, 1, 1)        
        _MainTex ("Base (RGB)", 2D) = "black" {}
    }

    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
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