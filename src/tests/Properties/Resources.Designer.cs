﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alphacloud.MSBuild.Xslt.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Alphacloud.MSBuild.Xslt.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;DuplicatesReport ToolsVersion=&quot;104.0.20151218.122648&quot;&gt;
        ///  &lt;Statistics&gt;
        ///    &lt;CodebaseCost&gt;38563&lt;/CodebaseCost&gt;
        ///    &lt;TotalDuplicatesCost&gt;223&lt;/TotalDuplicatesCost&gt;
        ///    &lt;TotalFragmentsCost&gt;446&lt;/TotalFragmentsCost&gt;
        ///  &lt;/Statistics&gt;
        ///  &lt;Duplicates&gt;
        ///    &lt;Duplicate Cost=&quot;81&quot;&gt;
        ///      &lt;Fragment&gt;
        ///        &lt;FileName&gt;SharpArch.Web.Mvc.Castle\WindsorPropertyInjectionExtensions.cs&lt;/FileName&gt;
        ///        &lt;OffsetRange Start=&quot;1657&quot; End=&quot;1886&quot;&gt;&lt;/OffsetRange&gt;
        ///        &lt;LineRange Start= [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string XmlDupReport {
            get {
                return ResourceManager.GetString("XmlDupReport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;root&gt;
        ///  &lt;settings&gt;
        ///    &lt;add name=&quot;name1&quot; value=&quot;value1&quot; /&gt;
        ///  &lt;/settings&gt;
        ///&lt;/root&gt;
        ///.
        /// </summary>
        internal static string XmlSource {
            get {
                return ResourceManager.GetString("XmlSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;!-- origin: https://gist.github.com/st-gwerner/6675196 --&gt;
        ///&lt;xsl:stylesheet xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot; version=&quot;1.0&quot;&gt;
        ///	&lt;xsl:output method=&quot;html&quot; indent=&quot;yes&quot; /&gt;
        ///  &lt;xsl:preserve-space elements=&quot;Text&quot; /&gt;
        ///	&lt;xsl:template match=&quot;/&quot;&gt;
        ///		&lt;html&gt;
        ///			&lt;head&gt;
        ///				&lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1.0&quot; /&gt;
        ///				&lt;link rel=&quot;stylesheet&quot; href=&quot;https://netdna.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css&quot; /&gt;
        ///				&lt;link  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string XsltDupFinder {
            get {
                return ResourceManager.GetString("XsltDupFinder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;xsl:stylesheet version=&quot;1.0&quot; xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;
        ///    xmlns:msxsl=&quot;urn:schemas-microsoft-com:xslt&quot; exclude-result-prefixes=&quot;msxsl&quot;
        ///&gt;
        ///    &lt;xsl:output method=&quot;xml&quot; indent=&quot;yes&quot;/&gt;
        ///
        ///    &lt;xsl:template match=&quot;/&quot;&gt;
        ///      &lt;configuration&gt;
        ///        &lt;settings&gt;
        ///          &lt;xsl:for-each select=&quot;//root/settings/add&quot;&gt;
        ///            &lt;xsl:element name=&quot;pair&quot;&gt;
        ///              &lt;xsl:element name=&quot;key&quot;&gt;
        ///                &lt;xsl:value-of select=&quot;@name&quot;/&gt;
        ///      [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string XsltTransform1 {
            get {
                return ResourceManager.GetString("XsltTransform1", resourceCulture);
            }
        }
    }
}
