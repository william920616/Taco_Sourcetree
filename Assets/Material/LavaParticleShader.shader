Shader "Custom/LavaParticleShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0.3, 0.1, 1)
        _EmissionColor ("Emission Color", Color) = (1, 0.4, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };
            
            uniform float4 _Color;
            uniform float4 _EmissionColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _Color + _EmissionColor; // 基本顏色加發光效果
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
