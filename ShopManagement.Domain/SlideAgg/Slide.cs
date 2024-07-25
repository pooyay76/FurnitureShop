using Framework.Domain;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }

        public Slide(string picture,string link, string pictureTitle, string pictureAlt, string heading, string title, string text, string btnText)
        {
            Link = link;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
        }
        public void Edit(string picture,string link, string pictureTitle, string pictureAlt, string heading, string title, string text, string btnText)
        {
            if (string.IsNullOrWhiteSpace(picture)==false) 
            {
                Picture = picture;
            }

            Link = link;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
        }
    }
}
