Shader "Custom/MirrorShader" {
	Properties{
		_MainColor("Main Color",Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Cube("Cube",CUBE) = ""{}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		//cull off  

		CGPROGRAM
#pragma surface surf Lambert alpha  

		fixed4 _MainColor;
	sampler2D _MainTex;
	samplerCUBE _Cube;

	struct Input {
		float2 uv_MainTex;
		float3 worldRefl;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb*_MainColor.rgb;
		o.Emission = texCUBE(_Cube,IN.worldRefl).rgb;
		o.Alpha = c.a*_MainColor.a;
	}
	ENDCG
	}

		FallBack "Diffuse"
}