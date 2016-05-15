Shader "5v1/SegmentedBarShader"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        _NumSegments("Number of Segments", int) = 1
        _SegmentSpacing("Segment Spacing", float) = 0.2
        _Fill("Fill", float) = 1
        _Vertical("Vertical", int) = 0
    }
    SubShader
    {
        Tags { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
        }
        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _NumSegments;
            float _SegmentSpacing;
            float _Fill;
            bool _Vertical;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {

                if (i.uv.y > _Fill) {
                    return 0;
                }

                float currentSegmenta = 1 / _NumSegments;
                float currentSegmentb = i.uv.y / currentSegmenta;
                float currentSegment = floor(currentSegmentb);

                if (i.uv.y % (1 / _NumSegments) > _SegmentSpacing - _SegmentSpacing/2) {
                    return 0;
                }

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.color; 


                return col;
            }
            ENDCG
        }
    }
}
 