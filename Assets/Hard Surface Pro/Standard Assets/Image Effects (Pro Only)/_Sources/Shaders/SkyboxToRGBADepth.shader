// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/SkyboxToRGBADepth" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "" {}
	}
	
	// Shader code pasted into all further CGPROGRAM blocks
	CGINCLUDE
		
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	uniform float noSkyBoxMask;
	uniform float dontUseSkyboxBrightness;
		
	sampler2D _MainTex;
		
	v2f vert( appdata_img v ) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;
		return o;
	}
	
	half4 frag(v2f i) : COLOR {
		float4 skyboxValue = tex2D( _MainTex, i.uv.xy );
		return max( skyboxValue.a, noSkyBoxMask * dot(skyboxValue.rgb,float3(0.59,0.3,0.11)) ); 
	}

	ENDCG
	
Subshader {
	
 Pass {
	  ZTest Always Cull Off ZWrite Off
	  Fog { Mode off }      

      CGPROGRAM

	  #pragma fragmentoption ARB_precision_hint_fastest 
      #pragma vertex vert
      #pragma fragment frag

      // the Cg code itself

      ENDCG
      // ... the rest of pass setup ...
  }
}

Fallback off
	
} // shader