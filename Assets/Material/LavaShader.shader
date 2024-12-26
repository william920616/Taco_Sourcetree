Shader "Custom/DeepLavaShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.6, 0.1, 0.0, 1)  // 更深的紅橙色（基礎顏色）
        _EmissionColor ("Emission Color", Color) = (1, 0.3, 0, 1)  // 深橙色發光
        _FlowSpeed ("Flow Speed", Float) = 0.5  // 流動速度
        _NoiseScale ("Noise Scale", Float) = 0.3  // 噪聲的縮放，控制流動的細節

        _Color1 ("Color 1", Color) = (0.6, 0.1, 0.0, 1)  // 深紅橙色
        _Color2 ("Color 2", Color) = (0.7, 0.2, 0.0, 1)  // 橙紅色
        _Color3 ("Color 3", Color) = (0.8, 0.3, 0.0, 1)  // 深橙色
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
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            uniform float _FlowSpeed;
            uniform float _NoiseScale;
            float4 _BaseColor;
            float4 _EmissionColor;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 基於時間和UV來控制流動
                float flow = sin(i.uv.x * _NoiseScale + _Time.y * _FlowSpeed); // 流動效果，利用正弦波
                flow = smoothstep(-0.5, 0.5, flow);  // 讓波動看起來平滑一些

                // 混合顏色：根據流動情況混合三個顏色
                float mixAmount1 = smoothstep(0.0, 1.0, flow); // 顏色混合1
                float mixAmount2 = smoothstep(0.5, 1.0, flow); // 顏色混合2
                float mixAmount3 = smoothstep(0.8, 1.0, flow); // 顏色混合3

                // 使用混合顏色的插值
                half4 mixedColor = lerp(_Color1, _Color2, mixAmount1);  // 混合第一個和第二個顏色
                mixedColor = lerp(mixedColor, _Color3, mixAmount2);  // 混合結果與第三個顏色

                // 基本顏色和發光顏色結合
                half4 color = mixedColor * flow;  // 顏色會隨流動波動
                color += _EmissionColor;  // 添加發光效果

                return color;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}









