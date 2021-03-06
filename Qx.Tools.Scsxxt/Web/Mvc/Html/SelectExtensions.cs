﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Qx.Tools.Scsxxt.Web.Mvc.Html
{
    public static class SelectExtensions
    {
        public static MvcHtmlString SelectFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList,
            string tipString = "", bool readOnly = false)
        {
            var dest = new StringBuilder();
            dest.Append("<div class='form-group'>");
            dest.Append(htmlHelper.LabelFor(expression, new {@class = "col-md-3 control-label"}));
            dest.Append(" <div class='col-md-9'>");
            dest.Append(readOnly
                ? htmlHelper.DropDownListFor(expression, selectList,
                    new {@class = "form-control", @readonly = "readonly"})
                : htmlHelper.DropDownListFor(expression, selectList, new {@class = "form-control"}));
            dest.Append("<span class='help-block'>");
            dest.Append(tipString);
            dest.Append(" </span>");
            dest.Append(" </div>");
            dest.Append(" </div>");
            return new MvcHtmlString(dest.ToString());
        }

        public static MvcHtmlString Select2For<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList,
            string tipString = "", bool readOnly = false)
        {
            var dest = new StringBuilder();
            dest.Append("<div class='form-group'>");
            dest.Append("<label>");
            dest.Append(htmlHelper.DisplayNameFor(expression));
            dest.Append("</label>");
            dest.Append(readOnly
                ? htmlHelper.DropDownListFor(expression, selectList,
                    new {@class = "form-control", @readonly = "readonly"})
                : htmlHelper.DropDownListFor(expression, selectList, new {@class = "form-control"}));
            dest.Append("<p class=\"help-block\">");
            dest.Append(tipString);
            dest.Append("</p>");
            dest.Append("</div>");
            return new MvcHtmlString(dest.ToString());
        }
    }
}