#pragma checksum "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\Account\ForgotPasswordConfirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccdf6035e5ad9973b2f67531cd286ffcdd72ba26"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ForgotPasswordConfirmation), @"mvc.1.0.view", @"/Views/Account/ForgotPasswordConfirmation.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using SSLDC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using WebApp.Helper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccdf6035e5ad9973b2f67531cd286ffcdd72ba26", @"/Views/Account/ForgotPasswordConfirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7adbb6854ae54089000fcb9f4663209a7360a89", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ForgotPasswordConfirmation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\Account\ForgotPasswordConfirmation.cshtml"
  
    ViewData["Title"] = localizer["Reset Password"];

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n<br />\r\n<h3 class=\"alert alert-info text-center\">");
#nullable restore
#line 7 "D:\Sites\Web_Template_15-1-2023\WebApp\WebApp\Views\Account\ForgotPasswordConfirmation.cshtml"
                                    Write(localizer["Check your email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
