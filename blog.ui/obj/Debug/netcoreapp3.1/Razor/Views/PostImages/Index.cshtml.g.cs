#pragma checksum "/Users/muratvuranok/Desktop/blog/blog.ui/Views/PostImages/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6d20252eca10bcf7a97962372e7066972049e7c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(blog.ui.PostImages.Views_PostImages_Index), @"mvc.1.0.view", @"/Views/PostImages/Index.cshtml")]
namespace blog.ui.PostImages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d20252eca10bcf7a97962372e7066972049e7c7", @"/Views/PostImages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85560ee1b9569a03d4627f40f17733acc2253933", @"/Views/_ViewImports.cshtml")]
    public class Views_PostImages_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Guid>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div  class=\"row row-col-4 mt-5\" id=\"card\"></div>\n\n");
            DefineSection("js", async() => {
                WriteLiteral("\n    <script>\n        $(document).ready(function(){\n            (function(){\n\n$.getJSON(\"/postimages/_index/");
#nullable restore
#line 9 "/Users/muratvuranok/Desktop/blog/blog.ui/Views/PostImages/Index.cshtml"
                         Write(Model);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""", function(data){

 $.each(data,function(key,value){

       $(""#card"").append(`<div class=""col-3 mb-3 border rounded p-2 shadow-sm""> 
                          <img src=""${value.imageUrl}"" alt=""bilgeadam"" class=""img-thumbnail mb-2"" />
                          <div class=""btn-group d-flex"">
        <a data-id=""${value.id}"" class=""btn btn-dark btn-sm main"" href=""#"" > 
             <i class=""fa fa-cogs text-${value.active ? 'success': 'warning'}""></i> <a>
        <a data-id=""${value.id}"" class=""btn btn-dark btn-sm delete"" href=""#"" >  <i class=""fa fa-trash text-danger""></i> <a> </div> </div>`);
 })
})
            })();


            $(document).on(""click"","".main"",function(){
                var button = $(this);
                var id = $(button).data(""id"");
                $.ajax({
                    url:""/postimages/_active/""+ id,
                    dataType:""json"",
                    type:""post"",
                    success:function(result){
if(result.code == 200){
    $(""#card svg"")
    .removeClass(""te");
                WriteLiteral(@"xt-success"")
    .addClass(""text-warning"");

    $(button).children()
    .removeClass(""text-warning"")
    .addClass(""text-success"");
}
                    },
                    error:function(error){

                    }
                })
            })
$(document).on(""click"","".delete"",function(){
    var id = $(this).data(""id""); 
    var buton = $(this);
    $.ajax({
        url:""/postimages/_delete/""+id,
        dataType:""json"",
        type :""post"",
        success:function(result){
            if(result== 200){
                $(buton).closest("".col-3"").hide(""slow"")
            }
        },
        error: function(){}
    })
})


        })
    </script>
");
            }
            );
            WriteLiteral("\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Guid> Html { get; private set; }
    }
}
#pragma warning restore 1591
