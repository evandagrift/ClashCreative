#pragma checksum "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edf4f913145a321ef980c2d2687dfbb925c913ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Players_PlayerData), @"mvc.1.0.view", @"/Views/Players/PlayerData.cshtml")]
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
#line 1 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\_ViewImports.cshtml"
using ClashCreative;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\_ViewImports.cshtml"
using ClashCreative.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf4f913145a321ef980c2d2687dfbb925c913ea", @"/Views/Players/PlayerData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a4bf67fedd148ed26939cd2338821cb41e8b3f09", @"/Views/_ViewImports.cshtml")]
    public class Views_Players_PlayerData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PlayerDataModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"jumbotron\">\r\n        <div class=\"row\">\r\n            <div class=\"col-4\">\r\n                <p>Name:");
#nullable restore
#line 13 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                   Write(Model.Player.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Tag:");
#nullable restore
#line 14 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                  Write(Model.Player.Tag);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                <p>Clan:");
#nullable restore
#line 16 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                   Write(Model.Player.Clan.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Clan Tag:");
#nullable restore
#line 17 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                       Write(Model.Player.ClanTag);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n            <div class=\"col-4\">\r\n                <p>Level:");
#nullable restore
#line 20 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                    Write(Model.Player.ExpLevel);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Star Points:");
#nullable restore
#line 21 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                          Write(Model.Player.StarPoints);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Favorite Card:");
#nullable restore
#line 22 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                            Write(Model.Player.CurrentFavouriteCard.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n\r\n\r\n            <div class=\"col-4\">\r\n                <p>Current Donations:");
#nullable restore
#line 27 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                Write(Model.Player.Donations);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Current Donations Recieved:");
#nullable restore
#line 28 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                         Write(Model.Player.DonationsReceived);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Total Donations:");
#nullable restore
#line 29 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                              Write(Model.Player.TotalDonations);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Total Donations Recieved:");
#nullable restore
#line 30 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                       Write(Model.Player.ClanCardsCollected);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n            </div>\r\n            <div class=\"row m-3\"><p>Current Tropies:");
#nullable restore
#line 33 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                               Write(Model.Player.Trophies);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Highest Trophies:");
#nullable restore
#line 33 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                                                                       Write(Model.Player.BestTrophies);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Wins:");
#nullable restore
#line 33 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                                                                                                       Write(Model.Player.Wins);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Losses:");
#nullable restore
#line 33 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                                                                                                                                                 Write(Model.Player.Losses);

#line default
#line hidden
#nullable disable
            WriteLiteral("  \r\n                Cards Discovered ");
#nullable restore
#line 34 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
                            Write(Model.Player.CardsDiscovered);

#line default
#line hidden
#nullable disable
            WriteLiteral("/101</p></div>\r\n        </div>\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 37 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
       Write(Html.Raw(Model.Deck.ToString()));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 40 "C:\Users\FreshZeph\source\repos\ClashCreative\ClashCreative\Views\Players\PlayerData.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PlayerDataModel> Html { get; private set; }
    }
}
#pragma warning restore 1591