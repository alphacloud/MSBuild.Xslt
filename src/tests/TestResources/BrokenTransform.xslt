<?xml version="1.0" encoding="utf-8"?>

<!-- origin: https://gist.github.com/st-gwerner/6675196 -->
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" version="5.0" indent="yes" />

  <xsl:key name="issue-lookup" select="//IssueTypes/IssueType" />
  
  <xsl:template match="/Report">
    <html>
      <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css" />
      </head>
      <body>
        <div class="container">
          <h1>Statistics</h1>
          <p>
            Total isnpections:
            <b><xsl:value-of select="count(//Issues//Issue)" /></b>
          </p>
          <h2>Inspections by projects</h2>
          <table class="table table-condensed">
            <thead>
              <tr>
                <th>Project</th>
                <th>Number of inspections</th>
              </tr>
            </thead>
            <tbody>
              <xsl:for-each-group select="//Issues/Project" group-by="@Name">
                <tr>
                  <td><xsl:value-of select="current-grouping-key()" /></td>
                  <td><xsl:value-of select="count(current-group()/Issue)" /></td>
                </tr>
              </xsl:for-each-group>

            </tbody>
          </table>

          <h1>Details by projects</h1>
          <xsl:for-each select="//Issues/Project">
            <xsl:variable name="projectName" select="@Name" />
            <h2><hr/><xsl:value-of select="$projectName" /></h2>

            <xsl:for-each-group select="//Issues/Project[@Name=$projectName]/Issue" group-by="@TypeId">
              <xsl:variable name="issueTypeId" select="@TypeId" />
              <h3>
                <xsl:value-of select="$issueTypeId" />
              </h3>
              <!-- todo: add issue description -->
              <table class="table table-striped table-condensed">
                <thead>
                  <tr>
                    <th>File name</th>
                    <th>Line number</th>
                    <th>Message</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="//Issues/Project[@Name=$projectName]/Issue[@TypeId=$issueTypeId]">
                    <tr>
                      <td><xsl:value-of select="replace(@File, $projectName, '.')" />
                        <!-- todo: remove project name -->
                      </td>
                      <td><xsl:value-of select="@Line" /></td>
                      <td><xsl:value-of select="@Message" /></td>
                    </tr>
                  </xsl:for-each>
                </tbody>
              </table>
            </xsl:for-each-group>

          </xsl:for-each>

        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.0/jquery.min.js" />
        <script src="https://netdna.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" />
      </body>
    </html>
  </xsl:template>
  <xsl:template  name="issueDetails">
    
  </xsl:template>

</xsl:stylesheet>