using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace SinavOlusturmaProjesi.DAL
{
    public class WiredDal
    {
		public List<Models.Wired> GetRSS()
		{
			List<Models.Wired> wiredPosts = new List<Models.Wired>();

			string url = "https://www.wired.com/feed/rss";
			XmlReader reader = XmlReader.Create(url);
			SyndicationFeed feed = SyndicationFeed.Load(reader);
			reader.Close();

			int counter = 0;
			foreach (var item in feed.Items)
			{
				Models.Wired wired = new Models.Wired();
				wired.Title = item.Title.Text;
				wired.Description = item.Summary.Text;
				wired.Id = counter;
				wiredPosts.Add(wired);
				counter++;
				if (counter >= 5)
					break;
			}
			return wiredPosts;
		}
	}
}
