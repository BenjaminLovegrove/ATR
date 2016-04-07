// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30929,y:32686|diff-6370-OUT,spec-6422-OUT,gloss-6447-OUT,alpha-5669-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:31628,y:32392,ptlb:Diffuse(RGB) Spec(A),ptin:_DiffuseRGBSpecA,tex:59ac46d95fca52d499ba15e347a0ea0c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:42,x:31415,y:32398|A-43-RGB,B-2-RGB,C-4219-OUT;n:type:ShaderForge.SFN_Color,id:43,x:31628,y:32223,ptlb:Diffuse Color,ptin:_DiffuseColor,glob:False,c1:0.6797686,c2:0.8497227,c3:0.9338235,c4:1;n:type:ShaderForge.SFN_Slider,id:171,x:31550,y:33674,ptlb:Alpha Intensity,ptin:_AlphaIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:4212,x:31415,y:32684|A-2-A,B-4213-OUT;n:type:ShaderForge.SFN_Slider,id:4213,x:31622,y:32754,ptlb:Specular Intensity,ptin:_SpecularIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4219,x:31622,y:32646,ptlb:Diffuse Intensity,ptin:_DiffuseIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:5669,x:31287,y:33465|A-6342-R,B-171-OUT;n:type:ShaderForge.SFN_Tex2d,id:6342,x:31550,y:33455,ptlb:Alpha Mask (R),ptin:_AlphaMaskR,tex:01b622d4830754f4886f4b03606fb1ec,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6370,x:31203,y:32398|A-42-OUT,B-6371-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6371,x:31415,y:32610,ptlb:Diffuse Multiplier,ptin:_DiffuseMultiplier,cmnt:Make Diffuse more noticable,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6422,x:31223,y:32684|A-4212-OUT,B-6424-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6424,x:31415,y:32853,ptlb:Specular Multiplier,ptin:_SpecularMultiplier,cmnt:Make Specular more noticable,glob:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:6447,x:31223,y:32810,v1:2;proporder:43-6371-4219-6424-4213-2-6342-171;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpec_Transp_IBL" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (0.6797686,0.8497227,0.9338235,1)
        _DiffuseMultiplier ("Diffuse Multiplier", Float ) = 1
        _DiffuseIntensity ("Diffuse Intensity", Range(0, 1)) = 1
        _SpecularMultiplier ("Specular Multiplier", Float ) = 1
        _SpecularIntensity ("Specular Intensity", Range(0, 1)) = 1
        _DiffuseRGBSpecA ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
        _AlphaMaskR ("Alpha Mask (R)", 2D) = "white" {}
        _AlphaIntensity ("Alpha Intensity", Range(0, 1)) = 1
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
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            uniform float _DiffuseMultiplier;
            uniform float _SpecularMultiplier;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 2.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_6472 = i.uv0;
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_6472.rg, _DiffuseRGBSpecA));
                float node_6422 = ((node_2.a*_SpecularIntensity)*_SpecularMultiplier);
                float3 specularColor = float3(node_6422,node_6422,node_6422);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * ((_DiffuseColor.rgb*node_2.rgb*_DiffuseIntensity)*_DiffuseMultiplier);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor,(tex2D(_AlphaMaskR,TRANSFORM_TEX(node_6472.rg, _AlphaMaskR)).r*_AlphaIntensity));
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
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            uniform float _DiffuseMultiplier;
            uniform float _SpecularMultiplier;
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
                float gloss = 2.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float2 node_6473 = i.uv0;
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_6473.rg, _DiffuseRGBSpecA));
                float node_6422 = ((node_2.a*_SpecularIntensity)*_SpecularMultiplier);
                float3 specularColor = float3(node_6422,node_6422,node_6422);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * ((_DiffuseColor.rgb*node_2.rgb*_DiffuseIntensity)*_DiffuseMultiplier);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaMaskR,TRANSFORM_TEX(node_6473.rg, _AlphaMaskR)).r*_AlphaIntensity),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
