z// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/GrabGlass"
{
	Properties
	{
		_Color("Main Color",Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{


		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }//Opaque  
		LOD 100

		//绘制半透明物体 关闭深度缓存  
		ZWrite Off
		//透明混合  
		Blend SrcAlpha OneMinusSrcAlpha

		//如果没有命名,则可以用_GrabTexture来读取,不过开销很大,应用到特殊效果时才去应用  
		GrabPass
	{
		"_GrabTex"
	}

		Pass
	{
		CGPROGRAM
#pragma vertex vert  
#pragma fragment frag  
		// make fog work  
#pragma multi_compile_fog  

#include "UnityCG.cginc"  

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed4 _Color;
	sampler2D _GrabTex;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture  
		fixed4 col = tex2D(_MainTex, i.uv)*_Color;

	// apply fog  
	UNITY_APPLY_FOG(i.fogCoord, col);
	//调整一下uv  
	float2 uv = i.uv;
	uv.x = 1 - uv.x;
	return col * tex2D(_GrabTex,uv);
	}
		ENDCG
	}
	}
}