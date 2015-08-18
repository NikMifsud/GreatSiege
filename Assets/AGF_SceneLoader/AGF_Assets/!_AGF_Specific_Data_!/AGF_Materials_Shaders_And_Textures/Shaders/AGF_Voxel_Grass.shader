Shader "AGF/AGF_Voxel_Grass" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse(RGB). Cutout(A)", 2D) = "gray" {}
		_Cutoff ("Alpha Ref", Range(0,1)) = 0.33
		_Ambient ("Additional Ambient", Color) = (1,1,1,1)
		_AnimStrength ("Animation Strength", Range(0,1)) = 0.33
	} 
	SubShader 
	{
		Cull Off
		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
		struct Input 
		{
			float2 uv_MainTex;        
			float3 worldPos;
			float3 screenPos;
			float4 color : Color;
		};
		sampler2D _MainTex;
		half3 _Ambient;
		fixed4 _Color;
		float _GrassAnimState;
		float _AnimStrength;
		void vert (inout appdata_full v) 
		{	
			float linearState = cos(_GrassAnimState + v.color.g*6.283185307179586476925286766559);
			v.vertex.xyz += float3(1-v.color.r*2, 0, 1-v.color.b*2) * linearState * _AnimStrength;
		}
		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color; 
			o.Alpha = c.a;

			o.Emission = o.Albedo * _Ambient * IN.color.a; 
		}
		ENDCG
    }
    Fallback "Transparent/Cutout/VertexLit"
}