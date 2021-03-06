Shader "Custom/RingAster"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DensityMap ("Density Map", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MinimumRenderDiatance("Minimum Render Distance", Float) =10
        _MaximumFadeDistance("Maximum Fade Distance", Float) =20
        _InnerRingDiameter ("Inner Ring Diametr", Range(0,1)) = 0.5
        _RingDiametr ("Ring Diametr", Range(0,10)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent" }
        LOD 200
        CULL off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _DensityMap;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _MinimumRenderDiatance;
        float _MaximumFadeDistance;
        float _InnerRingDiameter;
        float _RingDiametr;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
float distance = length(_WorldSpaceCameraPos - IN.worldPos);


float2 position = float2((0.5 - IN.uv_MainTex.x) * _RingDiametr, (0.5 - IN.uv_MainTex.y) * _RingDiametr);
float ringDistanceFromCenter = sqrt(position.x * position.x + position.y * position.y);

clip(ringDistanceFromCenter - _InnerRingDiameter);
clip(1 - ringDistanceFromCenter);
clip(distance - _MinimumRenderDiatance);
//color.r = (distance - _MinimumRenderDiatance);
//color.r = distance < _MinimumRenderDiatance ? 1:0;

fixed opacity = clamp((distance - _MinimumRenderDiatance) / (_MaximumFadeDistance - _MinimumRenderDiatance), 0, 1);


fixed4 density = tex2D(_DensityMap, float2(clamp((ringDistanceFromCenter - _InnerRingDiameter) / (1 - _InnerRingDiameter), 0, 1), 0.5));
fixed3 color = fixed3(position.x, position.y, density.a);
o.Albedo = density.rgb;

o.Metallic = _Metallic * opacity;
o.Smoothness = _Glossiness * opacity;
o.Alpha = opacity * density.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
