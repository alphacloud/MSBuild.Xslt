<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

  <xsl:param name="Version" />
  
    <xsl:template match="/">
      <xsl:element name="configuration">
        <xsl:attribute name="version">
          <xsl:value-of select="$Version"/>
        </xsl:attribute>
        <settings>
          <xsl:for-each select="//root/settings/add">
            <xsl:element name="pair">
              <xsl:element name="key">
                <xsl:value-of select="@name"/>
              </xsl:element>
              <xsl:element name="value">
                <xsl:value-of select="@value"/>
              </xsl:element>
            </xsl:element>
          </xsl:for-each>
        </settings>
      </xsl:element>
    </xsl:template>
  
</xsl:stylesheet>
