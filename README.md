KohaApiClient
this software is a part of my future product - KohaFramework - set of custom tools for KOHA instances.

KohaApiClient supports basic, and oauth authentication.
It has premade set of funtions for most common koha api endpoints.
and there will be possibility to create custom request to other endpoints.

All responses are in JSON format, but KohaApiClient can deserialize them to objects
for now only some of them - in future there will be option to create / inject custom 
classes for deserialization.

Usage:

  <img width="987" height="220" alt="image" src="https://github.com/user-attachments/assets/226ba719-d0db-4210-9256-6fead00183fb" />

You can also use cli version - (it can be compiled for linux too) - 

ApiClientCLI.exe -t GetBiblio -u https://yourkoha.com -l <client id/username> -p <password/client secret> -a oauth -q 12 -o test.txt
this will get biblio with id = 12, and will save output in test.txt file

ApiClientCLI.exe -t GetItemById -c -q 12345
this will load connection data from ApiConfig.ini file, and will display item data of item with ID = 12345
