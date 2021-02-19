using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParserAPI.Extractors
{
    public class DateExtractor : Core.Infrastructure.IDateExtractor
    {
        public KeyValuePair<string, int> GetEmploymentDate(string line)
        {
            var result = DateTimeRecognizer.RecognizeDateTime(line, Culture.English);
            var toCurrentRegexPattern = @"([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|\–)?\s?(JANUARY|January|january|JAN|Jan|jan|01|1|FEBRUARY|February|february|FEB|Feb|feb|02|2|MARCH|March|march|MAR|Mar|mar|03|3|APRIL|April|april|APR|Apr|apr|04|4|MAY|May|may|05|5|JUNE|June|june|JUN|Jun|jun|06|6|JULY|July|july|JUL|Jul|jul|07|7|AUGUST|August|august|AUG|Aug|aug|08|8|SEPTEMBER|September|september|SEP|Sep|sep|09|9|OCTOBER|October|october|OCT|Oct|oct|10|NOVEMBER|November|november|NOV|Nov|nov|11|DECEMBER|December|december|DEC|Dec|dec|12)\s?(-|\/|\\|_|\||\.|\,|\–)?\s?([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|\–)?\s?([1-2][0-9])?([0-9]?[0-9])?\s?(-|TO|to|To)\s?(PRESENT|Present|present|NOW|Now|now|CURRENT|Current|current)";
            var toDateRegexPattern = @"([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|\–)?\s?(JANUARY|January|january|JAN|Jan|jan|01|1|FEBRUARY|February|february|FEB|Feb|feb|02|2|MARCH|March|march|MAR|Mar|mar|03|3|APRIL|April|april|APR|Apr|apr|04|4|MAY|May|may|05|5|JUNE|June|june|JUN|Jun|jun|06|6|JULY|July|july|JUL|Jul|jul|07|7|AUGUST|August|august|AUG|Aug|aug|08|8|SEPTEMBER|September|september|SEP|Sep|sep|09|9|OCTOBER|October|october|OCT|Oct|oct|10|NOVEMBER|November|november|NOV|Nov|nov|11|DECEMBER|December|december|DEC|Dec|dec|12)\s?(-|\/|\\|_|\||\.|\,|\–)?\s?([1-2][0-9])?([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|\–)?\s?([1-2][0-9])?([0-9]?[0-9])?\s?(\–|-|TO|to|To|THROUGH|Through|through|UNTIL|Until|until)\s?([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|\–)?\s?(JANUARY|January|january|JAN|Jan|jan|01|1|FEBRUARY|February|february|FEB|Feb|feb|02|2|MARCH|March|march|MAR|Mar|mar|03|3|APRIL|April|april|APR|Apr|apr|04|4|MAY|May|may|05|5|JUNE|June|june|JUN|Jun|jun|06|6|JULY|July|july|JUL|Jul|jul|07|7|AUGUST|August|august|AUG|Aug|aug|08|8|SEPTEMBER|September|september|SEP|Sep|sep|09|9|OCTOBER|October|october|OCT|Oct|oct|10|NOVEMBER|November|november|NOV|Nov|nov|11|DECEMBER|December|december|DEC|Dec|dec|12)\s?(-|\/|\\|_|\||\.|\,|\–)?\s?([1-2][0-9])?([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,|–)?\s?([1-2][0-9])?([0-9]?[0-9])?";
            var dateRegexPattern = @"([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,)?\s?(JANUARY|January|january|JAN|Jan|jan|01|1|FEBRUARY|February|february|FEB|Feb|feb|02|2|MARCH|March|march|MAR|Mar|mar|03|3|APRIL|April|april|APR|Apr|apr|04|4|MAY|May|may|05|5|JUNE|June|june|JUN|Jun|jun|06|6|JULY|July|july|JUL|Jul|jul|07|7|AUGUST|August|august|AUG|Aug|aug|08|8|SEPTEMBER|September|september|SEP|Sep|sep|09|9|OCTOBER|October|october|OCT|Oct|oct|10|NOVEMBER|November|november|NOV|Nov|nov|11|DECEMBER|December|december|DEC|Dec|dec|12)\s?(-|\/|\\|_|\||\.|\,)?\s?([0-9]?[0-9])?\s?(-|\/|\\|_|\||\.|\,)?\s?([1-2][0-9])?([0-9]?[0-9])?";
            var impossibleDateStartPattern = @"\s?(-|\/|\\|_|\||\.|\,)?\s?";
            var toCurrentRegex = new Regex(toCurrentRegexPattern);
            var toDateRegex = new Regex(toDateRegexPattern);
            var dateRegex = new Regex(dateRegexPattern);
            var impossibleStartDateRegex = new Regex(impossibleDateStartPattern);
            var potentialDates = new List<DateTime>();
            var potentialRange = result.FindAll(x => x.Text.Contains('-') || x.Text.Contains('-'));
            var months = 0;
            var fullDateRange = string.Empty;
            if(result.Count > 0 && 
                (result.ElementAt(0).Text.ToLower() == "quarter" || result.ElementAt(0).Text.ToLower() == "quarterly" ||
                result.ElementAt(0).Text.ToLower() == "annual" || result.ElementAt(0).Text.ToLower() == "annually" ||
                result.ElementAt(0).Text.ToLower() == "biannual" || result.ElementAt(0).Text.ToLower() == "biannually" ||
                result.ElementAt(0).Text.ToLower() == "bimonthly" || result.ElementAt(0).Text.ToLower() == "biweekly" ||                 
                result.ElementAt(0).Text.ToLower() == "daily" || result.ElementAt(0).Text.ToLower() == "hourly" ||
                result.ElementAt(0).Text.ToLower() == "weekly"))
            {
                return new KeyValuePair<string, int>(fullDateRange, months);
            }
            else if(toCurrentRegex.IsMatch(line))
            {
                var dateRange = toCurrentRegex.Match(line);
                var startDate = dateRegex.Match(dateRange.Value);
                var date = Convert.ToDateTime(startDate.Value);
                months = ((DateTime.Now.Year - date.Year) * 12) + DateTime.Now.Month - date.Month;
                fullDateRange += dateRange.Value;
            }
            else if (toDateRegex.IsMatch(line))
            {
                var dateRange = toDateRegex.Match(line);
                var datesInRange = dateRegex.Matches(dateRange.Value);
                var dates = new List<DateTime>();
                datesInRange.ToList().ForEach(x => {
                    var impossibleMatch = impossibleStartDateRegex.Match(x.Value).Value;
                    var impossibleIndex = x.Value.Remove(x.Value.IndexOf(impossibleMatch), x.Value.Length);
                    if (x.Value.StartsWith(impossibleMatch))
                    {
                        var fixedDate = x.Value.Remove(0, impossibleMatch.Length);
                        dates.Add(Convert.ToDateTime(fixedDate));
                        fullDateRange += fixedDate;
                    }
                    else
                    {
                        dates.Add(Convert.ToDateTime(x.Value));
                        fullDateRange += x.Value;
                    }
                });
                months = ((dates[1].Year - dates[0].Year) * 12) + dates[1].Month - dates[0].Month;
            }
            else if (potentialRange.Count == 1)
            {
                var datesInRange = potentialRange.ElementAt(0).Text.Split('-').ToList();
                var dates = new List<DateTime>(); 
                datesInRange.ForEach(x => { 
                    dates.Add(Convert.ToDateTime(x));
                    fullDateRange += x;
                }); 
                months = ((dates[1].Year - dates[0].Year) * 12) + dates[1].Month - dates[0].Month;
            }
            else if (potentialRange.Count > 1)
            {
                potentialRange.ForEach(x => {
                    potentialDates.Add(Convert.ToDateTime(x.Text));
                    fullDateRange += x.Text;
                });
                months = ((potentialDates[1].Year - potentialDates[0].Year) * 12) + potentialDates[1].Month - potentialDates[0].Month;
            }
            //else
            //{
            //    var firstResult = result.Count > 0 ? Convert.ToDateTime(result.ElementAt(0).Text) : new DateTime();
            //    var secondResult = result.Count > 1 ? Convert.ToDateTime(result.ElementAt(1).Text) : new DateTime();
            //    var currentDate = Regex.IsMatch(line.ToLower(), @"\b(present|current|now)\b") ? DateTime.Today : new DateTime();

            //    if (firstResult != new DateTime() && secondResult != new DateTime())
            //    {
            //        months = ((secondResult.Year - firstResult.Year) * 12) + secondResult.Month - firstResult.Month;
            //    }
            //    else if (firstResult != new DateTime() && currentDate != new DateTime())
            //    {
            //        months = ((currentDate.Year - firstResult.Year) * 12) + currentDate.Month - firstResult.Month;
            //    }
            //}

            return new KeyValuePair<string, int>(fullDateRange, months);
        }
    }
}