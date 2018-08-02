using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Hisar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Hosting.Core
{
    public class MainMenuItemsRenderer : DefaultMenuItemsRenderer
    {
        public MainMenuItemsRenderer(IEnumerable<IMenuItemsBuilder> builders) : base(builders)
        {
        }

        public override IHtmlContent Render(IUrlHelper urlHelper, Action<IDictionary<ComponentPair, IEnumerable<IMenuItem>>> filter = null)
        {
            IDictionary<ComponentPair, IEnumerable<IMenuItem>> menuItems = PopulateMenuItems(urlHelper);
            filter?.Invoke(menuItems);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<ComponentPair, IEnumerable<IMenuItem>> entry in menuItems)
            {
                string title = entry.Key.Title;
                sb.Append($"<li class=\"nav-item dropdown\">" +
        $"<a href=\"javascript:void(0)\" class=\"nav-link\" data-toggle=\"dropdown\"><i class=\"fa fa-server\"></i>{title}</a>");

                if (entry.Value != null && entry.Value.Any())
                {
                    sb.Append("<div class=\"dropdown-menu dropdown-menu-arrow\">");
                    foreach (var menu in entry.Value)
                    {
                        if (!menu.ShowInMenu)
                            continue;

                        sb.Append($"<a href=\"{menu.Path}\" class=\"dropdown-item\">{menu.Text}</a>");
                    }
                    sb.Append("</div>");
                }
            }

            sb.Append("</li>");

            return new HtmlString(sb.ToString());
        }
    }
}
