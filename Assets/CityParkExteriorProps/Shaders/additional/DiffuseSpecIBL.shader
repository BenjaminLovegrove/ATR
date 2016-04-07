// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32509,y:32664|diff-2-RGB,spec-2-A,amdfl-9-OUT,amspl-26-OUT,alpha-61-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32944,y:32436,ptlb:Diffuse (RGB) Spec (A),ptin:_DiffuseRGBSpecA,tex:14bb96bd6f2891147a98ab0bd1662452,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9,x:33132,y:32573|A-10-OUT,B-12-RGB;n:type:ShaderForge.SFN_Multiply,id:10,x:33332,y:32533|A-11-OUT,B-12-A;n:type:ShaderForge.SFN_ValueProperty,id:11,x:33560,y:32533,ptlb:Diff IBL Value,ptin:_DiffIBLValue,glob:False,v1:1;n:type:ShaderForge.SFN_Cubemap,id:12,x:33560,y:32635,ptlb:Diff IBL,ptin:_DiffIBL,cube:f466cf7415226e046b096197eb7341aa,pvfc:0|DIR-13-OUT;n:type:ShaderForge.SFN_NormalVector,id:13,x:33765,y:32635,pt:True;n:type:ShaderForge.SFN_Multiply,id:26,x:33124,y:32881|A-28-OUT,B-32-RGB;n:type:ShaderForge.SFN_Multiply,id:28,x:33324,y:32841|A-30-OUT,B-32-A;n:type:ShaderForge.SFN_ValueProperty,id:30,x:33552,y:32841,ptlb:Spec IBL Value,ptin:_SpecIBLValue,glob:False,v1:1;n:type:ShaderForge.SFN_Cubemap,id:32,x:33552,y:32943,ptlb:Spec IBL,ptin:_SpecIBL,cube:f466cf7415226e046b096197eb7341aa,pvfc:1|DIR-161-OUT;n:type:ShaderForge.SFN_Tex2d,id:60,x:32948,y:33270,ptlb:Alpha Map (R) Offset,ptin:_AlphaMapROffset,tex:6cf014d92aa0e4f43a8af2776d6bd16a,ntxv:0,isnm:False|UVIN-99-UVOUT;n:type:ShaderForge.SFN_Multiply,id:61,x:32763,y:33270|A-73-R,B-60-R,C-62-OUT;n:type:ShaderForge.SFN_Slider,id:62,x:32948,y:33451,ptlb:node_62,ptin:_node_62,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:73,x:32948,y:33080,ptlb:Alpha Map (R),ptin:_AlphaMapR,tex:6e77d387966489d6cb6bb6ba39e09c45,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:99,x:33215,y:33118,spu:0.2,spv:1.5|UVIN-103-UVOUT,DIST-105-OUT;n:type:ShaderForge.SFN_ValueProperty,id:100,x:33661,y:33434,ptlb:Alpha Map (R) Offset Min,ptin:_AlphaMapROffsetMin,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:101,x:33661,y:33525,ptlb:Alpha Map (R) Offset Max,ptin:_AlphaMapROffsetMax,glob:False,v1:20;n:type:ShaderForge.SFN_Slider,id:102,x:33661,y:33621,ptlb:Alpha Map (R) Offset Value,ptin:_AlphaMapROffsetValue,min:0,cur:0.3007519,max:1;n:type:ShaderForge.SFN_TexCoord,id:103,x:33410,y:33118,uv:0;n:type:ShaderForge.SFN_Time,id:104,x:33661,y:33282;n:type:ShaderForge.SFN_Multiply,id:105,x:33409,y:33282|A-104-TSL,B-106-OUT;n:type:ShaderForge.SFN_Lerp,id:106,x:33490,y:33434|A-100-OUT,B-101-OUT,T-102-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:161,x:33769,y:32943;proporder:2-11-12-30-32-60-62-73-100-101-102;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpecIBL" {
    Properties {
        _DiffuseRGBSpecA ("Diffuse (RGB) Spec (A)", 2D) = "white" {}
        _DiffIBLValue ("Diff IBL Value", Float ) = 1
        _DiffIBL ("Diff IBL", Cube) = "_Skybox" {}
        _SpecIBLValue ("Spec IBL Value", Float ) = 1
        _SpecIBL ("Spec IBL", Cube) = "_Skybox" {}
        _AlphaMapROffset ("Alpha Map (R) Offset", 2D) = "white" {}
        _node_62 ("node_62", Range(0, 1)) = 1
        _AlphaMapR ("Alpha Map (R)", 2D) = "white" {}
        _AlphaMapROffsetMin ("Alpha Map (R) Offset Min", Float ) = 0
        _AlphaMapROffsetMax ("Alpha Map (R) Offset Max", Float ) = 20
        _AlphaMapROffsetValue ("Alpha Map (R) Offset Value", Range(0, 1)) = 0.3007519
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform float _DiffIBLValue;
            uniform samplerCUBE _DiffIBL;
            uniform float _SpecIBLValue;
            uniform samplerCUBE _SpecIBL;
            uniform sampler2D _AlphaMapROffset; uniform float4 _AlphaMapROffset_ST;
            uniform float _node_62;
            uniform sampler2D _AlphaMapR; uniform float4 _AlphaMapR_ST;
            uniform float _AlphaMapROffsetMin;
            uniform float _AlphaMapROffsetMax;
            uniform float _AlphaMapROffsetValue;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_32 = texCUBE(_SpecIBL,viewReflectDirection);
                float2 node_172 = i.uv0;
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_172.rg, _DiffuseRGBSpecA));
                float3 specularColor = float3(node_2.a,node_2.a,node_2.a);
                float3 specularAmb = ((_SpecIBLValue*node_32.a)*node_32.rgb) * specularColor;
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor + specularAmb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_12 = texCUBE(_DiffIBL,normalDirection);
                diffuseLight += ((_DiffIBLValue*node_12.a)*node_12.rgb); // Diffuse Ambient Light
                finalColor += diffuseLight * node_2.rgb;
                finalColor += specular;
                float4 node_104 = _Time + _TimeEditor;
                float2 node_99 = (i.uv0.rg+(node_104.r*lerp(_AlphaMapROffsetMin,_AlphaMapROffsetMax,_AlphaMapROffsetValue))*float2(0.2,1.5));
/// Final Color:
                return fixed4(finalColor,(tex2D(_AlphaMapR,TRANSFORM_TEX(node_172.rg, _AlphaMapR)).r*tex2D(_AlphaMapROffset,TRANSFORM_TEX(node_99, _AlphaMapROffset)).r*_node_62));
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
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform sampler2D _AlphaMapROffset; uniform float4 _AlphaMapROffset_ST;
            uniform float _node_62;
            uniform sampler2D _AlphaMapR; uniform float4 _AlphaMapR_ST;
            uniform float _AlphaMapROffsetMin;
            uniform float _AlphaMapROffsetMax;
            uniform float _AlphaMapROffsetValue;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_173 = i.uv0;
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_173.rg, _DiffuseRGBSpecA));
                float3 specularColor = float3(node_2.a,node_2.a,node_2.a);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_2.rgb;
                finalColor += specular;
                float4 node_104 = _Time + _TimeEditor;
                float2 node_99 = (i.uv0.rg+(node_104.r*lerp(_AlphaMapROffsetMin,_AlphaMapROffsetMax,_AlphaMapROffsetValue))*float2(0.2,1.5));
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaMapR,TRANSFORM_TEX(node_173.rg, _AlphaMapR)).r*tex2D(_AlphaMapROffset,TRANSFORM_TEX(node_99, _AlphaMapROffset)).r*_node_62),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
