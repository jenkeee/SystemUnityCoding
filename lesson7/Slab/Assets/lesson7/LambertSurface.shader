Shader "Custom/LambertSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Emission("Emission", Color) = (1,1,1,1) //������� ���������� _Emission:
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Lambert noforwardadd ��� �������� ������� ������������� ������ �� 1-�� ��������� ��������� ���������.
        #pragma surface surf Lambert noforwardadd noshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        float4 _Emission; // ������� ���������� _Emission:

        struct Input
        {
            float2 uv_MainTex; //� ������� � ���� ��������� ���������� ������ UV-����������: ��������, ������� � ������� ���� ��� ����������� ��������
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

void surf (Input IN, inout SurfaceOutput o)
{
fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
o.Albedo = c.rgb;
o.Emission = _Emission.xyz;
o.Alpha = c.a;
}
        ENDCG
    }
    FallBack "Diffuse"
}
