# MSBuild.Xslt
XSLT 3 transformation task for MSBuild

This package provides SAXON-based XSLT 3 transformation tasks for MSBuild.
This is **tools** package, i.e. no assembly references are added to .NET projects.

Project is based on [Saxon-HE](http://nuget.org/List/Packages/Saxon-HE) .


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

Stay tuned.
