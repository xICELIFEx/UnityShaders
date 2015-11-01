﻿Shader "CookbookShaders/Chapter3/Chapter3BlinnPhong" 
{
	Properties
	{
		_MainTint		("Diffuse Tint", Color)				= (1,1,1,1)
		_MainTex		("Base (RGB)", 2D)					= "white" {}
		_SpecularColor	("Specular Color", Color)			= (1,1,1,1)
		_SpecPower		("Specular Power", Range(0.1, 60))	= 3
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf CustomBlinnPhong

		sampler2D	_MainTex;
		float4		_MainTint;
		float4		_SpecularColor;
		float		_SpecPower;

		inline fixed4 LightingCustomBlinnPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
		{
			float3 halfVector = normalize(lightDir + viewDir);

			float diff = max(0, dot(s.Normal, lightDir));

			float nh = max(0, dot(s.Normal, halfVector));
			float spec = pow(nh, _SpecPower) * _SpecularColor;

			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff) + (_LightColor0.rgb * _SpecularColor.rgb * spec) * (atten * 2);
			c.a = s.Alpha;
			return c;
		}

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
