Shader "CookbookShaders/Chapter2/Chapter2SpriteAttlas" 
{
	Properties 
	{
		_MainTex	("Base (RGB)", 2D)			= "white" {}

		_CellAmount	("Cell Amount", float)		= 0.0
		_Speed		("Speed", Range(0.01, 32))	= 12
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		float _CellAmount;
		float _Speed;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			float2 spriteUV = IN.uv_MainTex;
			float cellUVPercentage = 1 / _CellAmount;
			float frame = fmod(_Time.y * _Speed, _CellAmount);
			frame = floor(frame);
			float xValue = (spriteUV.x + frame) * cellUVPercentage;
			spriteUV = float2(xValue, spriteUV.y);

			half4 c = tex2D (_MainTex, spriteUV);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
