Shader "RimLighting"
{
	Properties 
	{
_diffuse("_diffuse", 2D) = "black" {}
_diffuse_color("_diffuse_color", Color) = (1,0.9701493,0.9701493,1)
_specular("_specular", 2D) = "black" {}
_specular_color("_specular_color", Color) = (0.3742918,0.3555358,0.4179105,1)
_specular_power("_specular_power", Range(0,100) ) = 100
_normal("_normal", 2D) = "bump" {}
_rim_color("_rim_color", Color) = (0.06716418,0.06716418,0.06716418,1)
_rim_power("_rim_power", Range(0.1,5) ) = 5
_glossiness_power("_glossiness_power", Range(0,1) ) = 1

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _diffuse;
float4 _diffuse_color;
sampler2D _specular;
float4 _specular_color;
float _specular_power;
sampler2D _normal;
float4 _rim_color;
float _rim_power;
float _glossiness_power;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_diffuse;
float2 uv_normal;
float3 viewDir;
float2 uv_specular;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_diffuse,(IN.uv_diffuse.xyxy).xy);
float4 Multiply3=_diffuse_color * Tex2D0;
float4 Tex2D2=tex2D(_normal,(IN.uv_normal.xyxy).xy);
float4 UnpackNormal0=float4(UnpackNormal(Tex2D2).xyz, 1.0);
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,_rim_power.xxxx);
float4 Multiply0=_rim_color * Pow0;
float4 Multiply2=_specular_color * _specular_power.xxxx;
float4 Tex2D1=tex2D(_specular,(IN.uv_specular.xyxy).xy);
float4 Multiply1=Multiply2 * Tex2D1;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Multiply3;
o.Normal = UnpackNormal0;
o.Emission = Multiply0;
o.Specular = _glossiness_power.xxxx;
o.Gloss = Multiply1;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}