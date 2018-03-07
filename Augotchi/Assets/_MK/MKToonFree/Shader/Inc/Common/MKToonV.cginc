//uniform variables
#ifndef MK_TOON_V
	#define MK_TOON_V

	/////////////////////////////////////////////////////////////////////////////////////////////
	// UNIFORM VARIABLES
	/////////////////////////////////////////////////////////////////////////////////////////////

	//enabled uniform variables only if needed

	//Main
	uniform fixed4 _Color;
	uniform fixed4 _Color2;
	uniform fixed4 _Color3;
	uniform sampler2D _MainTex;
	uniform sampler2D _OffTex;
	uniform sampler2D _MiscTex;
	uniform sampler2D _MapTex;
	uniform sampler2D _MapTex2;
	uniform sampler2D _MapTex3;
	uniform float4 _MainTex_ST;
	uniform half _Brightness;

	//Normalmap
	uniform sampler2D _BumpMap;
	uniform sampler2D _BumpMap2;
	uniform sampler2D _BumpMap3;

	//Light
	uniform fixed4 _LightColor0;
	uniform half _LightThreshold;

	//Render
	uniform half _LightSmoothness;
	uniform half _RimSmoothness;

	//Custom shadow
	uniform fixed3 _ShadowColor;
	uniform fixed3 _HighlightColor;
	uniform fixed _ShadowIntensity;

	//Rim
	uniform fixed3 _RimColor;
	uniform half _RimSize;
	uniform fixed _RimIntensity;

	//Specular
	uniform half _Shininess;
	uniform fixed3 _SpecColor;
	uniform fixed _SpecularIntensity;

	//Outline
	#ifdef MKTOON_OUTLINE_PASS_ONLY
		uniform fixed4 _OutlineColor;
		uniform half _OutlineSize;
	#endif

	//Emission
	uniform fixed3 _EmissionColor;
#endif