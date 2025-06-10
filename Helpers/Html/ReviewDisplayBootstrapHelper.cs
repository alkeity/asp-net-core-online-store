using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Models.Entities;
using System.Text.Encodings.Web;

namespace OnlineStore.Helpers.Html
{
    public static class ReviewDisplayBootstrapHelper
    {
        public static HtmlString Review(this IHtmlHelper helper, Review review)
        {
            TagBuilder divCard = new TagBuilder("div");
            divCard.Attributes.Add("class", "card mb-3");

            TagBuilder divCardInner = new TagBuilder("div");
            divCardInner.Attributes.Add("class", "card-body");

            TagBuilder h5Username = new TagBuilder("h5");
            h5Username.Attributes.Add("class", "card-text");
            h5Username.InnerHtml.Append($"From: {review.Username}");

            TagBuilder divStars = new TagBuilder("div");

            for (int i = 0; i < review.Rating; i++)
            {
                divStars.InnerHtml.Append("★");
            }

            for (int i = review.Rating; i < 5; i++)
            {
                divStars.InnerHtml.Append("☆");
            }

            TagBuilder pText = new TagBuilder("p");
            pText.Attributes.Add("class", "card-text");
            pText.InnerHtml.Append(review.Text);

            divCard.InnerHtml.AppendHtml(divCardInner);

            divCardInner.InnerHtml.AppendHtml(h5Username);
            divCardInner.InnerHtml.AppendHtml(divStars);
            divCardInner.InnerHtml.AppendHtml(pText);

            using StringWriter writer = new StringWriter();
            divCard.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
