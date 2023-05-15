Shader "Hidden/Pixelation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pixels ("Resolution", int) = 512
        _PixelWidth ("Pixels Width", float) = 64
        _PixelHeight ("PixelHeight", float) = 64
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Pixels;
            float _PixelWidth;
            float _PixelHeight;

            fixed4 frag(v2f i) : SV_Target
            {
                float dx = _PixelWidth * (1 / _Pixels);
                float dy = _PixelHeight * (1 / _Pixels);
                float2 coord = float2(dx * floor(i.uv.x / dx), dy * floor(i.uv.y / dy));
                fixed4 col = tex2D(_MainTex, coord);
               
                return col;
            }
            ENDCG
        }
    }
}
