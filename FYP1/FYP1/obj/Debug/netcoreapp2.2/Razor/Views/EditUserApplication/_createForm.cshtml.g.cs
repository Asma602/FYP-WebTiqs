#pragma checksum "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "38606592f78728170308517636ead46b45c319fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EditUserApplication__createForm), @"mvc.1.0.view", @"/Views/EditUserApplication/_createForm.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EditUserApplication/_createForm.cshtml", typeof(AspNetCore.Views_EditUserApplication__createForm))]
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
#line 1 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\_ViewImports.cshtml"
using FYP1;

#line default
#line hidden
#line 2 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\_ViewImports.cshtml"
using FYP1.Models;

#line default
#line hidden
#line 4 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38606592f78728170308517636ead46b45c319fc", @"/Views/EditUserApplication/_createForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95f22006c358dc1d02b385bda3cfb4420360c0b0", @"/Views/_ViewImports.cshtml")]
    public class Views_EditUserApplication__createForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FYP1.Models.Column>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(108, 249, true);
            WriteLiteral("\r\n\r\n<section class=\" mt-4 \">\r\n\r\n    <div class=\"row mt-4\">\r\n\r\n        <div class=\"col-sm-7\">\r\n            <input type=\"text\" id=\"SetFormTitle\" placeholder=\"Write form title here (e.g Add User)\" class=\"form-control col-sm-12\" />\r\n            <br />\r\n");
            EndContext();
#line 13 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(414, 134, true);
            WriteLiteral("             <div class=\"attrsContainer\">\r\n                <label class=\"container ml-1 \">\r\n                    <input type=\"checkbox\"");
            EndContext();
            BeginWriteAttribute("dataType", " dataType=", 548, "", 572, 1);
#line 17 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 558, item.dataType, 558, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("minlen", " \r\n                           minlen=\"", 572, "\"", 625, 1);
#line 18 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 610, item.minLength, 610, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("maxlen", " maxlen=\"", 626, "\"", 650, 1);
#line 18 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 635, item.maxLength, 635, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("req", "\r\n                           req=", 651, "", 698, 1);
#line 19 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 684, item.required, 684, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("desc", " desc=", 698, "", 714, 1);
#line 19 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 704, item.name, 704, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=", 714, "", 728, 1);
#line 19 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
WriteAttributeValue("", 718, item.name, 718, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(728, 54, true);
            WriteLiteral(">\r\n                    <span style=\"font-size: 18px;\">");
            EndContext();
            BeginContext(783, 9, false);
#line 20 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"
                                              Write(item.name);

#line default
#line hidden
            EndContext();
            BeginContext(792, 109, true);
            WriteLiteral("</span>\r\n                    <span class=\"checkmark\"></span>\r\n                </label>\r\n             </div>\r\n");
            EndContext();
#line 24 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\_createForm.cshtml"



            }

#line default
#line hidden
            BeginContext(922, 339, true);
            WriteLiteral(@"            <br />
            <div id=""invalidFormName"" class=""text-danger"" style=""font-size:16px;""></div>
            <button onclick=""getForm();"" id=""createBtn"" data-toggle=""modal"" data-target=""#myModal"" style=""font-size:16px;"" class=""btn bgColor text-capitalize"">Create Form</button>

        </div>

    </div>


</section>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FYP1.Models.Column>> Html { get; private set; }
    }
}
#pragma warning restore 1591
