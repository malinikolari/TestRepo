Given the flexibilty in requirement, I've created an app to serve the following use case: 
Use CarWebApp to define  attributes for a "Car" class and ping the NHTSA Product Information Catalog Vehicle Listing API(https://vpic.nhtsa.dot.gov/api/) to pull detailed information regarding VINs supplied in a predefined list in "vehiclesVins.txt". The JSON response is retrieved and deserialized into a list of "Car" objects. This information is displayed in the UI in a tabular form. 
Implemented in Asp.Net Core with Razor pages.

