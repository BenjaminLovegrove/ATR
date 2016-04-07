// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32378,y:32793|diff-4068-OUT,diffpow-1597-OUT,emission-4070-RGB,alpha-1470-OUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:33122,y:32803,ptlb:Diffuse RGBA(alpha cutout),ptin:_DiffuseRGBAalphacutout,tex:1da2689537070b940b3e33a90279fa36,ntxv:0,isnm:False|UVIN-4000-UVOUT;n:type:ShaderForge.SFN_Slider,id:1459,x:32800,y:33261,ptlb:Alpha Cutout,ptin:_AlphaCutout,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:1470,x:32650,y:33131|A-4-A,B-1459-OUT;n:type:ShaderForge.SFN_Slider,id:1575,x:32781,y:32611,ptlb:Diffuse Power,ptin:_DiffusePower,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Power,id:1597,x:32793,y:32797|VAL-1575-OUT,EXP-1599-OUT;n:type:ShaderForge.SFN_Vector1,id:1599,x:32949,y:32841,v1:10;n:type:ShaderForge.SFN_TexCoord,id:1758,x:33810,y:32614,uv:0;n:type:ShaderForge.SFN_Time,id:1775,x:34038,y:32834;n:type:ShaderForge.SFN_ValueProperty,id:1776,x:34073,y:32997,ptlb:Min Speed,ptin:_MinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Slider,id:1777,x:33973,y:33168,ptlb:Rotation Speed,ptin:_RotationSpeed,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:1784,x:33863,y:32997|A-1776-OUT,B-1786-OUT,T-1777-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1786,x:34073,y:33077,ptlb:Max Speed,ptin:_MaxSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Panner,id:4000,x:33610,y:32578,spu:0,spv:-1.5|UVIN-1758-UVOUT,DIST-4067-OUT;n:type:ShaderForge.SFN_Multiply,id:4067,x:33733,y:32823|A-1775-TSL,B-1784-OUT;n:type:ShaderForge.SFN_Multiply,id:4068,x:33047,y:32574|A-4069-RGB,B-4-RGB;n:type:ShaderForge.SFN_Color,id:4069,x:33281,y:32499,ptlb:Diffuse Color,ptin:_DiffuseColor,glob:False,c1:0,c2:0.6275859,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4070,x:33004,y:32993,ptlb:Illum Map,ptin:_IllumMap,tex:06a57d6b6451b42f598a41b80e254ae9,ntxv:0,isnm:False|UVIN-4124-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4112,x:33387,y:33029,uv:0;n:type:ShaderForge.SFN_Time,id:4114,x:33615,y:33249;n:type:ShaderForge.SFN_ValueProperty,id:4116,x:33650,y:33412,ptlb:Illum - Min Speed,ptin:_IllumMinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Slider,id:4118,x:33550,y:33583,ptlb:Illum Offset Speed,ptin:_IllumOffsetSpeed,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:4120,x:33440,y:33412|A-4116-OUT,B-4122-OUT,T-4118-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4122,x:33650,y:33492,ptlb:Illum - Max Speed,ptin:_IllumMaxSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_Panner,id:4124,x:33187,y:32993,spu:0,spv:-1.5|UVIN-4112-UVOUT,DIST-4126-OUT;n:type:ShaderForge.SFN_Multiply,id:4126,x:33310,y:33238|A-4114-TSL,B-4120-OUT;proporder:4-1459-1575-1776-1777-1786-4069-4070-4116-4118-4122;pass:END;sub:END;*/

Shader "Shader Forge/DiffCutoutVtxAlphaOffset" {
    Properties {
        _DiffuseRGBAalphacutout ("Diffuse RGBA(alpha cutout)", 2D) = "white" {}
        _AlphaCutout ("Alpha Cutout", Range(0, 1)) = 1
        _DiffusePower ("Diffuse Power", Range(0, 1)) = 0
        _MinSpeed ("Min Speed", Float ) = 0
        _RotationSpeed ("Rotation Speed", Range(0, 1)) = 0
        _MaxSpeed ("Max Speed", Float ) = 0
        _DiffuseColor ("Diffuse Color", Color) = (0,0.6275859,1,1)
        _IllumMap ("Illum Map", 2D) = "white" {}
        _IllumMinSpeed ("Illum - Min Speed", Float ) = 0
        _IllumOffsetSpeed ("Illum Offset Speed", Range(0, 1)) = 0
        _IllumMaxSpeed ("Illum - Max Speed", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float _AlphaCutout;
            uniform float _DiffusePower;
            uniform float _MinSpeed;
            uniform float _RotationSpeed;
            uniform float _MaxSpeed;
            uniform float4 _DiffuseColor;
            uniform sampler2D _IllumMap; uniform float4 _IllumMap_ST;
            uniform float _IllumMinSpeed;
            uniform float _IllumOffsetSpeed;
            uniform float _IllumMaxSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = pow(max( 0.0, NdotL), pow(_DiffusePower,10.0)) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float4 node_4114 = _Time + _TimeEditor;
                float2 node_4124 = (i.uv0.rg+(node_4114.r*lerp(_IllumMinSpeed,_IllumMaxSpeed,_IllumOffsetSpeed))*float2(0,-1.5));
                float3 emissive = tex2D(_IllumMap,TRANSFORM_TEX(node_4124, _IllumMap)).rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_1775 = _Time + _TimeEditor;
                float2 node_4000 = (i.uv0.rg+(node_1775.r*lerp(_MinSpeed,_MaxSpeed,_RotationSpeed))*float2(0,-1.5));
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_4000, _DiffuseRGBAalphacutout));
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_4.rgb);
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,(node_4.a*_AlphaCutout));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBAalphacutout; uniform float4 _DiffuseRGBAalphacutout_ST;
            uniform float _AlphaCutout;
            uniform float _DiffusePower;
            uniform float _MinSpeed;
            uniform float _RotationSpeed;
            uniform float _MaxSpeed;
            uniform float4 _DiffuseColor;
            uniform sampler2D _IllumMap; uniform float4 _IllumMap_ST;
            uniform float _IllumMinSpeed;
            uniform float _IllumOffsetSpeed;
            uniform float _IllumMaxSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = pow(max( 0.0, NdotL), pow(_DiffusePower,10.0)) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_1775 = _Time + _TimeEditor;
                float2 node_4000 = (i.uv0.rg+(node_1775.r*lerp(_MinSpeed,_MaxSpeed,_RotationSpeed))*float2(0,-1.5));
                float4 node_4 = tex2D(_DiffuseRGBAalphacutout,TRANSFORM_TEX(node_4000, _DiffuseRGBAalphacutout));
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_4.rgb);
/// Final Color:
                return fixed4(finalColor * (node_4.a*_AlphaCutout),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
