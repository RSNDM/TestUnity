Shader "Custom/FragmentShader" {
	properties{
		_MainTint("Main Color", Color) = (1, 1, 1, 1)
		_AlphaVal("Alpha", Range(0, 1)) = 0.1
	}
		SubShader{
		Tags{ "Queue" = "Transparent" }

		PASS{

		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert    
#pragma fragment frag  

#include "UnityCG.cginc"  
#include "Lighting.cginc"  


		float4 _MainTint;
	float _AlphaVal;


	struct v2f {
		float4 pos:POSITION;
		float3 normal1:texcoord1;
		float4 vertex1:texcoord2;
		float4 col:texcoord0;
	};

	v2f vert(appdata_base v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.normal1 = v.normal;
		o.vertex1 = v.vertex;
		return o;
	}

	float4 frag(v2f IN) :COLOR{
		float4 col = UNITY_LIGHTMODEL_AMBIENT;
		float3 N = UnityObjectToWorldNormal(IN.normal1);
		float3 L = normalize(WorldSpaceLightDir(IN.vertex1));
		float diffuseScale = saturate(dot(N,L));
		col += _LightColor0 * diffuseScale + _MainTint;
		col[3] *= _AlphaVal;
		return col;
	}
		ENDCG
	}
	}
		FallBack "Diffuse"

}