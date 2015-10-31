Shader "CookbookShaders/Chapter2/Chapter2LevelEffect" 
{
	Properties
	{
		_MainTex	("Base (RGB)", 2D)				= "white" {}
		_inBlack	("Input Black", Range(0, 255))	= 0
		_inGamma	("Input Gamma", Range(0, 2))	= 1.61
		_inWhite	("Input White", Range(0,255))	= 255

		_outWhite	("Output White", Range(0, 255))	= 255
		_outBlack	("Output Black", Range(0, 255))	= 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _inBlack;
		float _inGamma;
		float _inWhite;
		float _outWhite;
		float _outBlack;

		struct Input
		{
			float2 uv_MainTex;
		};

		float GetPixelChanelLevel(float pixelColor)
		{
			float pixelResult;
			pixelResult = (pixelColor * 255.0);
			pixelResult = max(0, pixelResult - _inBlack);
			pixelResult = saturate(pow(pixelResult / (_inWhite - _inBlack), _inGamma));
			pixelResult = (pixelResult * (_outWhite - _outBlack) + _outBlack) / 255.0;
			return pixelResult;
		}

		float3 GetPixelLevel(float3 pixel)
		{
			float3 result;
			result.r = GetPixelChanelLevel(pixel.r);
			result.g = GetPixelChanelLevel(pixel.g);
			result.b = GetPixelChanelLevel(pixel.b);
			return result;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);

			float outRPixel;
			outRPixel = (c.r * 255.0);
			outRPixel = max(0, outRPixel - _inBlack);
			outRPixel = saturate(pow(outRPixel / (_inWhite - _inBlack), _inGamma));
			outRPixel = (outRPixel * (_outWhite - _outBlack) + _outBlack) / 255.0;

			o.Albedo = GetPixelLevel(c.rgb);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
