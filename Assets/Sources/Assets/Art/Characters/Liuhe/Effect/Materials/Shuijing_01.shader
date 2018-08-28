// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:5,bsrc:3,bdst:0,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33945,y:32712,varname:node_1,prsc:2|emission-3118-OUT,alpha-6-A;n:type:ShaderForge.SFN_Fresnel,id:2,x:32930,y:32648,varname:node_2,prsc:2|EXP-3-OUT;n:type:ShaderForge.SFN_Vector1,id:3,x:32701,y:32666,varname:node_3,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Vector3,id:4,x:32754,y:32843,varname:node_4,prsc:2,v1:0.3,v2:0.5,v3:1;n:type:ShaderForge.SFN_Multiply,id:5,x:33109,y:32756,varname:node_5,prsc:2|A-2-OUT,B-4-OUT;n:type:ShaderForge.SFN_VertexColor,id:6,x:33745,y:33183,varname:node_6,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:1239,x:32715,y:33090,ptovrint:False,ptlb:node_1239,ptin:_node_1239,varname:node_1239,prsc:2,tex:3639b9047fcd35143afca609547fe67d,ntxv:0,isnm:False|UVIN-6403-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:569,x:33127,y:33090,ptovrint:False,ptlb:node_569,ptin:_node_569,varname:node_569,prsc:2,tex:dd1094c90e52274459b5b5e2ef56d8f2,ntxv:0,isnm:False|UVIN-637-UVOUT;n:type:ShaderForge.SFN_Panner,id:6403,x:32497,y:33090,varname:node_6403,prsc:2,spu:0,spv:-0.25;n:type:ShaderForge.SFN_Panner,id:637,x:32924,y:33090,varname:node_637,prsc:2,spu:0,spv:-0.15|DIST-1239-R;n:type:ShaderForge.SFN_Add,id:3118,x:33595,y:32698,varname:node_3118,prsc:2|A-5-OUT,B-3210-OUT;n:type:ShaderForge.SFN_Vector3,id:7346,x:33454,y:33222,varname:node_7346,prsc:2,v1:0.6,v2:0.7,v3:1;n:type:ShaderForge.SFN_Multiply,id:3210,x:33489,y:32987,varname:node_3210,prsc:2|A-569-RGB,B-7346-OUT;proporder:1239-569;pass:END;sub:END;*/

Shader "Shader Forge/LeishenSkill3" {
    Properties {
        _node_1239 ("node_1239", 2D) = "white" {}
        _node_569 ("node_569", 2D) = "white" {}
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
            Blend SrcAlpha One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _node_1239; uniform float4 _node_1239_ST;
            uniform sampler2D _node_569; uniform float4 _node_569_ST;
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
                float4 screenPos : TEXCOORD3;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(4)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD5;
                #else
                    float3 shLight : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 node_5 = (pow(1.0-max(0,dot(normalDirection, viewDirection)),0.7)*float3(0.3,0.5,1));
                float4 node_3936 = _Time + _TimeEditor;
                float2 node_6403 = (i.uv0+node_3936.g*float2(0,-0.25));
                float4 _node_1239_var = tex2D(_node_1239,TRANSFORM_TEX(node_6403, _node_1239));
                float2 node_637 = (i.uv0+_node_1239_var.r*float2(0,-0.15));
                float4 _node_569_var = tex2D(_node_569,TRANSFORM_TEX(node_637, _node_569));
                float3 emissive = (node_5+(_node_569_var.rgb*float3(0.6,0.7,1)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,i.vertexColor.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
