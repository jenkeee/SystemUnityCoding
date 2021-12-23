Shader "Custom/GlosyPlanet"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _FunTex("Fun Texture",2D)= "white"{}
        _Color ("Color", Color) = (1,1,1,0)
        _Amount ("Extrusion Amount", Range(0,1)) = 0.5
        }

    SubShader
    {
    Cull off
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
        ZTest LEqual
             CGPROGRAM    
        #pragma fragment frag Lambert alpha:fade
        #pragma vertex vert

      #include "UnityCG.cginc"

      sampler2D _FunTex;
      float4 _FunTex_ST;

      sampler2D _MainTex;
      float4 _MainTex_ST;
      float4 _Color;

      struct VertexData
      {
      float4 vertex : POSITION;
      float4 normal : NORMAL;
      float2 uv : TEXCOORD0;
      };

      struct v2f
      {
      float2 uv : TEXCOORD0;
      float4 vertex : SV_POSITION;
      };


      float _Amount;

        v2f vert (VertexData v)
        {
        v2f result;
        float4 vertex = v.vertex;
             vertex.xyz += v.normal * _Amount;
    result.vertex = UnityObjectToClipPos(vertex);
    result.uv = TRANSFORM_TEX(v.uv,_FunTex);

        return result;
        }

        fixed4 frag (v2f i) : SV_Target
        {
        fixed4 color;
        color = tex2D(_MainTex, i.uv);
        color = color * _Color;       
     
     return float4(1,1,1,0);
        }
        ENDCG
        }

        /*

Shader "Custom/TransparentDoubleSided" 
{
	Properties{
	_Color("Main Color", Color) = (1,1,1,1)
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader{
	Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
	Cull off
	LOD 200

	CGPROGRAM
	#pragma surface surf Lambert alpha:fade

	sampler2D _MainTex;
	fixed4 _Color;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
	}

	Fallback "Legacy Shaders/Transparent/VertexLit"
}

*/


                Pass
        {
        ZTest Greater
             CGPROGRAM    
        #pragma fragment frag
        #pragma vertex vert
      #include "UnityCG.cginc"

      sampler2D _FunTex;
      float4 _FunTex_ST;

      sampler2D _MainTex;
      float4 _MainTex_ST;
      float4 _Color;

      struct VertexData
      {
      float4 vertex : POSITION;
      float4 normal : NORMAL;
      float2 uv : TEXCOORD0;
      };

      struct v2f
      {
      float2 uv : TEXCOORD0;
      float4 vertex : SV_POSITION;
      };

       float _Amount;

        v2f vert (VertexData v)
        {
        v2f result;
        float4 vertex = v.vertex;
             vertex.xyz += v.normal * _Amount/2;
    result.vertex = UnityObjectToClipPos(vertex);
    result.uv = TRANSFORM_TEX(v.uv,_FunTex);

        return result;
        }

        fixed4 frag (v2f i) : SV_Target
        {
        fixed4 color;
        color = tex2D(_MainTex, i.uv);
        color = color * _Color;
              return color;
        }
        ENDCG
        }
    }  
}
