Shader "PDI/WaterRipple" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Bump ("Bump", 2D) = "white" {}

		_Speed("Speed", float) = 1
		_Frequency("Frequency", float) = 1
		_Scale("Scale", float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bump;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
		};

		fixed4 _Color;

		float _Speed;
		float _Frequency;
		float _Scale;

		void vert(inout appdata_full v)
		{
			//Calcula a area do plano afetada
			half offset = (v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z);
			//Calcula a altura e a posição dos vertices, aplicando a variavel de tempo para animar as ondas
			half value = _Scale * sin(_Time.w * _Speed + offset * _Frequency);
			
			v.vertex.y += value;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//Textura mapeada aplicando uma cor
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//Calculo da aplicação da normal
			fixed3 b = UnpackNormal(tex2D(_Bump, IN.uv_Bump));

			o.Normal = b;
			o.Albedo = c.rgb;

			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}
