﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class _src_Emails_FluentEmailHelp_cshtml : RazorGenerator.Templating.RazorTemplateBase
    {
#line hidden
        #line 2 "..\..\src\Emails\FluentEmailHelp.cshtml"
            
    public dynamic Number { get; set; }

        #line default
        #line hidden
        
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n<p> Hello user #");

            
            #line 6 "..\..\src\Emails\FluentEmailHelp.cshtml"
           Write(Number);

            
            #line default
            #line hidden
WriteLiteral("</p>");

        }
    }
}
#pragma warning restore 1591
