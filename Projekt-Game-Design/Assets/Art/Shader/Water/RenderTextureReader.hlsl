//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

SamplerState MeshTextureSampler
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

void DepthFromRenderTexture_float(Texture2D _texture, float2 uv, out float3 Out) {
    float3 color = _texture.Sample(MeshTextureSampler, uv);
    Out = color;
}
#endif //MYHLSLINCLUDE_INCLUDED
