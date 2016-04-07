// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32412,y:32725|diff-1347-OUT,alpha-72-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32751,y:32722,ptlb:Diffuse RGB,ptin:_DiffuseRGB,tex:a0dccc86186c8804bbfa47f400df4dfe,ntxv:0,isnm:False|UVIN-9-OUT;n:type:ShaderForge.SFN_Lerp,id:9,x:32926,y:32722|A-10-UVOUT,B-11-R,T-13-OUT;n:type:ShaderForge.SFN_TexCoord,id:10,x:33129,y:32588,uv:0;n:type:ShaderForge.SFN_Tex2d,id:11,x:33129,y:32780,ptlb:Diffuse Alpha (offset),ptin:_DiffuseAlphaoffset,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-12-UVOUT;n:type:ShaderForge.SFN_Panner,id:12,x:33314,y:32780,spu:0,spv:0.1;n:type:ShaderForge.SFN_Slider,id:13,x:33129,y:33023,ptlb:Distortion,ptin:_Distortion,min:0,cur:0.2006331,max:1;n:type:ShaderForge.SFN_Tex2d,id:38,x:32862,y:33189,ptlb:Alpha Texture(vert col mult),ptin:_AlphaTexturevertcolmult,tex:6cf014d92aa0e4f43a8af2776d6bd16a,ntxv:2,isnm:False|UVIN-1393-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:49,x:33038,y:33373;n:type:ShaderForge.SFN_Multiply,id:72,x:32669,y:33325|A-38-R,B-49-G,C-83-OUT;n:type:ShaderForge.SFN_Slider,id:83,x:32674,y:33523,ptlb:Alpha Amount,ptin:_AlphaAmount,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:1346,x:32751,y:32556,ptlb:Diff Color,ptin:_DiffColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1347,x:32530,y:32554|A-1346-RGB,B-2-RGB;n:type:ShaderForge.SFN_Panner,id:1393,x:33110,y:33189,spu:0.1,spv:0.1|UVIN-1394-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1394,x:33290,y:33189,uv:0;proporder:1346-2-11-13-38-83;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseLiquidVertCol" {
    Properties {
        _DiffColor ("Diff Color", Color) = (1,1,1,1)
        _DiffuseRGB ("Diffuse RGB", 2D) = "white" {}
        _DiffuseAlphaoffset ("Diffuse Alpha (offset)", 2D) = "white" {}
        _Distortion ("Distortion", Range(0, 1)) = 0.2006331
        _AlphaTexturevertcolmult ("Alpha Texture(vert col mult)", 2D) = "black" {}
        _AlphaAmount ("Alpha Amount", Range(0, 1)) = 1
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
            uniform sampler2D _DiffuseRGB; uniform float4 _DiffuseRGB_ST;
            uniform sampler2D _DiffuseAlphaoffset; uniform float4 _DiffuseAlphaoffset_ST;
            uniform float _Distortion;
            uniform sampler2D _AlphaTexturevertcolmult; uniform float4 _AlphaTexturevertcolmult_ST;
            uniform float _AlphaAmount;
            uniform float4 _DiffColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_5056 = _Time + _TimeEditor;
                float2 node_12 = (i.uv0.rg+node_5056.g*float2(0,0.1));
                float4 _DiffuseAlphaoffset_var = tex2D(_DiffuseAlphaoffset,TRANSFORM_TEX(node_12, _DiffuseAlphaoffset));
                float2 node_9 = lerp(i.uv0.rg,float2(_DiffuseAlphaoffset_var.r,_DiffuseAlphaoffset_var.r),_Distortion);
                finalColor += diffuseLight * (_DiffColor.rgb*tex2D(_DiffuseRGB,TRANSFORM_TEX(node_9, _DiffuseRGB)).rgb);
                float2 node_1393 = (i.uv0.rg+node_5056.g*float2(0.1,0.1));
/// Final Color:
                return fixed4(finalColor,(tex2D(_AlphaTexturevertcolmult,TRANSFORM_TEX(node_1393, _AlphaTexturevertcolmult)).r*i.vertexColor.g*_AlphaAmount));
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
            uniform sampler2D _DiffuseRGB; uniform float4 _DiffuseRGB_ST;
            uniform sampler2D _DiffuseAlphaoffset; uniform float4 _DiffuseAlphaoffset_ST;
            uniform float _Distortion;
            uniform sampler2D _AlphaTexturevertcolmult; uniform float4 _AlphaTexturevertcolmult_ST;
            uniform float _AlphaAmount;
            uniform float4 _DiffColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_5058 = _Time + _TimeEditor;
                float2 node_12 = (i.uv0.rg+node_5058.g*float2(0,0.1));
                float4 _DiffuseAlphaoffset_var = tex2D(_DiffuseAlphaoffset,TRANSFORM_TEX(node_12, _DiffuseAlphaoffset));
                float2 node_9 = lerp(i.uv0.rg,float2(_DiffuseAlphaoffset_var.r,_DiffuseAlphaoffset_var.r),_Distortion);
                finalColor += diffuseLight * (_DiffColor.rgb*tex2D(_DiffuseRGB,TRANSFORM_TEX(node_9, _DiffuseRGB)).rgb);
                float2 node_1393 = (i.uv0.rg+node_5058.g*float2(0.1,0.1));
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaTexturevertcolmult,TRANSFORM_TEX(node_1393, _AlphaTexturevertcolmult)).r*i.vertexColor.g*_AlphaAmount),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
