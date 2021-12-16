Shader "Custom/GlosyPlanet"
{
    Properties
    {
        _MainTex ("Main Color", 2D) = "white"{}
        _FunTex("Fun Texture",2D)= "white"{}
        _Color ("Color", Color) = (1,1,1,1)
        }

    SubShader
    {
    Cull off
        Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
        LOD 100

        Pass
        {
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

        v2f vert (VertexData v)
        {
        v2f result;
        float4 vertex = v.vertex;
    vertex.y = v.vertex.y + v.uv.x* v.uv.x;
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
                Pass
        {
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

        v2f vert (VertexData v)
        {
        v2f result;
        float4 vertex = v.vertex;
    vertex.y = v.vertex.y - v.uv.x* v.uv.x;
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
