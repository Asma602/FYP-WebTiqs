#pragma checksum "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "517cc12510acc7f8dc62236d3e34f513fa79bb91"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EditDatabase__PartialMakeRelation), @"mvc.1.0.view", @"/Views/EditDatabase/_PartialMakeRelation.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EditDatabase/_PartialMakeRelation.cshtml", typeof(AspNetCore.Views_EditDatabase__PartialMakeRelation))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"517cc12510acc7f8dc62236d3e34f513fa79bb91", @"/Views/EditDatabase/_PartialMakeRelation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95f22006c358dc1d02b385bda3cfb4420360c0b0", @"/Views/_ViewImports.cshtml")]
    public class Views_EditDatabase__PartialMakeRelation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FYP1.ViewModel.tableRelation>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/partialMakeRelation.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "one-to-many", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "one-to-one", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(37, 10, true);
            WriteLiteral("    \r\n    ");
            EndContext();
            BeginContext(47, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "517cc12510acc7f8dc62236d3e34f513fa79bb915425", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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
            BeginContext(109, 395, true);
            WriteLiteral(@"

<section class=""columnsDisplay pt-4"" id=""disPlayColumns"">
    <h4 class=""font-weight-bolder"">Map Relations</h4>
    <h6 class=""font-weight-bolder"">Choose tables to map relations</h6>

    <div id=""mapRelation"">
        <div class=""form-row"">
            <div class=""col"" id=""firstChild"">
                <select id=""pKey"" class=""browser-default custom-select"">

                    ");
            EndContext();
            BeginContext(504, 42, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb917091", async() => {
                BeginContext(530, 7, true);
                WriteLiteral("Table-1");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(546, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 15 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
                     foreach (var i in Model.tables)
                    {

#line default
#line hidden
            BeginContext(625, 24, true);
            WriteLiteral("                        ");
            EndContext();
            BeginContext(649, 50, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb919252", async() => {
                BeginContext(679, 11, false);
#line 17 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
                                                Write(i.tableName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#line 17 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
                           WriteLiteral(i.tableName);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(699, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 18 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
                    }

#line default
#line hidden
            BeginContext(724, 226, true);
            WriteLiteral("                </select><span id=\"invalidPkey\" style=\"color:red\">*</span>\r\n            </div>\r\n            <div class=\"col\">\r\n                <select id=\"relation\" class=\" browser-default custom-select\">\r\n                    ");
            EndContext();
            BeginContext(950, 41, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb9111814", async() => {
                BeginContext(976, 6, true);
                WriteLiteral("Choose");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(991, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1013, 48, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb9113631", async() => {
                BeginContext(1041, 11, true);
                WriteLiteral("One to many");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1061, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(1083, 46, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb9115035", async() => {
                BeginContext(1110, 10, true);
                WriteLiteral("One to one");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1129, 150, true);
            WriteLiteral("\r\n                </select><span id=\"invalidRelation\" style=\"color:red\">*</span>\r\n            </div>\r\n            <div class=\"col\" id=\"foreignKey1\">\r\n");
            EndContext();
#line 29 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
                  
                    Html.RenderPartial("_PartialForeignKey", Model.tables);
                

#line default
#line hidden
            BeginContext(1395, 223, true);
            WriteLiteral("            </div>\r\n            <div class=\"col\" id=\"lastChild1\">\r\n                <button onclick=\"getForeignKey()\" class=\"btn \">Add</button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div id=\"viewRelation\">\r\n");
            EndContext();
#line 40 "D:\Asma\SEMESTERS\FYP\EXTERNAL FINAL CODE\Source Code\Final Code\FYP1\FYP1\Views\EditDatabase\_PartialMakeRelation.cshtml"
          
            Html.RenderPartial("_partialRelation", Model);
        

#line default
#line hidden
            BeginContext(1701, 26, true);
            WriteLiteral("    </div>\r\n\r\n</section>\r\n");
            EndContext();
            BeginContext(1727, 51, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "517cc12510acc7f8dc62236d3e34f513fa79bb9117592", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1778, 3144, true);
            WriteLiteral(@"
<script>
        var pkError=true;
        var fkError=true;
        var relationError=true;
         $(function() {
            $(""#invalidPkey"").hide();
            $(""#invalidRelation"").hide();
            $(""#invalidfKey"").hide();
            $(""#pKey"").focusout(function(){
             pKeyCheck();
             });
            $(""#fKey"").focusout(function(){
                 fKeyCheck();
            });
          $(""#relation"").focusout(function(){
                 relationCheck();

            });


        });
        function relationCheck(){
          var relation1= $(""#fKey"").val();
            if(relation1!=null){
                 $(""#relation"").css(""border-color"",""#D8D8D8"");
                 $(""#invalidRelation"").hide();
                 relationError=true;
            }
            else{
                 $(""#relation"").css(""border-color"",""red"");
                 $(""#invalidRelation"").show();
                 relationError=false;
            }
        }
        ");
            WriteLiteral(@"function  fKeyCheck(){
             var fKey1= $(""#fKey"").val();
            if(fKey1!=null){
                 $(""#fKey"").css(""border-color"",""#D8D8D8"");
                 $(""#invalidfKey"").hide();
                 fkError=true;
            }
            else{
                 $(""#fKey"").css(""border-color"",""red"");
                 $(""#invalidfKey"").show();
                 fkError=false;
            }
        }
        function  pKeyCheck(){
            var pKey1= $(""#pKey"").val();
            if(pKey1!=null){
                 $(""#pKey"").css(""border-color"",""#D8D8D8"");
                 $(""#invalidPkey"").hide();
                 pKError=true;

                $.ajax({
                    url: ""/EditDatabase/getForeignKeyList"",
                    type: ""POST"",
                    data: {
                        pKey: pKey1,
                    },
                    success: function (data) {

                        $(""#foreignKey1"").html(data);
                         $(""#invalidfK");
            WriteLiteral(@"ey"").hide();
                    },
                    error: function (err) {
                        console.error();
                    }
                });


            }
            else{
                 $(""#pKey"").css(""border-color"",""red"");
                 $(""#invalidPkey"").show();
                 pkError=false;
            }
        }
</script>
<script>
    function getForeignKey() {
        pKeyCheck();
        fKeyCheck();
        relationCheck();
        if(pkError && fkError && relationError){
           $.ajax({
                url: ""/EditDatabase/MakeRelation"",
                type: ""POST"",
                data: {
                    pKey: $(""#pKey"").val(),
                    fKey:$(""#fKey"").val(),
                    relation:$(""#relation"").val()
                },
                success: function (data) {
                    $(""#viewRelation"").html(data);
                },
                error: function (err) {
                    console.error();
 ");
            WriteLiteral("               }\r\n            });\r\n        }else{}\r\n    }\r\n\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FYP1.ViewModel.tableRelation> Html { get; private set; }
    }
}
#pragma warning restore 1591
