// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32061,y:32718|diff-1347-OUT,spec-2-R,gloss-3746-OUT,normal-1400-RGB,emission-3262-OUT,alpha-72-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32752,y:32728,ptlb:Distort Diffuse RGB,ptin:_DistortDiffuseRGB,ntxv:0,isnm:False|UVIN-9-OUT;n:type:ShaderForge.SFN_Lerp,id:9,x:32926,y:32722|A-10-UVOUT,B-11-RGB,T-13-OUT;n:type:ShaderForge.SFN_TexCoord,id:10,x:33132,y:32395,uv:0;n:type:ShaderForge.SFN_Tex2d,id:11,x:33132,y:32587,ptlb:Distort Alpha Map (offset),ptin:_DistortAlphaMapoffset,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-12-UVOUT;n:type:ShaderForge.SFN_Panner,id:12,x:33323,y:32587,spu:0.025,spv:0.05;n:type:ShaderForge.SFN_Slider,id:13,x:33107,y:32779,ptlb:Distortion,ptin:_Distortion,min:0,cur:0.2206012,max:1;n:type:ShaderForge.SFN_Tex2d,id:38,x:33105,y:33179,ptlb:Alpha Texture(vert col mult),ptin:_AlphaTexturevertcolmult,tex:e976c6d3ebc1e4a85aa7186a3192d8c5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:49,x:33105,y:33356;n:type:ShaderForge.SFN_Multiply,id:72,x:32652,y:33359|A-38-RGB,B-49-G,C-83-OUT;n:type:ShaderForge.SFN_Slider,id:83,x:32809,y:33524,ptlb:Alpha Amount,ptin:_AlphaAmount,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:1346,x:32751,y:32556,ptlb:Diff Color,ptin:_DiffColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1347,x:32321,y:32544|A-1346-RGB,B-2-RGB,C-3509-OUT;n:type:ShaderForge.SFN_Tex2d,id:1400,x:34520,y:32731,ptlb:Normal,ptin:_Normal,ntxv:3,isnm:True|UVIN-1412-UVOUT;n:type:ShaderForge.SFN_Panner,id:1412,x:34699,y:32689,spu:0,spv:0.08;n:type:ShaderForge.SFN_Multiply,id:1711,x:33598,y:32868|A-1729-OUT,B-1732-OUT;n:type:ShaderForge.SFN_Multiply,id:1729,x:33916,y:32832|A-1737-OUT,B-1740-OUT;n:type:ShaderForge.SFN_Vector1,id:1732,x:33916,y:32969,v1:6;n:type:ShaderForge.SFN_OneMinus,id:1737,x:34134,y:32832|IN-1757-OUT;n:type:ShaderForge.SFN_Power,id:1740,x:34134,y:32969|VAL-1744-R,EXP-1743-OUT;n:type:ShaderForge.SFN_Vector1,id:1743,x:34333,y:33141,v1:3;n:type:ShaderForge.SFN_Tex2d,id:1744,x:34333,y:32969,ptlb:Illum Alpha Map,ptin:_IllumAlphaMap,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-1977-UVOUT;n:type:ShaderForge.SFN_Power,id:1757,x:34332,y:32791|VAL-1400-R,EXP-1762-OUT;n:type:ShaderForge.SFN_Vector1,id:1762,x:34520,y:32915,v1:100;n:type:ShaderForge.SFN_Panner,id:1977,x:34520,y:32976,spu:0.03,spv:0.2|UVIN-1978-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1978,x:34714,y:32976,uv:0;n:type:ShaderForge.SFN_Color,id:2230,x:33381,y:33057,ptlb:Illum Color,ptin:_IllumColor,glob:False,c1:0,c2:0.8602941,c3:0.4331135,c4:1;n:type:ShaderForge.SFN_Blend,id:3259,x:33381,y:32868,blmd:10,clmp:True|SRC-1711-OUT,DST-3261-OUT;n:type:ShaderForge.SFN_Slider,id:3261,x:33598,y:33032,ptlb:Illum Value,ptin:_IllumValue,min:0,cur:0.1548779,max:1;n:type:ShaderForge.SFN_Multiply,id:3262,x:33129,y:32868|A-3259-OUT,B-2230-RGB;n:type:ShaderForge.SFN_ValueProperty,id:3509,x:32485,y:32699,ptlb:Diff Value,ptin:_DiffValue,glob:False,v1:5;n:type:ShaderForge.SFN_Slider,id:3746,x:32428,y:32903,ptlb:Gloss Value,ptin:_GlossValue,min:0,cur:0.09774436,max:1;proporder:1346-3509-3746-2-11-13-1400-2230-3261-1744-38-83-3872;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseAlphaOffsetVrtClrIllum" {
    Properties {
        _DiffColor ("Diff Color", Color) = (0.5,0.5,0.5,1)
        _DiffValue ("Diff Value", Float ) = 5
        _GlossValue ("Gloss Value", Range(0, 1)) = 0.09774436
        _DistortDiffuseRGB ("Distort Diffuse RGB", 2D) = "white" {}
        _DistortAlphaMapoffset ("Distort Alpha Map (offset)", 2D) = "white" {}
        _Distortion ("Distortion", Range(0, 1)) = 0.2206012
        _Normal ("Normal", 2D) = "bump" {}
        _IllumColor ("Illum Color", Color) = (0,0.8602941,0.4331135,1)
        _IllumValue ("Illum Value", Range(0, 1)) = 0.1548779
        _IllumAlphaMap ("Illum Alpha Map", 2D) = "white" {}
        _AlphaTexturevertcolmult ("Alpha Texture(vert col mult)", 2D) = "white" {}
        _AlphaAmount ("Alpha Amount", Range(0, 1)) = 1
        _node_3872 ("node_3872", Float ) = 0
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
            uniform sampler2D _DistortDiffuseRGB; uniform float4 _DistortDiffuseRGB_ST;
            uniform sampler2D _DistortAlphaMapoffset; uniform float4 _DistortAlphaMapoffset_ST;
            uniform float _Distortion;
            uniform sampler2D _AlphaTexturevertcolmult; uniform float4 _AlphaTexturevertcolmult_ST;
            uniform float _AlphaAmount;
            uniform float4 _DiffColor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _IllumAlphaMap; uniform float4 _IllumAlphaMap_ST;
            uniform float4 _IllumColor;
            uniform float _IllumValue;
            uniform float _DiffValue;
            uniform float _GlossValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_3961 = _Time + _TimeEditor;
                float2 node_3960 = i.uv0;
                float2 node_1412 = (node_3960.rg+node_3961.g*float2(0,0.08));
                float3 node_1400 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_1412, _Normal)));
                float3 normalLocal = node_1400.rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float3 emissive = (saturate(( _IllumValue > 0.5 ? (1.0-(1.0-2.0*(_IllumValue-0.5))*(1.0-(((1.0 - pow(node_1400.r,100.0))*pow(tex2D(_IllumAlphaMap,TRANSFORM_TEX((i.uv0.rg+node_3961.g*float2(0.03,0.2)), _IllumAlphaMap)).r,3.0))*6.0))) : (2.0*_IllumValue*(((1.0 - pow(node_1400.r,100.0))*pow(tex2D(_IllumAlphaMap,TRANSFORM_TEX((i.uv0.rg+node_3961.g*float2(0.03,0.2)), _IllumAlphaMap)).r,3.0))*6.0)) ))*_IllumColor.rgb);
///////// Gloss:
                float gloss = _GlossValue;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_12 = (node_3960.rg+node_3961.g*float2(0.025,0.05));
                float3 node_9 = lerp(float3(i.uv0.rg,0.0),tex2D(_DistortAlphaMapoffset,TRANSFORM_TEX(node_12, _DistortAlphaMapoffset)).rgb,_Distortion);
                float4 node_2 = tex2D(_DistortDiffuseRGB,TRANSFORM_TEX(node_9, _DistortDiffuseRGB));
                float3 specularColor = float3(node_2.r,node_2.r,node_2.r);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_DiffColor.rgb*node_2.rgb*_DiffValue);
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,(tex2D(_AlphaTexturevertcolmult,TRANSFORM_TEX(node_3960.rg, _AlphaTexturevertcolmult)).rgb*i.vertexColor.g*_AlphaAmount));
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
            uniform sampler2D _DistortDiffuseRGB; uniform float4 _DistortDiffuseRGB_ST;
            uniform sampler2D _DistortAlphaMapoffset; uniform float4 _DistortAlphaMapoffset_ST;
            uniform float _Distortion;
            uniform sampler2D _AlphaTexturevertcolmult; uniform float4 _AlphaTexturevertcolmult_ST;
            uniform float _AlphaAmount;
            uniform float4 _DiffColor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _IllumAlphaMap; uniform float4 _IllumAlphaMap_ST;
            uniform float4 _IllumColor;
            uniform float _IllumValue;
            uniform float _DiffValue;
            uniform float _GlossValue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_3963 = _Time + _TimeEditor;
                float2 node_3962 = i.uv0;
                float2 node_1412 = (node_3962.rg+node_3963.g*float2(0,0.08));
                float3 node_1400 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_1412, _Normal)));
                float3 normalLocal = node_1400.rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = _GlossValue;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_12 = (node_3962.rg+node_3963.g*float2(0.025,0.05));
                float3 node_9 = lerp(float3(i.uv0.rg,0.0),tex2D(_DistortAlphaMapoffset,TRANSFORM_TEX(node_12, _DistortAlphaMapoffset)).rgb,_Distortion);
                float4 node_2 = tex2D(_DistortDiffuseRGB,TRANSFORM_TEX(node_9, _DistortDiffuseRGB));
                float3 specularColor = float3(node_2.r,node_2.r,node_2.r);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_DiffColor.rgb*node_2.rgb*_DiffValue);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaTexturevertcolmult,TRANSFORM_TEX(node_3962.rg, _AlphaTexturevertcolmult)).rgb*i.vertexColor.g*_AlphaAmount),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
