Shader "Custom/UnlitTextureMix"
{
	//свойства шейдера
	Properties
	{
	_Tex1("Texture1", 2D) = "white" {} // текстура1
	_Tex2("Texture2", 2D) = "white" {} // текстура2
	_MixValue("Mix Value", Range(0,1)) = 0.5 // параметр смешивания текстур
	_Color("Main Color", COLOR) = (1,1,1,1) // цвет окрашивания
		_Height("Height", Range(-20,20)) = 0.5 // сила изгиба
	}
		//сабшейдер
		SubShader
			{

		Tags{ "RenderType" = "Opaque" } // тег, означающий, что шейдер непрозрачный
			LOD 100 // минимальный уровень детализации
		Cull off // установим отрисовку с двух сторон // есть еще опции Front Back
		ZWrite off // отключим запись в z буффер
		Fog {Mode off} // режим отрисовки тумана выключим авто мод
		Blend One One // Adictive // вывод геометрии есть в документации


			Pass
		{
		CGPROGRAM
		#pragma vertex vert // директива для обработки вершин
		#pragma fragment frag // директива для обработки фрагментов
		#include "UnityCG.cginc" // библиотека с полезными функциями
		sampler2D _Tex1; // текстура1
		float4 _Tex1_ST;
		sampler2D _Tex2; // текстура2
		float4 _Tex2_ST;
		float _MixValue; // параметр смешивания
		float4 _Color; // цвет, которым будет окрашиваться изображение
		float _Height; // сила изгиба

		// структура, которая помогает преобразовать данные вершины в данные фрагмента
		struct v2f
		{
		float2 uv : TEXCOORD0; // UV-координаты вершины
		float4 vertex : SV_POSITION; // координаты вершины
		};
		//здесь происходит обработка вершин
		v2f vert(appdata_full v)
		{		
		v2f result; // заводим переменную типа v2f так все делают говорит интернет
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


		//v.vertex.y += v.normal + _Height * v.texcoord.y;//опускает поднимает left side
		if(v.vertex.z <= 0.5)
		{          
		v.vertex.xyz += v.normal * _Height * v.texcoord.xyz * v.texcoord.xyz;
		}
		else{v.vertex.xyz += v.normal * -_Height * v.texcoord.xyz * v.texcoord.xyz;}
				
		result.vertex = UnityObjectToClipPos(v.vertex);
		result.uv = TRANSFORM_TEX(v.texcoord, _Tex1);
		return result; // вернем переменную
		}
		//здесь происходит обработка пикселей, цвет пикселей умножается на цвет	материала
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