#pragma checksum "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\previewSystem.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4ce6264b1304e9a6bb4fc6107b308beae516317e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EditUserApplication_previewSystem), @"mvc.1.0.view", @"/Views/EditUserApplication/previewSystem.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EditUserApplication/previewSystem.cshtml", typeof(AspNetCore.Views_EditUserApplication_previewSystem))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4ce6264b1304e9a6bb4fc6107b308beae516317e", @"/Views/EditUserApplication/previewSystem.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95f22006c358dc1d02b385bda3cfb4420360c0b0", @"/Views/_ViewImports.cshtml")]
    public class Views_EditUserApplication_previewSystem : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FYP1.ViewModel.formsApplicationDatabase>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("40"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(" logo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(48, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\previewSystem.cshtml"
  
    ViewData["Title"] = "index";


#line default
#line hidden
            BeginContext(93, 186, true);
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <nav class=\"navbar navbar-dark  row\" style=\"background-color:black\">\r\n        <div class=\"col-sm-3\">\r\n            <a class=\"navbar-brand\" href=\"#\">\r\n");
            EndContext();
            BeginContext(343, 16, true);
            WriteLiteral("                ");
            EndContext();
            BeginContext(359, 94, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4ce6264b1304e9a6bb4fc6107b308beae516317e4928", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 369, "~/logo/", 369, 7, true);
#line 13 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\previewSystem.cshtml"
AddHtmlAttributeValue("", 376, Model.applicationDatabase.userApplications.appLogo, 376, 51, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(453, 121, true);
            WriteLiteral("\r\n            </a>\r\n        </div>\r\n\r\n        <div class=\"col-sm-8\">\r\n            <h2 class=\"text-capitalize text-white\">");
            EndContext();
            BeginContext(575, 50, false);
#line 18 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\previewSystem.cshtml"
                                              Write(Model.applicationDatabase.userApplications.appName);

#line default
#line hidden
            EndContext();
            BeginContext(625, 359, true);
            WriteLiteral(@"</h2>
        </div>
    </nav>

    <div class=""row"">
        <div class=""col-sm-3 p-0"">
            <div id=""sidebar"" class="" p-2 elegant-color-dark"">

                <!-- sidebar menu start-->
                <h6 class=""font-weight-bold text-white mt-5""><i class=""fa fa-tasks""></i> View or delete forms</h6>
                <div id=""formList"">
");
            EndContext();
#line 29 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditUserApplication\previewSystem.cshtml"
                      
                        Html.RenderPartial("_FormList", Model.forms);
                    

#line default
#line hidden
            BeginContext(1102, 2021, true);
            WriteLiteral(@"                </div>
            </div>
        </div>
        <div class=""col-sm-6  mt-5 align-content-center"" id=""previewForm"">
            <h3 id=""formHeading"" class=""heading""></h3>
            <div id=""formView"">

            </div>
            <button type=""button"" style=""visibility:hidden""  onclick=""deletForm(this.id)"" id=""idDeleteForm"" >Delete Form</button>
        </div>

    </div>


</div>

<script>
  
    function getFormView(idArg) {
    var tableName=$(""#table""+idArg).val();

      //  console.log(""tableName "" +tableName);
       // console.log($(""#getFormTitle"").text());
        $.ajax({
            url: '/EditUserApplication/getView',
            type: 'POST',
            data: {
                getFormId : idArg,
                tableName:tableName
            },
            success: function (data) {
                $(""#formView"").html(data[0]);
                    $(""#formHeading"").html(data[1]);
                    $(""#tabName"").html(data[2]);
           ");
            WriteLiteral(@"          document.getElementById('idDeleteForm').style.visibility = 'visible';
                    document.getElementById('idDeleteForm').id= idArg;
                   
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    function deletForm(id){
        $.ajax({
            url: '/EditUserApplication/deleteForm',
            type: 'POST',
            data: {
                FormId : id
        },
            success: function (data) {
                $(""#formView"").html('');
                $(""#formHeading"").html('');
                $(""#tabName"").html('');
                $(""#formList"").html(data);
               document.getElementById(id).style.visibility = 'hidden';
               document.getElementById(id).id= 'idDeleteForm';
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

</script>













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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FYP1.ViewModel.formsApplicationDatabase> Html { get; private set; }
    }
}
#pragma warning restore 1591
