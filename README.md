"# Frimley Flyers" 

# Constructing
Run the FF Web Server. e.g. C:\git\frimleyFlyers\WebDataEntry.Owin\bin\Debug\WebDataEntry.Owin.exe

run /build.bat - This uses Phatomn to scrape the output and place into the folder site/output
Use FileZilla to upload the site/output to the root of the FF web host
Debugging can be done via http://localhost:53002.

# New year
Add to routerconfig.js
update ffchampionship to pass the current season/year (this is always the current year)
add new ko component for the previous year. The html passes the season/year
add to sitemap.xml
add new json file - raceDataXXXX.json
add to navigation menu - local and normal
add to knockout-startup.js

update competitorsData.js with new people (same name competitorsData.js & AthletesManager.cs)
update AthletesManager.cs with the same new people (same name competitorsData.js & AthletesManager.cs)

In sitePhantomjs
add siteGenerator
    build.bat
    new pageHandler
    runner.js

Ideas:


Home Page:
Welcome to the Frimley Flyers. We are a reasonably new running group who all share the same passion for parkrun and local events. We vary considerably in ability, age and running experience.

About Us

The Club
The club has grown from a handful of friends who regularly attended Frimley Lodge parkrun back in 2013. Since, the numbers have grown considerably. Our aims were to 'Beast Mode' it and attempt to earn a 'Rio' down at the Cafe. In other words, run a PB and as a reward you'd get a can of Rio to drink.
This bit of fun was picked up by Rio, to the extent where they sponsored our running tops.

The Life Of A Full On Flyer
After a Frimley Lodge parkrun we share a cup of tea and a bacon roll down Mytchett Canal Centre. We also having some regular training runs on a Tuesday and a Thursday night.
The Frimley Flyers also runs a competition through out the year where you effectively run against your previous best time at various different distances ranging from 5km to Half Marathon.
A second, shorter distance event has also been introduced for those not fancying 13.1 miles! 
In the summer months we hold a BBQ and a running competition, again to suit all abilities. We also enter a number of teams at the Glastonbury For Runners, Endure24.
A fully fledged Flyer dons the white, green, purple and yellow ruuning vest or t-shirt.

Some pics


Become a Flyer
Becoming a Flyer would entail you joining one of our season long events: The FF Championship or FF Trophy - both of which involves a modest fee. You can join at any time.

