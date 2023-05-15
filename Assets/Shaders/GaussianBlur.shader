Shader "Hidden/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Size", Float) = 0.0
        _Sigma ("St. dev. (sigma)", Float) = 5.0
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

            // Define the constants used in Gaussian calculation.
            static const float TWO_PI = 6.28319;
            static const float E = 2.71828;

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
            half4 _MainTex_TexelSize;
            float _Size;
            float _Sigma;

            float gaussian(int x, int y)
            {
                float sigmaSqu = _Sigma * _Sigma;
                return (1 / sqrt(TWO_PI * sigmaSqu)) * pow(E, -((x * x) + (y * y)) / (2 * sigmaSqu));
            }

            // We will keep _Size constant. We set it to 20 in the material
            fixed4 frag(v2f i) : SV_Target
            {

                float4 col = float4(0.0, 0.0, 0.0, 0.0);
                float kernelSum = 0.0;

                int upper = ((_Size - 1) / 2);
                int lower = -upper;

                for (int x = lower; x <= upper; ++x)
                {
                    for (int y = lower; y <= upper; ++y)
                    {
                        float gauss = gaussian(x, y);
                        kernelSum += gauss;

                        fixed2 offset = fixed2(_MainTex_TexelSize.x * x, _MainTex_TexelSize.y * y);
                        col += gauss * tex2D(_MainTex, i.uv + offset);
                    }
                }

                col /= kernelSum;
                return col;

            }
            ENDCG
        }
    }
}
