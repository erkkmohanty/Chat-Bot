using AdaptiveCards;
using BotApplication2.Entity;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotApplication2.Utilities
{
    public static class AdaptiveCardManager
    {
        public static IList<Attachment> GetFlightsAdaptiveCards(IEnumerable<Flight> flights)
        {
            //List<Attachment> attachments = new List<Attachment>();

            //foreach (var flight in flights)
            //{
            //    AdaptiveCard adaptiveCard = new AdaptiveCard();
            //    adaptiveCard.Body.Add(new AdaptiveTextBlock()
            //    {
            //        Text = flight.Airline_Name,
            //        Size = AdaptiveTextSize.ExtraLarge,
            //        Separator = true,
            //    });
            //    adaptiveCard.Body.Add(new AdaptiveTextBlock()
            //    {
            //        Text = $"$:{flight.Price}",
            //        Size = AdaptiveTextSize.Large,
            //        Separator = true,
            //    });
            //    Attachment attachment = new Attachment()
            //    {
            //        ContentType = AdaptiveCard.ContentType,
            //        Content = adaptiveCard
            //    };
            //    attachments.Add(attachment);
            //}
            //return attachments;

            List<Attachment> attachments = new List<Attachment>();
            foreach (var flight in flights)
            {
                AdaptiveCard adaptiveCard = new AdaptiveCard();

                adaptiveCard.Body.Add(new AdaptiveTextBlock()
                {
                    Text = $"{flight.Airline_Name}",
                    Size = AdaptiveTextSize.ExtraLarge,
                    Weight = AdaptiveTextWeight.Bolder,
                    IsSubtle = false,
                });
                adaptiveCard.Body.Add(new AdaptiveTextBlock()
                {
                    Text = $"ID: {flight.ID}",
                    Weight = AdaptiveTextWeight.Bolder,
                    Separator = true
                });
                adaptiveCard.Body.Add(new AdaptiveTextBlock()
                {

                    Text = $"Ticket Type: {flight.Ticket_Type}",
                    //Weight = AdaptiveTextWeight.Bolder,
                    Separator = true
                });
                adaptiveCard.Body.Add(new AdaptiveTextBlock()
                {
                    Text = $"Date:{flight.Departure_Time.DayOfWeek},{flight.Departure_Time.Date.ToString("dd/MM/yyyy")}",
                    Weight = AdaptiveTextWeight.Bolder,
                    Separator = true
                });
                adaptiveCard.Body.Add(new AdaptiveColumnSet()
                {
                    Separator = true,
                    Columns = new List<AdaptiveColumn>()
                            { new AdaptiveColumn()
                                {
                                    Width="1",
                                    Items = new List<AdaptiveElement>()
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text=$"{flight.Departue_City}",
                                            IsSubtle=true

                                        },
                                        new AdaptiveTextBlock()
                                        {
                                            Text=$"{flight.Departue_City_Code}",
                                            Size=AdaptiveTextSize.ExtraLarge,
                                            Color=AdaptiveTextColor.Accent,
                                            Spacing=AdaptiveSpacing.None,
                                            IsSubtle=true

                                        },
                                         new AdaptiveTextBlock()
                                        {
                                            Text=$"Time: {flight.Departure_Time.Hour}:{flight.Departure_Time.Minute}",
                                            IsSubtle=true

                                        }
                                    }
                                } ,
                             new AdaptiveColumn()
                                {
                                    Width= AdaptiveColumnWidth.Auto,
                                    Items = new List<AdaptiveElement>()
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text=" "

                                        },
                                        new AdaptiveImage()
                                        {

                                           Url=new Uri("http://adaptivecards.io/content/airplane.png"),
                                            Size=AdaptiveImageSize.Small,
                                            Spacing=AdaptiveSpacing.None,

                                        }

                                } },
                            new AdaptiveColumn()
                            {
                                Width = "1",
                                Items = new List<AdaptiveElement>()
                                    {
                                        new AdaptiveTextBlock()
                                        {
                                            Text=$"{flight.Arrival_City}",
                                            IsSubtle=true,
                                            HorizontalAlignment= AdaptiveHorizontalAlignment.Right

                                        },
                                        new AdaptiveTextBlock()
                                        {
                                            Text=$"{flight.Arrival_City_Code}",
                                            Size=AdaptiveTextSize.ExtraLarge,
                                            HorizontalAlignment= AdaptiveHorizontalAlignment.Right,
                                            Color=AdaptiveTextColor.Accent,
                                            Spacing=AdaptiveSpacing.None,
                                            IsSubtle=true

                                        },
                                         new AdaptiveTextBlock()
                                        {
                                            Text=$"Time: {flight.Arrival_Time.Hour}:{flight.Arrival_Time.Minute}",
                                            HorizontalAlignment= AdaptiveHorizontalAlignment.Right,
                                            IsSubtle=true

                                        }
                                    }
                            } }
                });
                adaptiveCard.Body.Add(new AdaptiveTextBlock()
                {
                    Text = $"Total: $ {flight.Price}",
                    Size = AdaptiveTextSize.ExtraLarge,
                    Weight = AdaptiveTextWeight.Bolder,
                    Separator = true,
                    IsSubtle = false,
                });
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = adaptiveCard
                };
                attachments.Add(attachment);
            }
            return attachments;
        }
    }
}