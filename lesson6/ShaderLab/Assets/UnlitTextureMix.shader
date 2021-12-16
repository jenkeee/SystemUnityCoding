Shader "Custom/UnlitTextureMix"
{
	//�������� �������
	Properties
	{
	_Tex1("Texture1", 2D) = "white" {} // ��������1
	_Tex2("Texture2", 2D) = "white" {} // ��������2
	_MixValue("Mix Value", Range(0,1)) = 0.5 // �������� ���������� �������
	_Color("Main Color", COLOR) = (1,1,1,1) // ���� �����������
		_Height("Height", Range(-20,20)) = 0.5 // ���� ������
	}
		//���������
		SubShader
			{

		Tags{ "RenderType" = "Opaque" } // ���, ����������, ��� ������ ������������
			LOD 100 // ����������� ������� �����������
		Cull off // ��������� ��������� � ���� ������ // ���� ��� ����� Front Back
		ZWrite off // �������� ������ � z ������
		Fog {Mode off} // ����� ��������� ������ �������� ���� ���
		Blend One One // Adictive // ����� ��������� ���� � ������������


			Pass
		{
		CGPROGRAM
		#pragma vertex vert // ��������� ��� ��������� ������
		#pragma fragment frag // ��������� ��� ��������� ����������
		#include "UnityCG.cginc" // ���������� � ��������� ���������
		sampler2D _Tex1; // ��������1
		float4 _Tex1_ST;
		sampler2D _Tex2; // ��������2
		float4 _Tex2_ST;
		float _MixValue; // �������� ����������
		float4 _Color; // ����, ������� ����� ������������ �����������
		float _Height; // ���� ������

		// ���������, ������� �������� ������������� ������ ������� � ������ ���������
		struct v2f
		{
		float2 uv : TEXCOORD0; // UV-���������� �������
		float4 vertex : SV_POSITION; // ���������� �������
		};
		//����� ���������� ��������� ������
		v2f vert(appdata_full v)
		{		
		v2f result; // ������� ���������� ���� v2f ��� ��� ������ ������� ��������
		//v.vertex.y += v.normal +_Height * -v.texcoord.y * v.texcoord.x;
//	v.vertex.xyz += v.normal +_Height * -v.texcoord.y * v.texcoord.x;
		//v.vertex.xyz += v.normal * _Height;
		/*
		if(v.vertex.z > 0.5)
		{          
		v.vertex.x += v.normal + _Height ;
		}
		else
		{
		v.vertex.y += v.normal + -v.texcoord.y*v.texcoord.y *_Height ;
		}*/

		//result = float4(0.1f,0.1f,0.1f,0.1f)*_Height;
	//	v.vertex.xyz -= v.normal * _Height * v.texcoord.x * v.texcoord.x + v.normal * 0.5 + 0.5;


		//v.vertex.y += v.normal + _Height * v.texcoord.y;//�������� ��������� left side
		if(v.vertex.z <= 0.5)
		{          
		v.vertex.xyz += v.normal * _Height * v.texcoord.xyz * v.texcoord.xyz;
		}
		else{v.vertex.xyz += v.normal * -_Height * v.texcoord.xyz * v.texcoord.xyz;}
				
		result.vertex = UnityObjectToClipPos(v.vertex);
		result.uv = TRANSFORM_TEX(v.texcoord, _Tex1);
		return result; // ������ ����������
		}
		//����� ���������� ��������� ��������, ���� �������� ���������� �� ����	���������
		fixed4 frag(v2f i) : SV_Target
		{
		fixed4 color;
		color = tex2D(_Tex1, i.uv) * _MixValue;
		color += tex2D(_Tex2, i.uv) * (1 - _MixValue);
		color = color * _Color;
		return color;

		}
		ENDCG
		}
				}

}