# MSBuild.Xslt
XSLT 3 transformation task for MSBuild

This package provides SAXON-based XSLT 3 transformation tasks for MSBuild.
This is **tools** package, i.e. no assembly references are added to .NET projects.

Project is based on [Saxon-HE](http://nuget.org/List/Packages/Saxon-HE).

[![Master branch build](https://ci.appveyor.com/api/projects/status/github/alphacloud/MSBuild.Xslt?branch=master&svg=true)](https://ci.appveyor.com/project/shatl/msbuild-xslt)

## Installation 

Install nuget package as usual.


## Usage

1. Import targets file 
```
<Import Project="$(MSBuildProjectDirectory)\Packages\Alphacloud.MSBuild.Xslt.0.1.0.0\tools\Alphacloud.MSBuild.Xslt.targets" />
```
2. Execute task
```
  <ItemGroup>
    <XsltTemplate Include="..\..\TestResources\Transform1.xslt">
      <Version>0.1</Version>
    </XsltTemplate>
  </ItemGroup>

<Xslt3SingleFileTransform
  Xslt="@(XsltTemplate)" Input="..\..\TestResources\Source.xml"
  Output="result1.xml">
```

Task parameters:
* Input - Source XML file;
* Xslt - XSL template;
* OmitXmlDeclatation - omits `<?xml ?>` declaration from output file. Not required if output is configured with `<xsl:output />` instruction.
* Output - Generated file name.


### Passing parameters into XSL transformation

Parameters can be passed as [item metadata](https://msdn.microsoft.com/en-us/library/ms171453.aspx) with `Input` and `Xslt` items.
Parameters passed within `Input` item will **override** `Xslt` parameters with the same names. This is usefull if same XSLT is used to transform many files and some parameters 
are specific per input XML file.


## Visual Studio intellisense support

Assuming you are running Visual Studio 2015 x64 Windows. Backup and replace `C:\Program Files (x86)\Microsoft Visual Studio 14.0\Xml\Schemas\xslt.xsd` file with `tools\xslt.xsd` from the package.

Please refer to the excellent post from [Steve Evangelista](http://appdevonsharepoint.com/adding-xslt-2-0-intellisense-to-visual-studio/) for detailed information how to enable XSLT intellisense.

---
*Stay tuned.*
