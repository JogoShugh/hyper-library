using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HyperLibrary.LinkClient
{
    public class LibraryLinkExplorer
    {
        private readonly LinkClient _libraryLinkClient;

        public LibraryLinkExplorer(Uri serverEndpoint)
        {
            _libraryLinkClient = new LinkClient(serverEndpoint);
        }

        public async Task Explore()
        {
            Console.WriteLine();
            Console.WriteLine("**************** Welcome to the Library ****************");
            dynamic root = await _libraryLinkClient.GetRoot();
            Console.WriteLine(root);
            var link = SelectAvalibleLink(root);
            await FollowYourNose(link);
        }

        private async Task FollowYourNose(dynamic linkToFollow)
        {
            Console.WriteLine("**** Follow your nose {0} -{1} ****",linkToFollow.Rel,linkToFollow.Name);
            var response = await _libraryLinkClient.Follow(linkToFollow);
            Console.WriteLine(response);
            var link = SelectAvalibleLink(response);
            await FollowYourNose(link);
        }


        private dynamic SelectAvalibleLink(dynamic response)
        {
            int optionIndex = 0;
            JObject jObject = (JObject) response;

            List<dynamic> allLinks = new List<dynamic>();
            WalkNode(jObject, n =>
            {
                JToken token = n["Links"];
                if (token != null && token.Type == JTokenType.Array)
                {
                    JArray tokenArray = (JArray) token;
                    allLinks.AddRange((dynamic) tokenArray);
                }
            });
            foreach (var link in allLinks)
            {
                Console.WriteLine("{0} - {1} -{2} {3}", optionIndex, link.SelectToken("Rel"), link.SelectToken("Name"),
                    link.SelectToken("Uri"));
                optionIndex++;
            }

            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            string enteredText = Console.ReadLine();
            int option = Convert.ToInt32(enteredText); // should error handle a little better :) 
            return allLinks[option];
        }


        private static void WalkNode(JToken node, Action<JObject> action)
        {
            if (node.Type == JTokenType.Array)
            {
                foreach (JToken child in node.Children())
                {
                    WalkNode(child, action);
                }
            }
            else if (node.Type == JTokenType.Object)
            {
                action((JObject) node);

                foreach (JProperty child in node.Children<JProperty>())
                {
                    WalkNode(child.Value, action);
                }
            }

        }
    }
}