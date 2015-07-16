<?xml version="1.0" encoding="utf-8"?>
<Deployment xmlns="http://schemas.microsoft.com/windowsphone/2009/deployment" AppPlatformVersion="7.0">
  <App xmlns="" ProductID="{5d8b66d1-80f8-4ab6-b848-87149b9c2805}" Title="Astro Flare" RuntimeType="XNA" Version="1.0.0.0" Genre="Apps.Games" Author="" Description="" Publisher="">
    <IconPath IsRelative="true" IsResource="false">Background.png</IconPath>
    <Capabilities>
      <Capability Name="ID_CAP_NETWORKING" />
      <Capability Name="ID_CAP_SENSORS" />
      <Capability Name="ID_CAP_MEDIALIB" />
      <Capability Name="ID_CAP_IDENTITY_DEVICE" />
    </Capabilities>
    <Tasks>
      <DefaultTask Name="_default" />
    </Tasks>
    <Tokens>
      <PrimaryToken TokenID="AstroFlareToken" TaskName="_default">
        <TemplateType5>
          <BackgroundImageURI IsRelative="true" IsResource="false">Background.png</BackgroundImageURI>
          <Count>0</Count>
          <Title>Astro Flare</Title>
        </TemplateType5>
      </PrimaryToken>
    </Tokens>
  </App>
</Deployment>