#pragma checksum "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3eae102a550b218e590e8cd113dc98a3d798afa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_Detail), @"mvc.1.0.view", @"/Views/Customer/Detail.cshtml")]
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
#line 1 "C:\Users\ProBook\Desktop\test2\Views\_ViewImports.cshtml"
using Lib_Management;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ProBook\Desktop\test2\Views\_ViewImports.cshtml"
using Lib_Management.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3eae102a550b218e590e8cd113dc98a3d798afa", @"/Views/Customer/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cebbc6dc6ae9d35f9064e8b08ccbd25a2110dcef", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Lib_Management.Models.Customer.CustomerDetailModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container"">
    <div class=""card-header clearfix detailHeading"">
        <h2 class=""text-muted"">Customer Information</h2>
    </div>
    <div class=""jumbotron"">
        <div class=""row"">
            <div class=""col-md-4"">
                <div>
                    <h2>");
#nullable restore
#line 11 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                   Write(Model.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <div class=\"customerContact\">\r\n                        <div class=\"customerTel\">Library Card Id : ");
#nullable restore
#line 13 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                              Write(Model.LibraryCardId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div class=\"customerAddress\">Address : ");
#nullable restore
#line 14 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                          Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div class=\"customerTel\">Telephone : ");
#nullable restore
#line 15 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                        Write(Model.Telephone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div class=\"customerDate\">Member Since : ");
#nullable restore
#line 16 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                            Write(Model.MemberSince);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div class=\"customerLibrary\">Home Library : ");
#nullable restore
#line 17 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                               Write(Model.HomeLibraryBranch);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 18 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                         if(@Model.OverdueFees > 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div id=\"customerHasFees\">Current Fees Due : $Model.OverdueFees</div>\r\n");
#nullable restore
#line 21 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div id=\"customerFees\">No Fees Currently Due.</div>\r\n");
#nullable restore
#line 25 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-md-4\">\r\n                <h4>Assets Currently Checked Out</h4>\r\n");
#nullable restore
#line 31 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                 if (Model.AssetsCheckedOut.Any())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div id=\"customerAssets\">\r\n                        <ul>\r\n");
#nullable restore
#line 35 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                             foreach (var checkout in Model.AssetsCheckedOut)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li>\r\n                                    ");
#nullable restore
#line 38 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                               Write(checkout.LibraryAsset.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - (Library Asset ID : ");
#nullable restore
#line 38 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                                                  Write(checkout.LibraryAsset);

#line default
#line hidden
#nullable disable
            WriteLiteral(")\r\n                                    <ul>\r\n                                        <li>\r\n                                            Since : ");
#nullable restore
#line 41 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                               Write(checkout.Since);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </li>\r\n                                        <li>\r\n                                            Due : ");
#nullable restore
#line 44 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                             Write(checkout.Until);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </li>\r\n                                    </ul>\r\n                                </li>\r\n");
#nullable restore
#line 48 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </ul>\r\n                    </div>\r\n");
#nullable restore
#line 51 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                 }
                 else
                 {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div> No Items currently out.</div>\r\n");
#nullable restore
#line 55 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                 }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-md-4\">\r\n                <h4>Assets Currently On Hold</h4>\r\n");
#nullable restore
#line 60 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                 if(Model.Holds.Any())
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div id=\"customerHolds\">\r\n                        <ul>\r\n");
#nullable restore
#line 64 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                             foreach(var hold in Model.Holds)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li>");
#nullable restore
#line 66 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                               Write(hold.LibraryAsset.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - Placed ");
#nullable restore
#line 66 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                                                                 Write(hold.HoldPlaced.ToString("yy-dd-MM - HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 67 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </ul>\r\n                    </div>\r\n");
#nullable restore
#line 71 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div>No Items on  Hold.</div>\r\n");
#nullable restore
#line 75 "C:\Users\ProBook\Desktop\test2\Views\Customer\Detail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Lib_Management.Models.Customer.CustomerDetailModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
