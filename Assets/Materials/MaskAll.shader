Shader "Custom/Mask"
{
	Properties{}

	SubShader{

		Tags {
			"RenderType" = "Opaque"
		}
        CULL FRONT

		Pass {
			ZWrite Off
		}
	}
}