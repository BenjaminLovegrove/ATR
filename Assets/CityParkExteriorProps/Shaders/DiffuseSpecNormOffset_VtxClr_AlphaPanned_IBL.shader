// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:33318,y:32593,varname:node_1,prsc:2|diff-42-OUT,spec-5157-OUT,gloss-423-OUT,normal-7076-OUT,amdfl-21-OUT,amspl-9981-OUT,alpha-5014-OUT,refract-5335-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32687,y:32318,ptovrint:False,ptlb:Diffuse(RGB),ptin:_DiffuseRGB,varname:node_8944,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1bf200f67c0a38949b6f8c46f50cafa5,ntxv:0,isnm:False|UVIN-5107-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:32597,y:33989,ptovrint:False,ptlb:Alpha Map (RGB) Panner,ptin:_AlphaMapRGBPanner,varname:node_9629,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2663-UVOUT;n:type:ShaderForge.SFN_Multiply,id:42,x:32953,y:32348,varname:node_42,prsc:2|A-43-RGB,B-2-RGB,C-4219-OUT;n:type:ShaderForge.SFN_Color,id:43,x:32687,y:32149,ptovrint:False,ptlb:Diffuse Color,ptin:_DiffuseColor,varname:node_3651,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:171,x:32518,y:34346,ptovrint:False,ptlb:Alpha Intensity,ptin:_AlphaIntensity,varname:node_505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:178,x:31946,y:34206,ptovrint:False,ptlb:Alpha Min Speed,ptin:_AlphaMinSpeed,varname:node_3525,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:179,x:31946,y:34299,ptovrint:False,ptlb:Alpha Max Speed,ptin:_AlphaMaxSpeed,varname:node_4240,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:180,x:32108,y:34237,varname:node_180,prsc:2|A-178-OUT,B-179-OUT,T-181-OUT;n:type:ShaderForge.SFN_Slider,id:181,x:31789,y:34398,ptovrint:False,ptlb:Alpha Offset Speed,ptin:_AlphaOffsetSpeed,varname:node_3585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.470086,max:1;n:type:ShaderForge.SFN_Time,id:183,x:31946,y:34059,varname:node_183,prsc:2;n:type:ShaderForge.SFN_Slider,id:4213,x:32541,y:32944,ptovrint:False,ptlb:Specular Intensity,ptin:_SpecularIntensity,varname:node_7569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4219,x:32541,y:32507,ptovrint:False,ptlb:Diffuse Intensity,ptin:_DiffuseIntensity,varname:node_2942,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:4249,x:31662,y:32164,ptovrint:False,ptlb:Diffuse Min Speed,ptin:_DiffuseMinSpeed,varname:node_1428,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4251,x:31662,y:32257,ptovrint:False,ptlb:Diffuse Max Speed,ptin:_DiffuseMaxSpeed,varname:node_3856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:4253,x:31828,y:32219,varname:node_4253,prsc:2|A-4249-OUT,B-4251-OUT,T-4255-OUT;n:type:ShaderForge.SFN_Slider,id:4255,x:31513,y:32351,ptovrint:False,ptlb:Diffuse Panning Speed,ptin:_DiffusePanningSpeed,varname:node_5526,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4917575,max:1;n:type:ShaderForge.SFN_Time,id:4257,x:31662,y:32017,varname:node_4257,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:4266,x:31707,y:32763,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_3479,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2dd3788f8589b40bf82a92d76ffc5091,ntxv:3,isnm:True|UVIN-5193-OUT;n:type:ShaderForge.SFN_VertexColor,id:4810,x:32597,y:34170,varname:node_4810,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:4970,x:32321,y:33862,varname:node_4970,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:5107,x:32537,y:32170,varname:node_5107,prsc:2|A-5110-UVOUT,B-5109-R,T-5111-OUT;n:type:ShaderForge.SFN_Tex2d,id:5109,x:32355,y:32178,ptovrint:False,ptlb:Diff Distortion Map (R),ptin:_DiffDistortionMapR,varname:node_1205,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d96dc913fb4167b4a9258348d23a1c92,ntxv:0,isnm:False|UVIN-5120-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5110,x:32088,y:31993,varname:node_5110,prsc:2,uv:0;n:type:ShaderForge.SFN_Slider,id:5111,x:32198,y:32374,ptovrint:False,ptlb:Diffuse Distortion Intensity,ptin:_DiffuseDistortionIntensity,varname:node_454,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.257511,max:1;n:type:ShaderForge.SFN_Panner,id:5120,x:32188,y:32178,varname:node_5120,prsc:2,spu:0,spv:0.1|UVIN-5110-UVOUT,DIST-5145-OUT;n:type:ShaderForge.SFN_Multiply,id:5145,x:32016,y:32198,varname:node_5145,prsc:2|A-4257-TSL,B-4253-OUT;n:type:ShaderForge.SFN_Tex2d,id:5179,x:31704,y:33047,ptovrint:False,ptlb:Normal 02,ptin:_Normal02,varname:node_8326,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True|UVIN-5194-OUT;n:type:ShaderForge.SFN_Vector3,id:5188,x:31707,y:32611,varname:node_5188,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Add,id:5193,x:31532,y:32791,varname:node_5193,prsc:2|A-5195-UVOUT,B-5196-OUT;n:type:ShaderForge.SFN_Add,id:5194,x:31532,y:33020,varname:node_5194,prsc:2|A-5202-UVOUT,B-5204-OUT;n:type:ShaderForge.SFN_TexCoord,id:5195,x:31342,y:32684,varname:node_5195,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5196,x:31342,y:32863,varname:node_5196,prsc:2|A-5197-OUT,B-5198-T;n:type:ShaderForge.SFN_Append,id:5197,x:31154,y:32729,varname:node_5197,prsc:2|A-5199-OUT,B-5200-OUT;n:type:ShaderForge.SFN_Time,id:5198,x:31154,y:32883,varname:node_5198,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5199,x:30979,y:32706,ptovrint:False,ptlb:Normal 01 X Speed,ptin:_Normal01XSpeed,varname:node_5057,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.02;n:type:ShaderForge.SFN_ValueProperty,id:5200,x:30979,y:32795,ptovrint:False,ptlb:Normal 01 Y Speed,ptin:_Normal01YSpeed,varname:node_8535,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_TexCoord,id:5202,x:31340,y:33043,varname:node_5202,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5204,x:31340,y:33221,varname:node_5204,prsc:2|A-5206-OUT,B-5208-T;n:type:ShaderForge.SFN_Append,id:5206,x:31152,y:33088,varname:node_5206,prsc:2|A-5210-OUT,B-5212-OUT;n:type:ShaderForge.SFN_Time,id:5208,x:31152,y:33242,varname:node_5208,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5210,x:30977,y:33065,ptovrint:False,ptlb:Normal 02 X Speed,ptin:_Normal02XSpeed,varname:node_3674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.02;n:type:ShaderForge.SFN_ValueProperty,id:5212,x:30977,y:33154,ptovrint:False,ptlb:Normal 02 Y Speed,ptin:_Normal02YSpeed,varname:node_9151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.05;n:type:ShaderForge.SFN_Slider,id:5325,x:31859,y:33032,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:node_9242,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:5331,x:32201,y:32636,varname:node_5331,prsc:2|A-5188-OUT,B-5364-OUT,T-5325-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5334,x:32201,y:32805,varname:node_5334,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5364-OUT;n:type:ShaderForge.SFN_Multiply,id:5335,x:32386,y:32805,varname:node_5335,prsc:2|A-5334-OUT,B-5346-OUT;n:type:ShaderForge.SFN_Multiply,id:5346,x:32201,y:32974,varname:node_5346,prsc:2|A-5325-OUT,B-5347-OUT;n:type:ShaderForge.SFN_Vector1,id:5347,x:32036,y:33151,varname:node_5347,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Add,id:5364,x:31914,y:32796,varname:node_5364,prsc:2|A-4266-RGB,B-5179-RGB;n:type:ShaderForge.SFN_Multiply,id:5669,x:32760,y:33989,varname:node_5669,prsc:2|A-8-R,B-4810-G;n:type:ShaderForge.SFN_Tex2d,id:2121,x:32698,y:32751,ptovrint:False,ptlb:Specular (G),ptin:_SpecularG,varname:node_2121,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:30e6d9574e2ea2a43972f3b69d00c69b,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:6405,x:32541,y:33068,ptovrint:False,ptlb:Gloss Intensity,ptin:_GlossIntensity,varname:_SpecularIntensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7994198,max:1;n:type:ShaderForge.SFN_Multiply,id:5157,x:33052,y:32631,varname:node_5157,prsc:2|A-4013-RGB,B-2121-RGB,C-42-OUT,D-4213-OUT,E-9537-OUT;n:type:ShaderForge.SFN_Color,id:4013,x:32698,y:32594,ptovrint:False,ptlb:Specular Color,ptin:_SpecularColor,varname:node_4013,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Panner,id:2663,x:32410,y:34107,varname:node_2663,prsc:2,spu:0.5,spv:1|UVIN-4970-UVOUT,DIST-9323-OUT;n:type:ShaderForge.SFN_Lerp,id:5014,x:32973,y:33989,varname:node_5014,prsc:2|A-4970-U,B-5669-OUT,T-171-OUT;n:type:ShaderForge.SFN_Multiply,id:9323,x:32220,y:34107,varname:node_9323,prsc:2|A-183-TSL,B-180-OUT;n:type:ShaderForge.SFN_Multiply,id:21,x:32903,y:33227,varname:node_21,prsc:2|A-9492-OUT,B-9798-RGB;n:type:ShaderForge.SFN_Multiply,id:9492,x:32724,y:33227,varname:node_9492,prsc:2|A-7409-OUT,B-9798-A;n:type:ShaderForge.SFN_ValueProperty,id:7409,x:32516,y:33227,ptovrint:False,ptlb:Diffuse IBL Intensity,ptin:_DiffuseIBLIntensity,varname:node_7409,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Cubemap,id:9798,x:32516,y:33339,ptovrint:False,ptlb:Diffuse IBL (Cubemap),ptin:_DiffuseIBLCubemap,varname:node_9798,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:c64de53cec965f741b5b29e3796d8e67,pvfc:0|DIR-5671-OUT;n:type:ShaderForge.SFN_NormalVector,id:5671,x:32324,y:33339,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:9981,x:32905,y:33522,varname:node_9981,prsc:2|A-952-OUT,B-7018-RGB;n:type:ShaderForge.SFN_Multiply,id:952,x:32726,y:33522,varname:node_952,prsc:2|A-3952-OUT,B-7018-A;n:type:ShaderForge.SFN_ValueProperty,id:3952,x:32518,y:33522,ptovrint:False,ptlb:Specular IBL Intensity,ptin:_SpecularIBLIntensity,varname:_node_7409_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Cubemap,id:7018,x:32518,y:33634,ptovrint:False,ptlb:Specular IBL (Cubemap),ptin:_SpecularIBLCubemap,varname:_node_9798_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:c64de53cec965f741b5b29e3796d8e67,pvfc:1|DIR-105-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:105,x:32321,y:33634,varname:node_105,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9537,x:33052,y:32778,ptovrint:False,ptlb:Specular Value Multiplier,ptin:_SpecularValueMultiplier,varname:node_9537,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Add,id:423,x:32975,y:32883,varname:node_423,prsc:2|A-2121-A,B-2-R,C-6405-OUT;n:type:ShaderForge.SFN_Normalize,id:7076,x:32365,y:32636,varname:node_7076,prsc:2|IN-5331-OUT;proporder:43-4219-2-5109-4249-4251-4255-5111-4013-9537-4213-6405-2121-8-171-178-179-181-5325-4266-5199-5200-5179-5210-5212-7409-9798-3952-7018;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpecNormOffset_AlphaPanner_IBL" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
        _DiffuseIntensity ("Diffuse Intensity", Range(0, 1)) = 1
        _DiffuseRGB ("Diffuse(RGB)", 2D) = "white" {}
        _DiffDistortionMapR ("Diff Distortion Map (R)", 2D) = "white" {}
        _DiffuseMinSpeed ("Diffuse Min Speed", Float ) = 0
        _DiffuseMaxSpeed ("Diffuse Max Speed", Float ) = 20
        _DiffusePanningSpeed ("Diffuse Panning Speed", Range(0, 1)) = 0.4917575
        _DiffuseDistortionIntensity ("Diffuse Distortion Intensity", Range(0, 1)) = 0.257511
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _SpecularValueMultiplier ("Specular Value Multiplier", Float ) = 2
        _SpecularIntensity ("Specular Intensity", Range(0, 1)) = 1
        _GlossIntensity ("Gloss Intensity", Range(0, 1)) = 0.7994198
        _SpecularG ("Specular (G)", 2D) = "black" {}
        _AlphaMapRGBPanner ("Alpha Map (RGB) Panner", 2D) = "white" {}
        _AlphaIntensity ("Alpha Intensity", Range(0, 1)) = 1
        _AlphaMinSpeed ("Alpha Min Speed", Float ) = 0
        _AlphaMaxSpeed ("Alpha Max Speed", Float ) = 20
        _AlphaOffsetSpeed ("Alpha Offset Speed", Range(0, 1)) = 0.470086
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 1
        _Normal ("Normal", 2D) = "bump" {}
        _Normal01XSpeed ("Normal 01 X Speed", Float ) = 0.02
        _Normal01YSpeed ("Normal 01 Y Speed", Float ) = 0.05
        _Normal02 ("Normal 02", 2D) = "bump" {}
        _Normal02XSpeed ("Normal 02 X Speed", Float ) = -0.02
        _Normal02YSpeed ("Normal 02 Y Speed", Float ) = -0.05
        _DiffuseIBLIntensity ("Diffuse IBL Intensity", Float ) = 5
        _DiffuseIBLCubemap ("Diffuse IBL (Cubemap)", Cube) = "_Skybox" {}
        _SpecularIBLIntensity ("Specular IBL Intensity", Float ) = 10
        _SpecularIBLCubemap ("Specular IBL (Cubemap)", Cube) = "_Skybox" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
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
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGB; uniform float4 _DiffuseRGB_ST;
            uniform sampler2D _AlphaMapRGBPanner; uniform float4 _AlphaMapRGBPanner_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _AlphaMinSpeed;
            uniform float _AlphaMaxSpeed;
            uniform float _AlphaOffsetSpeed;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DiffDistortionMapR; uniform float4 _DiffDistortionMapR_ST;
            uniform float _DiffuseDistortionIntensity;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _RefractionIntensity;
            uniform sampler2D _SpecularG; uniform float4 _SpecularG_ST;
            uniform float _GlossIntensity;
            uniform float4 _SpecularColor;
            uniform float _DiffuseIBLIntensity;
            uniform samplerCUBE _DiffuseIBLCubemap;
            uniform float _SpecularIBLIntensity;
            uniform samplerCUBE _SpecularIBLCubemap;
            uniform float _SpecularValueMultiplier;
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
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_5198 = _Time + _TimeEditor;
                float2 node_5193 = (i.uv0+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_5198.g));
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_5193, _Normal)));
                float4 node_5208 = _Time + _TimeEditor;
                float2 node_5194 = (i.uv0+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_5208.g));
                float3 _Normal02_var = UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_5194, _Normal02)));
                float3 node_5364 = (_Normal_var.rgb+_Normal02_var.rgb);
                float3 normalLocal = normalize(lerp(float3(0,0,1),node_5364,_RefractionIntensity));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_5364.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _SpecularG_var = tex2D(_SpecularG,TRANSFORM_TEX(i.uv0, _SpecularG));
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_5120 = (i.uv0+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0,0.1));
                float4 _DiffDistortionMapR_var = tex2D(_DiffDistortionMapR,TRANSFORM_TEX(node_5120, _DiffDistortionMapR));
                float2 node_5107 = lerp(i.uv0,float2(_DiffDistortionMapR_var.r,_DiffDistortionMapR_var.r),_DiffuseDistortionIntensity);
                float4 _DiffuseRGB_var = tex2D(_DiffuseRGB,TRANSFORM_TEX(node_5107, _DiffuseRGB));
                float gloss = (_SpecularG_var.a+_DiffuseRGB_var.r+_GlossIntensity);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _SpecularIBLCubemap_var = texCUBE(_SpecularIBLCubemap,viewReflectDirection);
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 node_42 = (_DiffuseColor.rgb*_DiffuseRGB_var.rgb*_DiffuseIntensity);
                float3 specularColor = (_SpecularColor.rgb*_SpecularG_var.rgb*node_42*_SpecularIntensity*_SpecularValueMultiplier);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (0 + ((_SpecularIBLIntensity*_SpecularIBLCubemap_var.a)*_SpecularIBLCubemap_var.rgb));
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _DiffuseIBLCubemap_var = texCUBE(_DiffuseIBLCubemap,normalDirection);
                indirectDiffuse += ((_DiffuseIBLIntensity*_DiffuseIBLCubemap_var.a)*_DiffuseIBLCubemap_var.rgb); // Diffuse Ambient Light
                float3 diffuseColor = node_42;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float4 node_183 = _Time + _TimeEditor;
                float2 node_2663 = (i.uv0+(node_183.r*lerp(_AlphaMinSpeed,_AlphaMaxSpeed,_AlphaOffsetSpeed))*float2(0.5,1));
                float4 _AlphaMapRGBPanner_var = tex2D(_AlphaMapRGBPanner,TRANSFORM_TEX(node_2663, _AlphaMapRGBPanner));
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,lerp(i.uv0.r,(_AlphaMapRGBPanner_var.r*i.vertexColor.g),_AlphaIntensity)),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGB; uniform float4 _DiffuseRGB_ST;
            uniform sampler2D _AlphaMapRGBPanner; uniform float4 _AlphaMapRGBPanner_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _AlphaMinSpeed;
            uniform float _AlphaMaxSpeed;
            uniform float _AlphaOffsetSpeed;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DiffDistortionMapR; uniform float4 _DiffDistortionMapR_ST;
            uniform float _DiffuseDistortionIntensity;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _RefractionIntensity;
            uniform sampler2D _SpecularG; uniform float4 _SpecularG_ST;
            uniform float _GlossIntensity;
            uniform float4 _SpecularColor;
            uniform float _SpecularValueMultiplier;
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
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_5198 = _Time + _TimeEditor;
                float2 node_5193 = (i.uv0+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_5198.g));
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_5193, _Normal)));
                float4 node_5208 = _Time + _TimeEditor;
                float2 node_5194 = (i.uv0+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_5208.g));
                float3 _Normal02_var = UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_5194, _Normal02)));
                float3 node_5364 = (_Normal_var.rgb+_Normal02_var.rgb);
                float3 normalLocal = normalize(lerp(float3(0,0,1),node_5364,_RefractionIntensity));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_5364.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _SpecularG_var = tex2D(_SpecularG,TRANSFORM_TEX(i.uv0, _SpecularG));
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_5120 = (i.uv0+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0,0.1));
                float4 _DiffDistortionMapR_var = tex2D(_DiffDistortionMapR,TRANSFORM_TEX(node_5120, _DiffDistortionMapR));
                float2 node_5107 = lerp(i.uv0,float2(_DiffDistortionMapR_var.r,_DiffDistortionMapR_var.r),_DiffuseDistortionIntensity);
                float4 _DiffuseRGB_var = tex2D(_DiffuseRGB,TRANSFORM_TEX(node_5107, _DiffuseRGB));
                float gloss = (_SpecularG_var.a+_DiffuseRGB_var.r+_GlossIntensity);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 node_42 = (_DiffuseColor.rgb*_DiffuseRGB_var.rgb*_DiffuseIntensity);
                float3 specularColor = (_SpecularColor.rgb*_SpecularG_var.rgb*node_42*_SpecularIntensity*_SpecularValueMultiplier);
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuseColor = node_42;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float4 node_183 = _Time + _TimeEditor;
                float2 node_2663 = (i.uv0+(node_183.r*lerp(_AlphaMinSpeed,_AlphaMaxSpeed,_AlphaOffsetSpeed))*float2(0.5,1));
                float4 _AlphaMapRGBPanner_var = tex2D(_AlphaMapRGBPanner,TRANSFORM_TEX(node_2663, _AlphaMapRGBPanner));
                fixed4 finalRGBA = fixed4(finalColor * lerp(i.uv0.r,(_AlphaMapRGBPanner_var.r*i.vertexColor.g),_AlphaIntensity),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
