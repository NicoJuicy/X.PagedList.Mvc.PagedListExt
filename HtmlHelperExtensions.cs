using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace X.PagedList.Mvc.PagedListExt
{
	[DebuggerStepThrough]
	public static class HtmlHelperExtensions
	{
		private static MvcHtmlString _pagerBuilder(AjaxHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, AjaxOptions ajaxOptions, string pagerCss = "", int totalGroups = 15, string prevText = "<<", string nextText = ">>")
		{
			int num = totalGroups;
			int num1 = index;
			int pageSize = list.PageSize;
			int totalCount = list.TotalCount;
			int num2 = (int)Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(pageSize));
			int num3 = (int)Math.Ceiling(Convert.ToDecimal(num2) / Convert.ToDecimal(num));
			int num4 = 0;
			if (num2 <= 1)
			{
				return MvcHtmlString.Empty;
			}
			num4 = (int)Math.Floor(Convert.ToDecimal(num1) / Convert.ToDecimal(num));
			int num5 = num4 * num;
			int num6 = num5 + num;
			if (num6 > num2)
			{
				num6 = num2;
			}
			TagBuilder tagBuilder = new TagBuilder("div");
			tagBuilder.GenerateId("calabongapager");
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder str = new TagBuilder("ul");
			if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
			{
				str.AddCssClass(pagerCss);
			}
			TagBuilder tagBuilder1 = null;
			MvcHtmlString mvcHtmlString = null;
			if (num4 > 0)
			{
				tagBuilder1 = new TagBuilder("li");
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num5.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, htmlAttributes);
				}
				else
				{
					tagBuilder1.AddCssClass("ui-state-default ui-corner-all ui-button ui-widget ui-button-text-only");
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num5.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, new { @class = "ui-button-text" });
				}
				tagBuilder1.InnerHtml = mvcHtmlString.ToString();
				stringBuilder.Append(tagBuilder1.ToString(TagRenderMode.Normal));
			}
			for (int i = num5; i < num6; i++)
			{
				if (i != num1)
				{
					tagBuilder1 = new TagBuilder("li");
					if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						int num7 = i + 1;
						int num8 = i + 1;
						mvcHtmlString = source.ActionLink(num7.ToString(CultureInfo.CurrentCulture), string.Concat(actionName, "/", num8.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, htmlAttributes);
					}
					else
					{
						tagBuilder1.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
						int num9 = i + 1;
						int num10 = i + 1;
						mvcHtmlString = source.ActionLink(num9.ToString(CultureInfo.CurrentCulture), string.Concat(actionName, "/", num10.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, new { @class = "ui-button-text" });
					}
					tagBuilder1.InnerHtml = mvcHtmlString.ToString();
				}
				else
				{
					tagBuilder1 = new TagBuilder("li");
					TagBuilder tagBuilder2 = new TagBuilder("span");
					tagBuilder1.AddCssClass("active");
					string str1 = i.ToString(CultureInfo.InvariantCulture);
					if (pagerCss.Equals("pagination", StringComparison.InvariantCulture))
					{
						TagBuilder tagBuilder3 = new TagBuilder("span");
						tagBuilder3.AddCssClass("sr-only");
						str1 = string.Concat(str1, tagBuilder3.InnerHtml);
					}
					if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						tagBuilder2.AddCssClass("active");
					}
					tagBuilder2.SetInnerText(str1);
					if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						tagBuilder2.AddCssClass("ui-button-text");
						tagBuilder1.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
					}
					tagBuilder1.InnerHtml = tagBuilder2.ToString(TagRenderMode.Normal);
				}
				stringBuilder.Append(tagBuilder1.ToString(TagRenderMode.Normal));
			}
			if (num3 > 0 && num4 < num3 - 1)
			{
				tagBuilder1 = new TagBuilder("li");
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					int num11 = num6 + 1;
					mvcHtmlString = source.ActionLink(nextText, string.Concat(actionName, "/", num11.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, htmlAttributes);
				}
				else
				{
					tagBuilder1.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
					int num12 = num6 + 1;
					mvcHtmlString = source.ActionLink(nextText, string.Concat(actionName, "/", num12.ToString(CultureInfo.CurrentCulture)), routevalues, ajaxOptions, new { @class = "ui-button-text" });
				}
				tagBuilder1.InnerHtml = mvcHtmlString.ToString();
				stringBuilder.Append(tagBuilder1.ToString(TagRenderMode.Normal));
			}
			str.InnerHtml = stringBuilder.ToString();
			tagBuilder.InnerHtml = str.ToString();
			return MvcHtmlString.Create(tagBuilder.ToString());
		}

		private static MvcHtmlString _pagerBuilderPrevNext(HtmlHelper source, int currentIndex, IPagedList list, string actionName, object routevalues, string prevText, string nextText, string firstText, string lastText, bool isShowInfo, bool isShowFirstLast, string pagerCss, string pageInfoFormat = "{0} - {1} ({2})")
		{
			MvcHtmlString mvcHtmlString;
			int num = 0;
			if (list == null || source == null)
			{
				return null;
			}
			if (currentIndex < 0)
			{
				return null;
			}
			num = currentIndex;
			TagBuilder tagBuilder = new TagBuilder("div");
			tagBuilder.GenerateId("pager");
			TagBuilder str = new TagBuilder("ul");
			if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
			{
				str.AddCssClass(pagerCss);
			}
			num++;
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder htmlString = null;
			TagBuilder tagBuilder1 = null;
			if (isShowFirstLast)
			{
				htmlString = new TagBuilder("li");
				if (!list.IsPreviousPage)
				{
					tagBuilder1 = new TagBuilder("span");
					tagBuilder1.SetInnerText(firstText);
					if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						tagBuilder1.AddCssClass("ui-button-text");
						htmlString.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
					}
					htmlString.InnerHtml = tagBuilder1.ToString();
				}
				else
				{
					MvcHtmlString mvcHtmlString1 = null;
					if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						mvcHtmlString1 = source.ActionLink(firstText, string.Concat(actionName, "/", "1"), routevalues);
					}
					else
					{
						htmlString.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
						mvcHtmlString1 = source.ActionLink(firstText, string.Concat(actionName, "/", "1"), routevalues, new { @class = "ui-button-text" });
					}
					htmlString.InnerHtml = mvcHtmlString1.ToHtmlString();
				}
				stringBuilder.Append(htmlString.ToString());
			}
			htmlString = new TagBuilder("li");
			if (!list.IsPreviousPage)
			{
				tagBuilder1 = new TagBuilder("span");
				tagBuilder1.SetInnerText(prevText);
				if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					tagBuilder1.AddCssClass("ui-button-text");
					htmlString.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
				}
				htmlString.InnerHtml = tagBuilder1.ToString();
			}
			else
			{
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					int num1 = num - 1;
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num1.ToString(CultureInfo.InvariantCulture)), routevalues, null);
				}
				else
				{
					htmlString.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
					int num2 = num - 1;
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num2.ToString(CultureInfo.InvariantCulture)), routevalues, new { @class = "ui-button-text" });
				}
				htmlString.InnerHtml = mvcHtmlString.ToHtmlString();
			}
			stringBuilder.Append(htmlString.ToString());
			if (isShowInfo)
			{
				htmlString = new TagBuilder("li");
				TagBuilder tagBuilder2 = new TagBuilder("span");
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				object[] objArray = new object[] { num.ToString(CultureInfo.InvariantCulture), list.TotalPages, list.TotalCount };
				tagBuilder2.InnerHtml = string.Format(invariantCulture, pageInfoFormat, objArray);
				htmlString.InnerHtml = tagBuilder2.ToString();
				stringBuilder.Append(htmlString.ToString());
			}
			htmlString = new TagBuilder("li");
			MvcHtmlString mvcHtmlString2 = null;
			if (!list.IsNextPage)
			{
				tagBuilder1 = new TagBuilder("span");
				tagBuilder1.SetInnerText(nextText);
				if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					tagBuilder1.AddCssClass("ui-button-text");
					htmlString.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
				}
				htmlString.InnerHtml = tagBuilder1.ToString();
			}
			else
			{
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					int num3 = num + 1;
					mvcHtmlString2 = source.ActionLink(nextText, string.Concat(actionName, "/", num3.ToString()), routevalues, null);
				}
				else
				{
					htmlString.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
					int num4 = num + 1;
					mvcHtmlString2 = source.ActionLink(nextText, string.Concat(actionName, "/", num4.ToString()), routevalues, new { @class = "ui-button-text" });
				}
				htmlString.InnerHtml = mvcHtmlString2.ToHtmlString();
			}
			stringBuilder.Append(htmlString.ToString());
			if (isShowFirstLast)
			{
				htmlString = new TagBuilder("li");
				MvcHtmlString mvcHtmlString3 = null;
				if (num + 1 > list.TotalPages)
				{
					tagBuilder1 = new TagBuilder("span");
					tagBuilder1.SetInnerText(lastText);
					if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						tagBuilder1.AddCssClass("ui-button-text");
						htmlString.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
					}
					htmlString.InnerHtml = tagBuilder1.ToString();
				}
				else
				{
					if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						int totalPages = list.TotalPages;
						mvcHtmlString3 = source.ActionLink(lastText, string.Concat(actionName, "/", totalPages.ToString()), routevalues, null);
					}
					else
					{
						htmlString.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
						int totalPages1 = list.TotalPages;
						mvcHtmlString3 = source.ActionLink(lastText, string.Concat(actionName, "/", totalPages1.ToString()), routevalues, new { @class = "ui-button-text" });
					}
					htmlString.InnerHtml = mvcHtmlString3.ToHtmlString();
				}
				stringBuilder.Append(htmlString.ToString());
			}
			str.InnerHtml = stringBuilder.ToString();
			tagBuilder.InnerHtml = str.ToString(TagRenderMode.Normal);
			return MvcHtmlString.Create(tagBuilder.ToString());
		}

		private static MvcHtmlString BootstrapPagerBuilder(HtmlHelper source, IPagedList list, string templateUrl, object routevalues, object htmlAttributes, int totalGroups = 15, string prevText = "&laquo;", string nextText = "&raquo;")
		{
			TagBuilder tagBuilder;
			int num = totalGroups;
			int pageIndex = list.PageIndex;
			int pageSize = list.PageSize;
			int totalCount = list.TotalCount;
			int num1 = (int)Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(pageSize));
			int num2 = (int)Math.Ceiling(Convert.ToDecimal(num1) / Convert.ToDecimal(num));
			if (num1 <= 1)
			{
				return MvcHtmlString.Empty;
			}
			int num3 = (int)Math.Floor(Convert.ToDecimal(pageIndex) / Convert.ToDecimal(num));
			int num4 = num3 * num;
			int num5 = num4 + num;
			if (num5 > num1)
			{
				num5 = num1;
			}
			TagBuilder str = new TagBuilder("nav");
			str.GenerateId("calabongapager");
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder tagBuilder1 = new TagBuilder("ul");
			tagBuilder1.AddCssClass("pagination");
			if (num3 > 0)
			{
				tagBuilder = new TagBuilder("li");
				TagBuilder tagBuilder2 = new TagBuilder("a");
				tagBuilder2.Attributes.Add("href", templateUrl.Replace("{PageIndex}", num4.ToString(CultureInfo.InvariantCulture)));
				tagBuilder2.InnerHtml = prevText;
				tagBuilder.InnerHtml = tagBuilder2.ToString();
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			for (int i = num4; i < num5; i++)
			{
				if (i != pageIndex)
				{
					tagBuilder = new TagBuilder("li");
					if (string.IsNullOrWhiteSpace(templateUrl))
					{
						throw new ArgumentNullException("templateUrl");
					}
					if (!templateUrl.Contains("{PageIndex}"))
					{
						throw new ArgumentException("TemplateUrl for PagerForPagedListBootstrap builer should contain a {PageIndex} path for replace with current page index.");
					}
					TagBuilder str1 = new TagBuilder("a");
					int num6 = i + 1;
					str1.Attributes.Add("href", templateUrl.Replace("{PageIndex}", num6.ToString(CultureInfo.InvariantCulture)));
					int num7 = i + 1;
					str1.InnerHtml = num7.ToString(CultureInfo.InvariantCulture);
					tagBuilder.InnerHtml = str1.ToString();
				}
				else
				{
					tagBuilder = new TagBuilder("li");
					TagBuilder tagBuilder3 = new TagBuilder("a");
					TagBuilder tagBuilder4 = new TagBuilder("span");
					tagBuilder.AddCssClass("active");
					string str2 = (i + 1).ToString(CultureInfo.InvariantCulture);
					tagBuilder4.AddCssClass("sr-only");
					tagBuilder4.SetInnerText("(current)");
					tagBuilder3.InnerHtml = string.Concat(str2, tagBuilder4.ToString());
					tagBuilder3.Attributes.Add("href", "#");
					tagBuilder.InnerHtml = tagBuilder3.ToString();
				}
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			if (num2 > 0 && num3 < num2 - 1)
			{
				tagBuilder = new TagBuilder("li");
				TagBuilder tagBuilder5 = new TagBuilder("a");
				int num8 = num5 + 1;
				tagBuilder5.Attributes.Add("href", templateUrl.Replace("{PageIndex}", num8.ToString(CultureInfo.InvariantCulture)));
				tagBuilder5.InnerHtml = nextText;
				tagBuilder.InnerHtml = tagBuilder5.ToString();
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			tagBuilder1.InnerHtml = stringBuilder.ToString();
			str.InnerHtml = tagBuilder1.ToString();
			return MvcHtmlString.Create(str.ToString());
		}

		private static MvcHtmlString PagerBuilder(HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, object htmlAttributes, string pagerCss = "", int totalGroups = 15, string prevText = "<<", string nextText = ">>")
		{
			TagBuilder tagBuilder;
			MvcHtmlString mvcHtmlString;
			int num = totalGroups;
			int num1 = index;
			int pageSize = list.PageSize;
			int totalCount = list.TotalCount;
			int num2 = (int)Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(pageSize));
			int num3 = (int)Math.Ceiling(Convert.ToDecimal(num2) / Convert.ToDecimal(num));
			if (num2 <= 1)
			{
				return MvcHtmlString.Empty;
			}
			int num4 = (int)Math.Floor(Convert.ToDecimal(num1) / Convert.ToDecimal(num));
			int num5 = num4 * num;
			int num6 = num5 + num;
			if (num6 > num2)
			{
				num6 = num2;
			}
			TagBuilder str = new TagBuilder("div");
			str.GenerateId("calabongapager");
			if (pagerCss.Equals("paginationMetro"))
			{
				str.AddCssClass("pagination");
			}
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder tagBuilder1 = new TagBuilder("ul");
			if (!HtmlHelperExtensions.UseJQueryCss(pagerCss) && !pagerCss.Equals("paginationMetro"))
			{
				tagBuilder1.AddCssClass(pagerCss);
			}
			if (num4 > 0)
			{
				tagBuilder = new TagBuilder("li");
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num5.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, new { @class = "first" });
				}
				else
				{
					tagBuilder.AddCssClass("ui-state-default ui-corner-all ui-button ui-widget ui-button-text-only");
					mvcHtmlString = source.ActionLink(prevText, string.Concat(actionName, "/", num5.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, new { @class = "ui-button-text" });
				}
				tagBuilder.InnerHtml = mvcHtmlString.ToString();
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			for (int i = num5; i < num6; i++)
			{
				if (i != num1)
				{
					tagBuilder = new TagBuilder("li");
					if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						int num7 = i + 1;
						int num8 = i + 1;
						mvcHtmlString = source.ActionLink(num7.ToString(CultureInfo.CurrentCulture), string.Concat(actionName, "/", num8.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, htmlAttributes);
					}
					else
					{
						tagBuilder.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
						int num9 = i + 1;
						int num10 = i + 1;
						mvcHtmlString = source.ActionLink(num9.ToString(CultureInfo.CurrentCulture), string.Concat(actionName, "/", num10.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, new { @class = "ui-button-text" });
					}
					tagBuilder.InnerHtml = mvcHtmlString.ToString();
				}
				else
				{
					tagBuilder = new TagBuilder("li");
					if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
					{
						tagBuilder.AddCssClass("ui-state-default ui-corner-all ui-button ui-widget ui-button-text-only");
						TagBuilder tagBuilder2 = new TagBuilder("span");
						if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
						{
							tagBuilder2.AddCssClass("active");
						}
						tagBuilder2.SetInnerText((i + 1).ToString());
						tagBuilder = new TagBuilder("li");
						if (HtmlHelperExtensions.UseJQueryCss(pagerCss))
						{
							tagBuilder2.AddCssClass("ui-button-text");
							tagBuilder.AddCssClass("ui-state-default ui-state-disabled ui-corner-all ui-button ui-widget ui-button-text-only");
						}
						tagBuilder.InnerHtml = tagBuilder2.ToString(TagRenderMode.Normal);
					}
					else if (!pagerCss.Equals("paginationMetro"))
					{
						TagBuilder tagBuilder3 = new TagBuilder("span");
						tagBuilder.AddCssClass("active");
						string str1 = (i + 1).ToString(CultureInfo.InvariantCulture);
						if (pagerCss.Equals("pagination", StringComparison.InvariantCulture))
						{
							TagBuilder tagBuilder4 = new TagBuilder("span");
							tagBuilder4.AddCssClass("sr-only");
							str1 = string.Concat(str1, tagBuilder4.InnerHtml);
						}
						tagBuilder3.SetInnerText(str1);
						tagBuilder.InnerHtml = tagBuilder3.ToString();
					}
					else
					{
						TagBuilder tagBuilder5 = new TagBuilder("a");
						tagBuilder.AddCssClass("active disabled");
						int num11 = i + 1;
						tagBuilder5.SetInnerText(num11.ToString(CultureInfo.InvariantCulture));
						tagBuilder.InnerHtml = tagBuilder5.ToString();
					}
				}
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			if (num3 > 0 && num4 < num3 - 1)
			{
				tagBuilder = new TagBuilder("li");
				if (!HtmlHelperExtensions.UseJQueryCss(pagerCss))
				{
					int num12 = num6 + 1;
					mvcHtmlString = source.ActionLink(nextText, string.Concat(actionName, "/", num12.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, htmlAttributes);
				}
				else
				{
					tagBuilder.AddCssClass("ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only");
					int num13 = num6 + 1;
					mvcHtmlString = source.ActionLink(nextText, string.Concat(actionName, "/", num13.ToString(CultureInfo.CurrentCulture)), controllerName, routevalues, new { @class = "ui-button-text" });
				}
				tagBuilder.InnerHtml = mvcHtmlString.ToString();
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.Normal));
			}
			tagBuilder1.InnerHtml = stringBuilder.ToString();
			str.InnerHtml = tagBuilder1.ToString();
			return MvcHtmlString.Create(str.ToString());
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, string targetName, string loadingElement)
		{
			AjaxOptions ajaxOption = new AjaxOptions()
			{
				HttpMethod = "GET",
				LoadingElementId = loadingElement,
				UpdateTargetId = targetName
			};
			AjaxOptions ajaxOption1 = ajaxOption;
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, null, null, ajaxOption1, string.Empty, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, string targetName, string loadingElement, string pagerCss)
		{
			AjaxOptions ajaxOption = new AjaxOptions()
			{
				HttpMethod = "GET",
				LoadingElementId = loadingElement,
				UpdateTargetId = targetName
			};
			AjaxOptions ajaxOption1 = ajaxOption;
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, null, null, ajaxOption1, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, object routevalues, AjaxOptions opitions, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, routevalues, null, opitions, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, AjaxOptions opitions, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, routevalues, htmlAttributes, opitions, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, object routevalues, AjaxOptions opitions)
		{
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, routevalues, null, opitions, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this AjaxHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, AjaxOptions opitions, string pagerCss, int totalGroups, string prevText, string nextText)
		{
			return HtmlHelperExtensions._pagerBuilder(source, index, list, actionName, routevalues, htmlAttributes, opitions, pagerCss, totalGroups, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, null, null, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, null, null, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, null, null, string.Empty, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, null, null, string.Empty, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, string pagerCss)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, null, null, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, string pagerCss, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, null, null, pagerCss, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, null, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, null, "", totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, null, "", totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string pagerCss)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, null, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, string pagerCss)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, null, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string pagerCss, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, null, pagerCss, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, string pagerCss, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, null, pagerCss, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, htmlAttributes, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, object htmlAttributes)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, htmlAttributes, "", 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, htmlAttributes, "", totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, object htmlAttributes, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, htmlAttributes, "", totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, string pagerCss)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, htmlAttributes, pagerCss, 15, "<<", ">>");
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, object htmlAttributes, string pagerCss, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, string.Empty, routevalues, htmlAttributes, pagerCss, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, string controllerName, object routevalues, object htmlAttributes, string pagerCss, int totalInGroup, string prevText, string nextText)
		{
			return HtmlHelperExtensions.PagerBuilder(source, index, list, actionName, controllerName, routevalues, htmlAttributes, pagerCss, totalInGroup, prevText, nextText);
		}

		public static MvcHtmlString PagerForPagedListBootstrap(this HtmlHelper source, IPagedList list, string templateUrl)
		{
			return HtmlHelperExtensions.BootstrapPagerBuilder(source, list, templateUrl, null, null, 15, "&laquo;", "&raquo;");
		}

		public static MvcHtmlString PagerForPagedListBootstrap(this HtmlHelper source, IPagedList list, string templateUrl, int totalGroups)
		{
			return HtmlHelperExtensions.BootstrapPagerBuilder(source, list, templateUrl, null, null, totalGroups, "&laquo;", "&raquo;");
		}

		public static MvcHtmlString PrevNextForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string prevText, string nextText, string firstText, string lastText, string pageInfoFormat, bool isShowInfo, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilderPrevNext(source, index, list, actionName, routevalues, prevText, nextText, firstText, lastText, isShowInfo, true, pageInfoFormat, pagerCss);
		}

		public static MvcHtmlString PrevNextForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string prevText, string nextText, string firstText, string lastText, bool isShowInfo, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilderPrevNext(source, index, list, actionName, routevalues, prevText, nextText, firstText, lastText, isShowInfo, true, pagerCss, "{0} - {1} ({2})");
		}

		public static MvcHtmlString PrevNextForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string prevText, string nextText, bool isShowInfo, bool isShowFirstLast, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilderPrevNext(source, index, list, actionName, routevalues, prevText, nextText, "<<", ">>", isShowInfo, isShowFirstLast, pagerCss, "{0} - {1} ({2})");
		}

		public static MvcHtmlString PrevNextForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, bool isShowInfo, bool isShowFirstLast, string pagerCss)
		{
			return HtmlHelperExtensions._pagerBuilderPrevNext(source, index, list, actionName, routevalues, "<", ">", "<<", ">>", isShowInfo, isShowFirstLast, pagerCss, "{0} - {1} ({2})");
		}

		public static MvcHtmlString PrevNextForPagedList(this HtmlHelper source, int index, IPagedList list, string actionName, object routevalues, string pageInfoFormat, bool isShowFirstLast)
		{
			return HtmlHelperExtensions._pagerBuilderPrevNext(source, index, list, actionName, routevalues, "<", ">", "<<", ">>", true, isShowFirstLast, pageInfoFormat, "{0} - {1} ({2})");
		}

		private static bool UseJQueryCss(string pagerCss)
		{
			return string.IsNullOrEmpty(pagerCss);
		}
	}
}