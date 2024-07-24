# MyWeatherApp
----------------

This is a Blazor Web App created to utilize the National Weather Service API.

## Summary
This program takes a city name and a state name from the user, and uses those values to query the Geocode.maps.co API. The GET call returns the latitude and longitude of a matching city. This is essential because the NWS API only uses Reverse Geocoding. Geocode.maps.co allows Forward Geocoding which I utilized to get the latitude and longitude I needed for querying the NWS API.

After the latitude and longitude of the location was acquired, multiple consecutive calls to NWS API get forecast information for a 12-hour forecast and a weekly forecast and displays both of these to the user. 

## Unit testing
Additionally, there is a project of unit tests to test out the individual API calls. 

Unit testing was done with MSTest for C#.

## Other Fun Facts
This is my first EVER web app. I have worked on one other Web App before in my career, but as it was for a large organization, all of the setup and basics were already done for me. Not to mention, the web app that I did work on was based on JavaScript and AngularJS. I worked on it in 2018 when I was only 2 years into my degree, so I wasn't able to grasp some of the web-based concepts yet. Also, I was exclusively UI/UX and had no backend interactions. 

This was a HUGE challenge for me. I have used C# a lot in my current job, but I am self-taught in C#. This project showed me just how much I still have left to learn! Every single line of code on the Razor pages was something I learned in the past 48 hours. Not to mention, I also learned what a Razor page was. I had no idea what a huge undertaking this was. It's messy, it's probably (definitely) not using good design patterns, and I am 100% sure I could have used fewer lines of code. 

If given the opportunity, I would go back and learn more about client factories, JSON deserialization (Newtonsoft!), and general good practices for making API calls.

Regardless, I am proud of my first web app. Any and all constructive criticism is highly appreciated!
