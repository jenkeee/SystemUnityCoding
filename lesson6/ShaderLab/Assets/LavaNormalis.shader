Shader "Custom/LavaNormalis"
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("NormalMap",2D) = "white"{}
		_SecondTex ("Lava_Texture",2D) = "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Str ("Strenght", Range(0,10)) = 1
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _SecondTex;
		//float4 _SecondTex_ST;

		struct Input //и добавлением переменных с uv координатами в входную структуру Input 
		{
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float2 uv2_SecondTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _Str;
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			o.Normal = UnpackNormal(tex2D(_NormalMap,IN.uv_NormalMap)); //распакуем карту нормалей
			float3 norm = o.Normal; // запишем карут нормалей
			fixed4 lava = tex2D(_SecondTex,(IN.uv2_SecondTex)); //Затем создадим переменные для льда и лавы: 
			fixed4 ice = tex2D(_MainTex,IN.uv_MainTex); // Затем создадим переменные для льда и лавы: 

			/*Уже намного лучше. Заменим красный цвет на лаву:
fixed4 col = lerp(ice,lava,length(norm.xy)*_Str);*/
			fixed4 col = lerp(ice * _Color,max(lava-0.5,lava*(_SinTime.w+_CosTime.x)*length(norm.xy)*_Str),length(norm.xy)*_Str);
			//за трещины отвечает xy компонента вектора нормали. Интерполируем цвет между цветом льда и красным с использованием длинны norm.xy:

			o.Albedo = col.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = col.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
